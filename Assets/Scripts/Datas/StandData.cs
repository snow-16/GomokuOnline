using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StandData
{
    private static int _size = 14;

    private static List<SquareData> _cells;
    private static List<bool> _cellsCanPut;

    public static SquareData GetCell(Vector2 pos)
    {
        return _cells[(int)pos.y * _size + (int)pos.x];
    }

    public static void SetCell(Vector2 pos, StoneColor color)
    {
        int index = (int)pos.y * _size + (int)pos.x;;
        _cells[index] = new SquareData(color, pos);
        _cellsCanPut[index] = false;
    }

    public static void ResetCells()
    {
        _cells = new List<SquareData>(_size * _size);
        _cellsCanPut = new List<bool>(_size * _size).Select(cell => true).ToList();
    }

    public record SquareData(StoneColor Color, Vector2 Pos);
}

namespace System.Runtime.CompilerServices
{
    internal static class IsExternalInit { }
}