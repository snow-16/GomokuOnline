using UnityEngine;

public class RoomSwitchColorButton : CustomButton
{
    protected override void PressingAction()
    {
        if(DataManager.PlayerData != null)
        {
            DataManager.PlayerData.ChangeColor();
        }
    }
}
