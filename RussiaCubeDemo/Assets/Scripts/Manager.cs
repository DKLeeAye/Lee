using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	public GameObject[] blocks;
	public Transform cube;
	public Transform leftWall;//左边界
	public Transform rightWall;//右边界
	public int maxBlockSize = 4;//方块最大长度
	public int _fieldWidth = 10;
	public int _fieldHeight = 13;
	public float blockNormalFallSpeed = 2f;
	public float blockDropSpeed = 30f;

	private int blockRandom;

	private int fieldWidth;
	private int fieldHeight;
	private bool[,] fields;

	private int[] cubeYposition;
	private Transform[] cubeTransforms;
	
	public static Manager manager;//游戏管理对象manager

	// Use this for initialization
	void Start () 
	{
	
		if (manager == null)
		{
			manager = this;
		}
	
		blockRandom = Random.Range(0, blocks.Length);
		
		fieldWidth = _fieldWidth + maxBlockSize * 2;//游戏区域宽
		fieldHeight = _fieldHeight + maxBlockSize;//游戏区域高   17
		fields = new bool[fieldWidth, fieldHeight];//布尔型二维数组  游戏区域18*17 (10+4*2,13+4)
		cubeYposition = new int[fieldHeight * fieldWidth];
		cubeTransforms = new Transform[fieldHeight * fieldWidth];

		//两边边界布尔值全设为true，使方块无法出界
		for (int i = 0;i < fieldHeight;i++)
		{
			
			for (int j =0 ;j < maxBlockSize;j++)
			{
				
				fields[j, i] = true;
				fields[fieldWidth -1 - j, i] = true;
				
			}
			
		}
		
		//底部边界
		for (int i=0;i<fieldWidth;i++)
		{
			fields[i, 0] = true;
		}
		
		CreateBlock(blockRandom);//创建方块
	}
	
	void CreateBlock(int random)
	{
		Instantiate(blocks[random]);//实例化对象
		blockRandom = Random.Range(0, blocks.Length);//随机数，块的序号
	}
	
	public int GetFieldWidth()
	{
		return fieldWidth;
	}
	
	public int GetFieldHeight()
	{
		return fieldHeight;
	}
	
	public int GetBlockRandom()
	{
		return blockRandom;
	}
	
	//检查子块
	public bool CheckBlock(bool [,] blockMatrix, int xPos, int yPos)
	{
		var size = blockMatrix.GetLength(0);//第0行长度，数组维数

		for (int y = 0;y < size;y++)
		{
			for (int x = 0;x < size;x++)
			{
				if (blockMatrix[y, x] && fields[xPos + x, yPos - y])//y=0，yPos=17，检查第18行是否有子块  fields与blockMatrix的对比  
				{
					return true;
				}
			}
		}	
		return false;
	}
	
	//将块停留的位置bool区域矩阵设为true，并摆放单个方块
	public void SetBlock(bool[,] blockMatrix, int xPos, int yPos)
	{
		
		int size = blockMatrix.GetLength(0);//第0行长度
		for (int y = 0;y < size;y++)
		{
			for (int x = 0;x < size;x++)
			{
				if (blockMatrix[y, x])
				{
					Instantiate(cube, new Vector3(xPos + x, yPos - y, 0), Quaternion.identity);//还原子块
					fields[xPos + x, yPos - y] = true;//游戏区域数组对应位置设为true
				}
			}
		}
		StartCoroutine(CheckRows(yPos - size, size));
		
	}
	
	
	//是否成行
	IEnumerator CheckRows(int yStart, int size)
	{
		yield return null;
		if (yStart < 1)
		{
			yStart = 1;
		}
		//int count = 1;
		for (int y = yStart;y < yStart + size;y++)
		{
			int x;
			for (x = maxBlockSize;x < fieldWidth - maxBlockSize;x++)//找到该行一个子块X就+1
			{
				if (!fields[x, y])
				{
					break;
				}
			}
			if (x == fieldWidth - maxBlockSize)//行满
			{
				yield return StartCoroutine(SetRows(y));
				y--;//行消除所以y-1
				//count++;
			}
		}
		CreateBlock(blockRandom);//再次创建
	}
	

	IEnumerator SetRows(int yStart)
	{
		//游戏区域布尔数组值依次下降一行
		for (int y = yStart;y < fieldHeight - 1;y++)
		{
			for (int x = maxBlockSize;x < fieldWidth - maxBlockSize;x++)
			{
				fields[x, y] = fields[x, y + 1];
			}
		}
		//下降完最上面一行设为false
		for (int x = maxBlockSize;x < fieldWidth - maxBlockSize;x++)
		{
			fields[x, fieldHeight - 1] = false;
		}
		
		var cubes = GameObject.FindGameObjectsWithTag("Cube");//cube设置标签
		int cubeToMove = 0;
		for (int i = 0;i < cubes.Length;i++)
		{
			GameObject cube = cubes[i];
			if (cube.transform.position.y > yStart)
			{
				cubeYposition[cubeToMove] = (int)(cube.transform.position.y);
				cubeTransforms[cubeToMove++] = cube.transform;
			}//需要下移一行的cube
			else if (cube.transform.position.y == yStart)
			{
				Destroy(cube);
			}//需要销毁的cube
		}
		
		float t = 0;
		while (t <= 1f)
		{
			t += Time.deltaTime * 5f;
			for(int i = 0;i < cubeToMove;i++)
			{
				cubeTransforms[i].position = new Vector3(cubeTransforms[i].position.x, Mathf.Lerp(cubeYposition[i], cubeYposition[i] - 1, t),
					cubeTransforms[i].position.z);//方块下落的时间过渡
			}
		    yield return null;
		}
		
		
		
	}
	
	public void GameOver()
	{
		print("Game Over!!!");
	}
}
