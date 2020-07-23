using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
	
	public GameObject[] blocks;
	public Transform cube;
	public Transform leftWall;
	public Transform rightWall;
	public int maxBlockSize = 4;
	public int _fieldWidth = 10;
	public int _fieldHeight = 13;

	private int fieldWidth;
	private int fieldHeight;
	private bool[,] fields;
	private int[] cubeYposition;
	private Transform[] cubeTransforms;
	private int clearTimes;

	private bool[,] blockMatrix;

	private int blockRandom;
	private GameObject nextBlock;
	private Block nextB;
	private int nextSize;
	private string[] nextblock;


	public static Manager manager;//游戏管理对象manager

	// Use this for initialization
	void Start () 
	{
		blockRandom = Random.Range(0, blocks.Length);
		
		

		fieldWidth = _fieldWidth + maxBlockSize * 2;
		fieldHeight = _fieldHeight + maxBlockSize;
		fields = new bool[fieldWidth, fieldHeight];//布尔型二维数组  游戏区域18*17
		cubeYposition = new int[fieldHeight * fieldWidth];
		cubeTransforms = new Transform[fieldHeight * fieldWidth];

		for (int i = 0; i < fieldWidth; i++)
		{
			fields[i, 0] = true;
		}


		CreateBlock(blockRandom);//随机创建方块

	
	}

	void CreateBlock(int random)
	{
		Instantiate(blocks[random]);//实例化对象
		blockRandom = Random.Range(0, blocks.Length);//随机数，块的序号
		nextBlock = blocks[blockRandom];//随机块布尔数组生成
		nextB = (Block)nextBlock.GetComponent("Block");//获取nextBlock的Block组件，强制转化成block型
		nextSize = nextB.block.GetLength(0);//获得第一维的长度
		nextblock = new string[nextSize];//新建长度相同的字符串
		nextblock = nextB.block;//储存第一维的字符串 例如0000
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

	public void SetBlock(bool[,] blockMatrix, int xPos, int yPos)
	{

		int size = blockMatrix.GetLength(0);//第一行长度
		for (int y = 0; y < size; y++)
		{
			for (int x = 0; x < size; x++)
			{
				if (blockMatrix[y, x])
				{
					Instantiate(cube, new Vector3(xPos + x, yPos - y, 0), Quaternion.identity);
				}
			}
		}		
	}


}
