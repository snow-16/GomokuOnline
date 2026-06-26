using Fusion;
using UnityEngine;

public class InGameDataManager : NetworkBehaviour
{
    [Networked]
    public int SaveFinished { get; private set; }

    [Networked]
    public int Turn { get; private set; }
    [Networked]
    public StoneColor Winner { get; private set; }

    public override void Spawned()
    {
        RPC_SetTurn(RoomData.Instance.Turn);
        RPC_SetWinner(RoomData.Instance.Winner);

        DataManager.InGameData = this;
        SaveFinished = 0;
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

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ClearManager()
    {
        DataManager.InGameData = null;
        RPC_EndGame();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_EndGame()
    {
        SaveFinished++;
        if(SaveFinished == 2)
        {
            RelayManager.NetworkRunner.LoadScene("Room");
        }
    }
}
