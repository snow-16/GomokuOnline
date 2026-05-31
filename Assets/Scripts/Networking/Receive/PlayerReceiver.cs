using UnityEngine;

public class PlayerReceiver
{
    public static void PullDatas()
    {
        ObjectAcceser.PlayerController.SetNo(DataManager.PlayerData.PlayerNo);
        ObjectAcceser.PlayerController.SetTurn(DataManager.PlayerData.Turn);
    }
}
