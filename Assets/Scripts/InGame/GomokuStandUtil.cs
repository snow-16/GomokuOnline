using UnityEngine;

public class GomokuStandUtil
{
    private static float _gridSize = 0.537f;

    public static Vector2? PositionToCell(Vector2 pos)
    {
        float? x = null;
        for(int i = 0; i < 14; i++)
        {
            float gridPosX = (i - 6) * _gridSize;
            if(Mathf.Abs(gridPosX - pos.x) < _gridSize / 2)
            {
                x = gridPosX;
                break;
            }
        }

        float? y = null;
        for(int i = 0; i < 14; i++)
        {
            float gridPosY = (i - 6) * _gridSize;
            if(Mathf.Abs(gridPosY - pos.y) < _gridSize / 2)
            {
                y = gridPosY;
                break;
            }
        }

        return (x != null && y != null) ? new Vector2(x.Value, y.Value) : null;
    }
}
