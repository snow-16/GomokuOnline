using System;
using Fusion;

public class PlayerData : NetworkBehaviour
{
    [Networked]
    public int PlayerNo { get; private set; }
    [Networked]
    public int Turn { get; private set; }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetTurnServerRpc(int turn)
    {
        Turn = turn;
    }

    public void UpdateTurn()
    {
        RPC_SetTurnServerRpc(((Turn - 1) % 2) + 1);
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void SetNoServerRpc(int no)
    {
        PlayerNo = no;
    }
}
