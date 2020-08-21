using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class UIScroller : MonoBehaviour
{
    public enum Arrangement { Horizontal, Vertical, }
    public Arrangement _movement = Arrangement.Horizontal;
    //Item之间的距离
    [Range(0, 20)]
    public int cellPadiding = 5;
    //Item的宽高
    public int cellWidth = 500;
    public int cellHeight = 75;
    //默认加载的Item个数，一般比可显示个数大2~3个
    [Range(0, 20)]
    public int viewCount = 5;
    public GameObject itemPrefab;
    public RectTransform _content;

    public int mesNum = 0;
    private int _index = -1;

    public List<string> _mesText;
    public List<UIScrollIndex> _itemList;
    //private int _dataCount;
    public Queue<UIScrollIndex> _unUsedQueue;   //将未显示出来的Item存入未使用队列里面，等待需要使用的时候直接取出

    void Start()
    {
        _itemList = new List<UIScrollIndex>();
        _unUsedQueue = new Queue<UIScrollIndex>();
        _mesText = new List<string>();
        _content.GetComponent<RectTransform>().sizeDelta = GetContentSize();
        // DataCount = 100;
        // OnValueChange(Vector2.zero);
    }

    public UIScrollIndex AddItem(MsgChat msg)
    {
        UIScrollIndex item = CreateItem(mesNum);
        _content.GetComponent<RectTransform>().anchoredPosition = GetContentPos();
        _content.GetComponent<RectTransform>().sizeDelta = GetContentSize();
        item.mesIndex.text = mesNum.ToString();
        item.mesText.text = DateTime.Now.ToLongTimeString().ToString() + msg.userName + ":" + msg.chatMessage;
        _mesText.Add(item.mesText.text);
        mesNum++;
        return item;
    }

    public void OnValueChange(Vector2 pos)
    {
        int index = GetPosIndex();
        if (_index != index && index > -1)
        {
            _index = index;
            for (int i = _itemList.Count; i > 0; i--)
            {
                UIScrollIndex item = _itemList[i - 1];
                if (item.Index < index || (item.Index >= index + viewCount))
                {
                    //GameObject.Destroy(item.gameObject);
                    _itemList.Remove(item);
                    _unUsedQueue.Enqueue(item);
                }
            }
            for (int i = _index; i < _index + viewCount; i++)
            {
                if (i < 0) continue;
                // if (i > _dataCount - 1) continue;//超过100条
                if (i > mesNum - 1) continue;
                bool isOk = false;
                foreach (UIScrollIndex item in _itemList)
                {
                    if (item.Index == i) isOk = true;
                }
                if (isOk) continue;
                UIScrollIndex itemBase = CreateItem(i);
                itemBase.mesText.text = _mesText[i];
            }
        }
    }


    private UIScrollIndex CreateItem(int index)
    {
        UIScrollIndex itemBase;
        if (_unUsedQueue.Count > 0)
        {
            itemBase = _unUsedQueue.Dequeue();
        }
        else
        { 
            itemBase = GameTools.AddChild(_content, itemPrefab).GetComponent<UIScrollIndex>();
        }

        itemBase.Scroller = this;
        itemBase.Index = index;
        _itemList.Add(itemBase);

        return itemBase;
    }

    private int GetPosIndex()
    {
        return Mathf.FloorToInt(_content.GetComponent<RectTransform>().anchoredPosition.y / (cellHeight + cellPadiding));
    }

    public Vector3 GetPosition(int i)
    {
        switch (_movement)
        {
            case Arrangement.Horizontal:
                return new Vector3(i * (cellWidth + cellPadiding), 0f, 0f);
            case Arrangement.Vertical:
                return new Vector3(0f, i * -(cellHeight + cellPadiding), 0f);
        }
        return Vector3.zero;
    }

    public Vector3 GetContentPos()
    {
        if(mesNum > 4)
        {
            return new Vector3(0, (mesNum - 4) * (cellHeight + cellPadiding), 0);
        }
        return Vector3.zero;
    }

    public Vector2 GetContentSize()
    {
        return new Vector2(400, (cellHeight + cellPadiding) * (mesNum + 1));
    }
}
