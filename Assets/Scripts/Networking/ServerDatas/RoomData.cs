using System;
using Fusion;
using UnityEngine;

public class RoomData : NetworkBehaviour
{
    [Networked]
    public int PlayerCount { get; private set; }
}
