using UnityEngine;

/// <summary>
/// 五目並べ盤上の状態操作用クラス
/// </summary>
public class BoardController : MonoBehaviour
{
    /// <summary> 盤上に設置する用の石プレハブ </summary>
    [SerializeField]
    private GameObject _stonePrefab;

    /// <summary> 外部からのBoardControllerアクセス用プロパティ </summary>
    public static BoardController Instance { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }
    
    /// <summary>
    /// 石の設置処理。
    /// その後にターン進行も行う
    /// </summary>
    /// <param name="pos">設置座標</param>
    /// <param name="color">設置する色</param>
    public async void DecisionPutStone(Vector2 pos, StoneColor color)
    {
        await RelayManager.NetworkRunner.SpawnAsync(_stonePrefab, pos, Quaternion.identity, RelayManager.NetworkRunner.LocalPlayer,
        (runner, obj) =>
        {
            var stoneController = obj.GetComponent<StoneController>();
            stoneController.SetColor(color);
        });

        DataManager.InGameData.RPC_SwitchTurn();

        if(BoardUtil.FilledFive(pos, color))
        {
            DataManager.InGameData.RPC_SetWinner(color);
        }
    }
}
