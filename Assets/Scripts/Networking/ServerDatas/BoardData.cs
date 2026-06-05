using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fusion;
using Unity.Collections;
using UnityEngine;

public class BoardData : NetworkBehaviour
{
    public static int _size = 13;

    /// <summary> 盤面の状態。N=空、X=黒、O=白 </summary>
    [Networked]
    public NetworkString<_512> Cells { get; private set; } = new NetworkString<_512>(string.Concat(Enumerable.Repeat("N:", _size * _size))[..^2]);

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_UpdateCellsServer(string cells)
    {
        Cells = cells;
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
