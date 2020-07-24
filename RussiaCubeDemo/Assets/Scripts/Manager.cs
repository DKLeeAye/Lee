using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	public GameObject[] blocks;
	public Transform cube;
	public Transform leftWall;//��߽�
	public Transform rightWall;//�ұ߽�
	public int maxBlockSize = 4;//������󳤶�
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
	
	public static Manager manager;//��Ϸ�������manager

	// Use this for initialization
	void Start () 
	{
	
		if (manager == null)
		{
			manager = this;
		}
	
		blockRandom = Random.Range(0, blocks.Length);
		
		fieldWidth = _fieldWidth + maxBlockSize * 2;//��Ϸ�����
		fieldHeight = _fieldHeight + maxBlockSize;//��Ϸ�����   17
		fields = new bool[fieldWidth, fieldHeight];//�����Ͷ�ά����  ��Ϸ����18*17 (10+4*2,13+4)
		cubeYposition = new int[fieldHeight * fieldWidth];
		cubeTransforms = new Transform[fieldHeight * fieldWidth];

		//���߽߱粼��ֵȫ��Ϊtrue��ʹ�����޷�����
		for (int i = 0;i < fieldHeight;i++)
		{
			
			for (int j =0 ;j < maxBlockSize;j++)
			{
				
				fields[j, i] = true;
				fields[fieldWidth -1 - j, i] = true;
				
			}
			
		}
		
		//�ײ��߽�
		for (int i=0;i<fieldWidth;i++)
		{
			fields[i, 0] = true;
		}
		
		CreateBlock(blockRandom);//��������
	}
	
	void CreateBlock(int random)
	{
		Instantiate(blocks[random]);//ʵ��������
		blockRandom = Random.Range(0, blocks.Length);//�������������
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
	
	//����ӿ�
	public bool CheckBlock(bool [,] blockMatrix, int xPos, int yPos)
	{
		var size = blockMatrix.GetLength(0);//��0�г��ȣ�����ά��

		for (int y = 0;y < size;y++)
		{
			for (int x = 0;x < size;x++)
			{
				if (blockMatrix[y, x] && fields[xPos + x, yPos - y])//y=0��yPos=17������18���Ƿ����ӿ�  fields��blockMatrix�ĶԱ�  
				{
					return true;
				}
			}
		}	
		return false;
	}
	
	//����ͣ����λ��bool���������Ϊtrue�����ڷŵ�������
	public void SetBlock(bool[,] blockMatrix, int xPos, int yPos)
	{
		
		int size = blockMatrix.GetLength(0);//��0�г���
		for (int y = 0;y < size;y++)
		{
			for (int x = 0;x < size;x++)
			{
				if (blockMatrix[y, x])
				{
					Instantiate(cube, new Vector3(xPos + x, yPos - y, 0), Quaternion.identity);//��ԭ�ӿ�
					fields[xPos + x, yPos - y] = true;//��Ϸ���������Ӧλ����Ϊtrue
				}
			}
		}
		StartCoroutine(CheckRows(yPos - size, size));
		
	}
	
	
	//�Ƿ����
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
			for (x = maxBlockSize;x < fieldWidth - maxBlockSize;x++)//�ҵ�����һ���ӿ�X��+1
			{
				if (!fields[x, y])
				{
					break;
				}
			}
			if (x == fieldWidth - maxBlockSize)//����
			{
				yield return StartCoroutine(SetRows(y));
				y--;//����������y-1
				//count++;
			}
		}
		CreateBlock(blockRandom);//�ٴδ���
	}
	

	IEnumerator SetRows(int yStart)
	{
		//��Ϸ���򲼶�����ֵ�����½�һ��
		for (int y = yStart;y < fieldHeight - 1;y++)
		{
			for (int x = maxBlockSize;x < fieldWidth - maxBlockSize;x++)
			{
				fields[x, y] = fields[x, y + 1];
			}
		}
		//�½���������һ����Ϊfalse
		for (int x = maxBlockSize;x < fieldWidth - maxBlockSize;x++)
		{
			fields[x, fieldHeight - 1] = false;
		}
		
		var cubes = GameObject.FindGameObjectsWithTag("Cube");//cube���ñ�ǩ
		int cubeToMove = 0;
		for (int i = 0;i < cubes.Length;i++)
		{
			GameObject cube = cubes[i];
			if (cube.transform.position.y > yStart)
			{
				cubeYposition[cubeToMove] = (int)(cube.transform.position.y);
				cubeTransforms[cubeToMove++] = cube.transform;
			}//��Ҫ����һ�е�cube
			else if (cube.transform.position.y == yStart)
			{
				Destroy(cube);
			}//��Ҫ���ٵ�cube
		}
		
		float t = 0;
		while (t <= 1f)
		{
			t += Time.deltaTime * 5f;
			for(int i = 0;i < cubeToMove;i++)
			{
				cubeTransforms[i].position = new Vector3(cubeTransforms[i].position.x, Mathf.Lerp(cubeYposition[i], cubeYposition[i] - 1, t),
					cubeTransforms[i].position.z);//���������ʱ�����
			}
		    yield return null;
		}
		
		
		
	}
	
	public void GameOver()
	{
		print("Game Over!!!");
	}
}
