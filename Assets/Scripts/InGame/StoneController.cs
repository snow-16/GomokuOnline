using Fusion;
using UnityEngine;

/// <summary>
/// 設置した石の状態同期クラス
/// </summary>
public class StoneController : NetworkBehaviour
{
    /// <summary> 黒色の石のスプライト </summary>
    [SerializeField]
    private Sprite _black;
    /// <summary> 白色の石のスプライト </summary>
    [SerializeField]
    private Sprite _white;

    /// <summary> 石の色同期用プロパティ </summary>
    [Networked]
    public StoneColor MyColor { get; private set; }

    public override void Spawned()
    {
        GetComponent<SpriteRenderer>().sprite = MyColor == StoneColor.Black ? _black : _white;
        BoardSelfData.SetCell(transform.position, MyColor);
    }

    /// <summary>
    /// 石の色を指定してクライアント間で同期する
    /// </summary>
    /// <param name="color"></param>
    public void SetColor(StoneColor color)
    {
        MyColor = color;
    }
}
