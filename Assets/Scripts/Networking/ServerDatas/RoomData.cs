using System;
using Fusion;
using UnityEngine;

public class RoomData
{
    public static RoomData Instance { get; private set; }

    public static void SetInstance()
    {
        Instance = new RoomData();
    }

    public static int OwnNumber()
    {
        return Instance.PlayerNumber;
    }

    public static int OpponentsNumber()
    {
        return (Instance.PlayerNumber % 2) + 1;
    }

    public static int OwnNumberIndex()
    {
        return OwnNumber() - 1;
    }

    public static int OpponentsNumberIndex()
    {
        return OpponentsNumber() - 1;
    }



    public string RoomCode { get; private set; }
    public int PlayerNumber { get; private set; }
    public int Turn { get; private set; }

    public void UpdateData(string code, int num, int turn)
    {
        RoomCode = code;
        Turn = turn;
        PlayerNumber = num;
    }
}
