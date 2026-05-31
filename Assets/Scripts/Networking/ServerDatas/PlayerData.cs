using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerData : NetworkBehaviour
{
    private NetworkVariable<int> _playerNo = new NetworkVariable<int>();
    private NetworkVariable<int> _turn = new NetworkVariable<int>();

    public int PlayerNo { get { return _playerNo.Value; } }
    public int Turn { get { return _turn.Value; } }

    void Start()
    {
        DataManager.PlayerData = this;
    }

    [ServerRpc]
    public void SetTurnServerRpc(int turn)
    {
        _turn.Value = turn;
    }

    [ClientRpc]
    public void PullTurnClientRpc()
    {
        PlayerReceiver.PullDatas();
    }

    public void UpdateTurn()
    {
        SetTurnServerRpc(((Turn - 1) % 2) + 1);
        PullTurnClientRpc();
    }

    [ServerRpc]
    public void SetNoServerRpc(int no)
    {
        _playerNo.Value = no;
    }
}
