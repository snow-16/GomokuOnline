using Fusion;

/// <summary>
/// 各セッション固有のデータ同期用クラス
/// </summary>
public class RoomDataManager: NetworkBehaviour
{
    /// <summary> 部屋コード </summary>
    [Networked]
    public NetworkString<_8> RoomCode { get; private set; }

    public override void Spawned()
    {
        RPC_SetCode(RoomData.Instance.RoomCode);

        DataManager.RoomData = this;
    }

    /// <summary>
    /// 部屋コードの変更
    /// </summary>
    /// <param name="data">変更先のコード</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetCode(string data)
    {
        RoomCode = data;
    }
}
