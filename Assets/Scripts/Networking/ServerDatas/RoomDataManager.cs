using System;
using Fusion;
using UnityEngine;

public class RoomDataManager: NetworkBehaviour
{
    [Networked]
    public NetworkString<_8> RoomCode { get; private set; }

    public override void Spawned()
    {
        RPC_SetCode(RoomData.Instance.RoomCode);

        DataManager.RoomData = this;
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetCode(string data)
    {
        RoomCode = data;
    }
}
