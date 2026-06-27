/// <summary>
/// 対戦開始ボタンクラス
/// </summary>
public class RoomStartGameButton : CustomButton
{
    void Update()
    {
        if(!_interactable && RoomData.Instance.PlayerNumber == 1)
        {
            ChangeInteractable();
        }
        else if(_interactable && RoomData.Instance.PlayerNumber == 2)
        {
            ChangeInteractable();
        }
    }

    protected override void PressingAction()
    {
        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 2)
        {
            DataManager.PlayerData.RPC_SaveData();
        }
    }
}
