using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class BoardData : NetworkBehaviour
{
    public static int _size = 13;
    /// <summary> 盤面の状態。N=空、X=黒、O=白 </summary>
    private NetworkVariable<FixedString512Bytes> _cells = new NetworkVariable<FixedString512Bytes>(string.Concat(Enumerable.Repeat("N:", _size * _size))[..^2]);

    /// <summary> 盤面の状態。N=空、X=黒、O=白 </summary>
    public string Cells { get { return _cells.Value.ToString();} set { _cells.Value = value; } }

    void Start()
    {
        DataManager.BoardData = this;
    }

    [ServerRpc]
    public void UpdateCellsServerRpc(string cells)
    {
        _cells.Value = cells;
    }

    public static string CellListToString(List<StoneColor> cells)
    {
        return string.Join(":", cells.Select(c => c switch
        {
            StoneColor.Black => "X",
            StoneColor.White => "O",
            _ => "N"
        })); 
    }
}
