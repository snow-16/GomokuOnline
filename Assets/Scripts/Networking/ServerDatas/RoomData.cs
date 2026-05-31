using System;
using Unity.Netcode;
using UnityEngine;

public class RoomData : NetworkBehaviour
{
    private NetworkVariable<int> _playerCount = new NetworkVariable<int>();

    public int PlayerCount { get { return _playerCount.Value; } }

    void Start()
    {
        DataManager.RoomData = this;
    }
}
