using UnityEngine;

public class PlayerSender
{
    public static void UpdateTurn()
    {
        DataManager.PlayerData.UpdateTurn();
    }

    public static void SetTurn(int turn)
    {
        DataManager.PlayerData.RPC_SetTurnServerRpc(turn);
    }
}
