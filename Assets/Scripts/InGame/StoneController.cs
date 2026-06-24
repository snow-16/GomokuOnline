using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField]
    private Sprite _black;
    [SerializeField]
    private Sprite _white;

    private StoneColor _myColor;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _myColor == StoneColor.Black ? _black : _white;
        BoardSelfData.SetCell(transform.position, _myColor);
    }

    public void SetColor(StoneColor color)
    {
        _myColor = color;
    }
}
