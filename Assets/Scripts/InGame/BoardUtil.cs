using System;
using UnityEngine;

using Random = UnityEngine.Random;

/// <summary>
/// 五目並べ盤関連の演算用クラス
/// </summary>
public class BoardUtil
{
    /// <summary>
    /// マウス座標をマス目上にスナップした座標に変換
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static Vector2? PositionToCell(Vector2 pos)
    {
        float? x = null;
        for(int i = 0; i < 13; i++)
        {
            if(Mathf.Abs(i - pos.x) < 0.5f)
            {
                x = i;
                break;
            }
        }

        float? y = null;
        for(int i = 0; i < 13; i++)
        {
            if(Mathf.Abs(i - pos.y) < 0.5f)
            {
                y = i;
                break;
            }
        }

        return (x != null && y != null) ? new Vector2(x.Value, y.Value) : null;
    }

    /// <summary>
    /// 設置座標を中心に、五目並んだ列が存在するか判定する
    /// </summary>
    /// <param name="putPos">石の設置座標</param>
    /// <param name="color">石の色</param>
    /// <returns></returns>
    public static bool FilledFive(Vector2 putPos, StoneColor color)
    {
        for(int i = 0; i < 4; i++)
        {
            var vector = new Vector2(Math.Sign(MathF.Round(Mathf.Cos(Mathf.PI * i / 4), 1)), Math.Sign(MathF.Round(Mathf.Sin(Mathf.PI * i / 4), 1)));
            if(FilledLine(putPos, color, vector))
            {
                return true;
            }
        }
        
        return false;
    }

    /// <summary>
    /// 特定の1列に対して、五目並んでいるかの判定を行う
    /// </summary>
    /// <param name="putPos">設置座標</param>
    /// <param name="color">石の色</param>
    /// <param name="vector">列の方向ベクトル</param>
    /// <returns></returns>
    private static bool FilledLine(Vector2 putPos, StoneColor color, Vector2 vector)
    {
        var count = 0;

        for(int i = 0; i < 2; i++)
        {
            var checkPos = putPos;

            for(int j = 0; j < 4; j++)
            {
                checkPos += vector;

                if(checkPos.x < 0 || checkPos.x > 12 || checkPos.y < 0 || checkPos.y > 12 || !BoardSelfData.MatchColor(checkPos, color))
                {
                    break;
                }
                
                count++;
            }

            vector *= -1;
        }

        return count >= 4;
    }

    /// <summary>
    /// ランダムな空きマスの座標を取得する
    /// </summary>
    /// <returns></returns>
    public static Vector2 RandomEmptyCell()
    {
        var emptyCells = BoardSelfData.GetEmptyCells();
        return emptyCells[Random.Range(0, emptyCells.Count)];
    }
}
