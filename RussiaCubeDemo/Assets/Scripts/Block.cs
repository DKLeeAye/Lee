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
		blockMatrix = new bool[size, size];//�����

		//��������
		for (int y=0;y<size;y++)
		{
			for(int x=0;x<size;x++)
			{
				if (block[y][x] == '1')
				{				
					blockMatrix[y, x] = true;

					//ʵ����һ��cube��ǿ��ת��ΪTransform����
					//childSize - y��i������������y��j���� ��  x��j�� - childSizeΪ����
					var cube = (Transform)Instantiate(Manager.manager.cube, new Vector3(x - childSize, childSize - y, 0), Quaternion.identity);
					cube.parent = transform;//���岻����������Ӱ��
				}
			}
		}

        yPosition = Manager.manager.GetFieldHeight() - 1;
		transform.position = new Vector3( Manager.manager.GetFieldWidth() / 2 + (size % 2 == 0 ? 0.5f : 0), yPosition - childSize, 0);//�����ʼλ��
		xPosition = (int)(transform.position.x - childSize);//�����е����ӿ����������
		fallSpeed = Manager.manager.blockNormalFallSpeed;
				
		//��Ϸ����
		if (Manager.manager.CheckBlock(blockMatrix, xPosition, yPosition))//���17 ��18���Ƿ����ӿ����
		{
			Manager.manager.GameOver();
			return;
		}

		StartCoroutine(CheckInput());
		StartCoroutine(Fall());
	}

    //����
    IEnumerator Fall()
	{
		while(true)
		{
			//��������
			for (float i = yPosition + 1;i > yPosition;i -= Time.deltaTime * fallSpeed)
			{
				transform.position = new Vector3(transform.position.x, i - childSize, transform.position.z);
				yield return null;
			}

			//�ж�ֹͣ��Destroy����SetBlock���������ٸ��鲢�ڶ�Ӧλ������cube��
			yPosition--;
			if (Manager.manager.CheckBlock(blockMatrix, xPosition, yPosition))
			{
				Manager.manager.SetBlock(blockMatrix, xPosition, yPosition + 1);
				Destroy(gameObject);
				break;
			}

		}
	}


    //ˮƽ�ƶ�
    IEnumerator MoveHorizontal(int distance)
	{
		
		if (!Manager.manager.CheckBlock(blockMatrix, xPosition + distance, yPosition))
		{
			transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
			xPosition += distance;
			yield return new WaitForSeconds(.1f);//��ʱ
		}
		
	}


    //��ת����
    void RotateBlock(){
		
		var tempMatrix = new bool[size, size];//�½�һ����������

	    for (int y = 0; y < size; y++)
		{
		     for (int x = 0; x < size; x++)
			{
		          tempMatrix[y, x] = blockMatrix[x, (size-1)-y];//��ת
	         }
		}
		
		if (!Manager.manager.CheckBlock(tempMatrix, xPosition, yPosition))//
		{
			System.Array.Copy(tempMatrix, blockMatrix, size * size);//����ת������鸴�ƹ�ȥ
			transform.Rotate(0, 0, 90);//������ת90��
		}
	}


	//����û�����
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
