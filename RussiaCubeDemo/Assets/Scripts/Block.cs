using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	
	public string[] block;
	
	private bool[,] blockMatrix;
	
	private int size;
	private float halfSize;
	private float halfSizeFloat;
	private float childSize;
	private int xPosition;
	private int yPosition;
	private float fallSpeed;
	private bool drop = false;
	
	// Use this for initialization
	void Start () 
	{

        size = block.Length;
		int width = block[0].Length;

		halfSize = (size + 1) * .5f;
		childSize = (size - 1) * .5f;//Size除以2再-0.5   方块0，1，2，3各自的相对位置计算   ！第0个子块的相对距离！！！
		halfSizeFloat = size * .5f;
		
		blockMatrix = new bool[size, size];//块矩阵  二维布尔数组
		for(int y=0;y<size;y++)
		{
			for(int x=0;x<size;x++){
				if (block[y][x] == '1'){
				
					blockMatrix[y, x] = true;

					//实例化一个cube并强制转化为Transform类型
					//childSize - y（i）是坐标下移y（j）格 ，  x（j） - childSize为右移
					var cube = (Transform)Instantiate(Manager.manager.cube, new Vector3(x - childSize, childSize - y, 0), Quaternion.identity);//实例化（复制）生成cube，设置坐标位置，默认角度
					cube.parent = transform;////物体不受其他外力影响

				}
			}
		}

			yPosition = Manager.manager.GetFieldHeight() - 1;
			transform.position = new Vector3(Manager.manager.GetFieldWidth() / 2 + (size % 2 == 0 ? 0.5f : 0), yPosition - childSize, 0);
			xPosition = (int)(transform.position.x - childSize);
	}
	


	// Update is called once per frame
	void Update ()
	{
	
	}
	
}
