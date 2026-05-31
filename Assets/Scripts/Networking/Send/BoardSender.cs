using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardSender
{
    public static List<StoneColor> GetCells()
    {
        return DataManager.BoardData.Cells.Split(':').Select(c => c switch
        {
            "X" => StoneColor.Black,
            "O" => StoneColor.White,
            _ => StoneColor.None
        }).ToList();
    }

    public static void SetCell(Vector2 pos, StoneColor color)
    {
        var cells = GetCells();
        cells[(int)(pos.y * BoardData._size + pos.x)] = color;
        DataManager.BoardData.UpdateCellsServerRpc(BoardData.CellListToString(cells));
    }

    public static void ResetBoard()
    {
        DataManager.BoardData.UpdateCellsServerRpc(BoardData.CellListToString(Enumerable.Repeat(StoneColor.None, BoardData._size * BoardData._size).ToList()));
    }
}
