using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 五目並べ盤の状態データ保管クラス
/// </summary>
public class BoardSelfData
{
    /// <summary> 盤面状態データリスト </summary>
    public static List<StoneColor> CellData { get; private set; }
    
    /// <summary>
    /// 盤面のリセット
    /// </summary>
    public static void ResetBoard()
    {
        CellData = new(Enumerable.Repeat(StoneColor.None, 13 * 13));
    }

    /// <summary>
    /// 盤面上の座標を盤面データリストのインデックスへ変換
    /// </summary>
    /// <param name="cellPos">盤面座標</param>
    /// <returns></returns>
    private static int PosToIndex(Vector2 cellPos)
    {
        return (int)(cellPos.y * 13 + cellPos.x);
    }

    /// <summary>
    /// 特定のマスの状態取得
    /// </summary>
    /// <param name="cellPos">取得するマスの座標</param>
    /// <returns></returns>
    public static StoneColor GetCell(Vector2 cellPos)
    {
        return CellData[PosToIndex(cellPos)];
    }

    /// <summary>
    /// 特定のマスの状態変更
    /// </summary>
    /// <param name="cellPos">変更するマスの座標</param>
    /// <param name="color">置き換える色</param>
    /// <returns></returns>
    public static void SetCell(Vector2 cellPos, StoneColor color)
    {
        CellData[PosToIndex(cellPos)] = color;
    }

    /// <summary>
    /// 盤面上の空きマスをリストとして取得
    /// </summary>
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

    /// <summary>
    /// 特定のマスの色を判定する
    /// </summary>
    /// <param name="cellPos">判定するマスの座標</param>
    /// <param name="color">比較する色</param>
    /// <returns></returns>
    public static bool MatchColor(Vector2 cellPos, StoneColor color)
    {
        return CellData[PosToIndex(cellPos)] == color;
    }

    /// <summary>
    /// 特定のマスが空きマスか判定
    /// </summary>
    /// <param name="cellPos">判定するマスの座標</param>
    /// <returns></returns>
    public static bool IsNone(Vector2 cellPos)
    {
        return MatchColor(cellPos, StoneColor.None);
    }

    /// <summary>
    /// 盤上が完全に埋まっているか判定
    /// </summary>
    public static bool FullyCells()
    {
        return !CellData.Contains(StoneColor.None);
    }
}
