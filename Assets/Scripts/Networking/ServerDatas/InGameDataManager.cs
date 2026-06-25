using Fusion;
using UnityEngine;

public class InGameDataManager : NetworkBehaviour
{
    [Networked]
    public int Turn { get; private set; }
    [Networked]
    public StoneColor Winner { get; private set; }

    public override void Spawned()
    {
        RPC_SetTurn(RoomData.Instance.Turn);
        RPC_SetWinner(RoomData.Instance.Winner);

        DataManager.InGameData = this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetTurn(int data)
    {
        Turn = data;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetWinner(StoneColor data)
    {
        Winner = data;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SwitchTurn()
    {
        Turn = (Turn % 2) + 1;
    }
}
