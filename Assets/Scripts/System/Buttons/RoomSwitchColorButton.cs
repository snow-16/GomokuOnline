using UnityEngine;

public class RoomSwitchColorButton : CustomButton
{
    protected override void PressingAction()
    {
        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 2 && DataManager.PlayerData != null)
        {
            DataManager.PlayerData.ChangeColor();
        }
    }
}
