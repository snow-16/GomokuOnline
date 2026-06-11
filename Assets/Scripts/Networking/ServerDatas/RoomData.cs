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



    public string RoomCode { get; private set; }
    public int Turn { get; private set; }

    public void UpdateData(string code, int turn)
    {
        RoomCode = code;
        Turn = turn;
    }
}
