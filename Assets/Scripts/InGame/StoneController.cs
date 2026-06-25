using Fusion;
using UnityEngine;

public class StoneController : NetworkBehaviour
{
    [SerializeField]
    private Sprite _black;
    [SerializeField]
    private Sprite _white;

    public StoneColor _myColor;

    public override void Spawned()
    {
        GetComponent<SpriteRenderer>().sprite = _myColor == StoneColor.Black ? _black : _white;
        BoardSelfData.SetCell(transform.position, _myColor);
    }

    public void SetColor(StoneColor color)
    {
        _myColor = color;
    }
}
