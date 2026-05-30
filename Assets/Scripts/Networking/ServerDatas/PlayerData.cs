using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    private NetworkVariable<string> _playerName = new NetworkVariable<string>();
    private NetworkVariable<int> _turn = new NetworkVariable<int>();

    public string PlayerName { get { return _playerName.Value; } }
    public int Turn { get { return _turn.Value; } }

    void Start()
    {
        DataManager.PlayerData = this;
    }
}
