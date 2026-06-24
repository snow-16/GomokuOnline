using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardSelfData
{
    public static List<StoneColor> CellData { get; private set; }

    public static void ResetBoard()
    {
        CellData = new(Enumerable.Repeat(StoneColor.None, 13 * 13));
    }

    private static int PosToIndex(Vector2 cellPos)
    {
        return (int)(cellPos.y * 13 + cellPos.x);
    }

    public static StoneColor GetCell(Vector2 cellPos)
    {
        return CellData[PosToIndex(cellPos)];
    }

    public static void SetCell(Vector2 cellPos, StoneColor color)
    {
        CellData[PosToIndex(cellPos)] = color;
    }

    public static List<Vector2> GetEmptyCells()
    {
        var output = new List<Vector2>();

        for(int x = 0; x < 13; x++)
        {
            for(int y = 0; y < 13; y++)
            {
                var checkCell = new Vector2(x, y);
                if(GetCell(checkCell) == StoneColor.None)
                {
                    output.Add(checkCell);
                }
            }
        }

        return output;
    }

    public static bool MatchColor(Vector2 cellPos, StoneColor color)
    {
        return CellData[PosToIndex(cellPos)] == color;
    }

    public static bool IsNone(Vector2 cellPos)
    {
        return MatchColor(cellPos, StoneColor.None);
    }
}
