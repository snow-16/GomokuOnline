using System;
using Unity.Netcode;
using UnityEngine;

public class RoomData : NetworkBehaviour
{
    private NetworkVariable<string> _roomId = new NetworkVariable<string>();
    private NetworkVariable<int> _playerCount = new NetworkVariable<int>();

    public string RoomId { get { return _roomId.Value; } }
    public int PlayerCount { get { return _playerCount.Value; } }

    void Start()
    {
        DataManager.RoomData = this;
    }
}
