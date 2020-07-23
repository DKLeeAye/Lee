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


	public static Manager manager;//��Ϸ�������manager

	// Use this for initialization
	void Start () 
	{
		blockRandom = Random.Range(0, blocks.Length);
		
		

		fieldWidth = _fieldWidth + maxBlockSize * 2;
		fieldHeight = _fieldHeight + maxBlockSize;
		fields = new bool[fieldWidth, fieldHeight];//�����Ͷ�ά����  ��Ϸ����18*17
		cubeYposition = new int[fieldHeight * fieldWidth];
		cubeTransforms = new Transform[fieldHeight * fieldWidth];

		for (int i = 0; i < fieldWidth; i++)
		{
			fields[i, 0] = true;
		}


		CreateBlock(blockRandom);//�����������

	
	}

	void CreateBlock(int random)
	{
		Instantiate(blocks[random]);//ʵ��������
		blockRandom = Random.Range(0, blocks.Length);//�������������
		nextBlock = blocks[blockRandom];//����鲼����������
		nextB = (Block)nextBlock.GetComponent("Block");//��ȡnextBlock��Block�����ǿ��ת����block��
		nextSize = nextB.block.GetLength(0);//��õ�һά�ĳ���
		nextblock = new string[nextSize];//�½�������ͬ���ַ���
		nextblock = nextB.block;//�����һά���ַ��� ����0000
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

		int size = blockMatrix.GetLength(0);//��һ�г���
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
