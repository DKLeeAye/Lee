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
		childSize = (size - 1) * .5f;//Size����2��-0.5   ����0��1��2��3���Ե����λ�ü���   ����0���ӿ����Ծ��룡����
		halfSizeFloat = size * .5f;
		
		blockMatrix = new bool[size, size];//�����  ��ά��������
		for(int y=0;y<size;y++)
		{
			for(int x=0;x<size;x++){
				if (block[y][x] == '1'){
				
					blockMatrix[y, x] = true;

					//ʵ����һ��cube��ǿ��ת��ΪTransform����
					//childSize - y��i������������y��j���� ��  x��j�� - childSizeΪ����
					var cube = (Transform)Instantiate(Manager.manager.cube, new Vector3(x - childSize, childSize - y, 0), Quaternion.identity);//ʵ���������ƣ�����cube����������λ�ã�Ĭ�ϽǶ�
					cube.parent = transform;////���岻����������Ӱ��

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
