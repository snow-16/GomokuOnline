using Fusion;

/// <summary>
/// インゲームシーン内で利用するデータの同期用クラス
/// </summary>
public class InGameDataManager : NetworkBehaviour
{
    /// <summary> DataManager上のデータリセットが完了したクライアントの数 </summary>
    [Networked]
    public int ClearFinished { get; private set; }

    /// <summary> 現在のターン </summary>
    [Networked]
    public int Turn { get; private set; }
    /// <summary> 対戦の勝者 </summary>
    [Networked]
    public StoneColor Winner { get; private set; }

    public override void Spawned()
    {
        RPC_SetTurn(RoomData.Instance.Turn);
        RPC_SetWinner(RoomData.Instance.Winner);

        DataManager.InGameData = this;
        ClearFinished = 0;
    }

    /// <summary>
    /// ターン設定
    /// </summary>
    /// <param name="data">ターンを渡すプレイヤー番号</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetTurn(int data)
    {
        Turn = data;
    }

    /// <summary>
    /// 勝者設定
    /// </summary>
    /// <param name="data">勝利プレイヤーの石の色</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetWinner(StoneColor data)
    {
        Winner = data;
    }

    /// <summary>
    /// 次のプレイヤーへターンを渡す
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SwitchTurn()
    {
        Turn = (Turn % 2) + 1;
    }

    /// <summary>
    /// DataManager上から自身を削除。
    /// シーン遷移中の誤アクセスを防止する
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ClearManager()
    {
        DataManager.InGameData = null;
        RPC_EndGame();
    }

    /// <summary>
    /// データリセットの完了を通達
    /// 全員完了すれば部屋へ戻る
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_EndGame()
    {
        ClearFinished++;
        if(ClearFinished == 2)
        {
            RelayManager.NetworkRunner.LoadScene("Room");
        }
    }
}
