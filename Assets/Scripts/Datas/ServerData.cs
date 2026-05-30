using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

public class ServerData
{
    public static PlayerController[] Players { get; private set; } = new PlayerController[2];

    public static int Turn { get; private set; } = 1;

    public static void Reset()
    {
        Players[0] = null;
        Players[1] = null;
        Turn = 1;
    }

    public static void Join(PlayerController player)
    {
        if (Players[0] == null)
        {
            Players[0] = player;
        }
        else if (Players[1] == null)
        {
            Players[1] = player;
        }
    }

    public static void NextTurn()
    {
        Turn = Turn % 2 + 1;
        Players[Turn - 1].SetTurnClientRpc(true);
        Players[Turn % 2].SetTurnClientRpc(false);
    }

    [ClientRpc]
    public static void OveringCellClientRpc(Vector2 pos)
    {
        Players[Turn - 1].OveringCellPos = pos;
    }
}