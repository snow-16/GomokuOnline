using Fusion;
using UnityEngine;

public class StoneController : NetworkBehaviour
{
    [SerializeField]
    private Sprite _black;
    [SerializeField]
    private Sprite _white;

    [Networked]
    public StoneColor MyColor { get; private set; }

    public override void Spawned()
    {
        GetComponent<SpriteRenderer>().sprite = MyColor == StoneColor.Black ? _black : _white;
        BoardSelfData.SetCell(transform.position, MyColor);
    }

    public void SetColor(StoneColor color)
    {
        MyColor = color;
    }
}
