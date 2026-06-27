/// <summary>
/// プレイヤーの石の割り当て色切り替えボタンクラス
/// </summary>
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
