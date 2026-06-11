using System;
using Fusion;
using UnityEngine;

public class RoomDataManager: NetworkBehaviour
{
    [Networked]
    public NetworkString<_8> RoomCode { get; private set; }
    [Networked]
    public int Turn { get; private set; }

    public override void Spawned()
    {
        DataManager.RoomData = this;

        RoomCode = RoomData.Instance.RoomCode;
        Turn = RoomData.Instance.Turn;
    }
}
