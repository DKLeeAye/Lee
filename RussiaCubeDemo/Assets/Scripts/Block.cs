using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	
	public string[] block;
	
	private bool[,] blockMatrix;
	
	private int size;
	private float childSize;
	private int xPosition;
	private int yPosition;
	private float fallSpeed;
	
	// Use this for initialization
	void Start () 
	{
        size = block.Length;
		int width = block[0].Length;
		childSize = (size - 1) * .5f;		
		blockMatrix = new bool[size, size];//块矩阵

		//方块生成
		for (int y=0;y<size;y++)
		{
			for(int x=0;x<size;x++)
			{
				if (block[y][x] == '1')
				{				
					blockMatrix[y, x] = true;

					//实例化一个cube并强制转化为Transform类型
					//childSize - y（i）是坐标下移y（j）格 ，  x（j） - childSize为右移
					var cube = (Transform)Instantiate(Manager.manager.cube, new Vector3(x - childSize, childSize - y, 0), Quaternion.identity);
					cube.parent = transform;//物体不受其他外力影响
				}
			}
		}

        yPosition = Manager.manager.GetFieldHeight() - 1;
		transform.position = new Vector3( Manager.manager.GetFieldWidth() / 2 + (size % 2 == 0 ? 0.5f : 0), yPosition - childSize, 0);//方块初始位置
		xPosition = (int)(transform.position.x - childSize);//方块中单个子块的世界坐标
		fallSpeed = Manager.manager.blockNormalFallSpeed;
				
		//游戏结束
		if (Manager.manager.CheckBlock(blockMatrix, xPosition, yPosition))//编号17 第18排是否有子块出现
		{
			Manager.manager.GameOver();
			return;
		}

		StartCoroutine(CheckInput());
		StartCoroutine(Fall());
	}

    //下落
    IEnumerator Fall()
	{
		while(true)
		{
			//父块下落
			for (float i = yPosition + 1;i > yPosition;i -= Time.deltaTime * fallSpeed)
			{
				transform.position = new Vector3(transform.position.x, i - childSize, transform.position.z);
				yield return null;
			}

			//判断停止，Destroy并且SetBlock（）（销毁父块并在对应位置设置cube）
			yPosition--;
			if (Manager.manager.CheckBlock(blockMatrix, xPosition, yPosition))
			{
				Manager.manager.SetBlock(blockMatrix, xPosition, yPosition + 1);
				Destroy(gameObject);
				break;
			}

		}
	}


    //水平移动
    IEnumerator MoveHorizontal(int distance)
	{
		
		if (!Manager.manager.CheckBlock(blockMatrix, xPosition + distance, yPosition))
		{
			transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
			xPosition += distance;
			yield return new WaitForSeconds(.1f);//延时
		}
		
	}


    //旋转方块
    void RotateBlock(){
		
		var tempMatrix = new bool[size, size];//新建一布尔型数组

	    for (int y = 0; y < size; y++)
		{
		     for (int x = 0; x < size; x++)
			{
		          tempMatrix[y, x] = blockMatrix[x, (size-1)-y];//旋转
	         }
		}
		
		if (!Manager.manager.CheckBlock(tempMatrix, xPosition, yPosition))//
		{
			System.Array.Copy(tempMatrix, blockMatrix, size * size);//将旋转后的数组复制过去
			transform.Rotate(0, 0, 90);//父块旋转90度
		}
	}


	//检测用户输入
	IEnumerator CheckInput()
	{
		
		while(true)
		{
			var input = Input.GetAxisRaw("Horizontal");
			if (input < 0)
			{
				yield return StartCoroutine(MoveHorizontal(-1));
			}
			
			if (input > 0)
			{
				yield return StartCoroutine(MoveHorizontal(1));
			}
			
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				RotateBlock();
			}
			
			if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				fallSpeed = Manager.manager.blockDropSpeed;
				
			}
			
			yield return null;
		}
		
	}


}
