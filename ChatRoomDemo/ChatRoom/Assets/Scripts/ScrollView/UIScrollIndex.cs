using UnityEngine;
using UnityEngine.UI;

public class UIScrollIndex : MonoBehaviour
{
    public Text mesIndex;
    public Text mesText;

    private UIScroller _scroller;
    private int _index;

    void Start()
    {
        mesIndex.text = _index.ToString();

    }
    public int Index
    {
        get { return _index; }
        set
        {
            _index = value;
            transform.localPosition = _scroller.GetPosition(_index);
            mesIndex.text = _index.ToString();
            gameObject.name = "Message" + (_index < 10 ? "0" + _index : _index.ToString());
        }
    }

    public UIScroller Scroller
    {
        set { _scroller = value; }
    }
}
