/// <summary>
/// 対戦終了後の部屋帰還用ボタンクラス
/// </summary>
public class BackToRoomButton : CustomButton
{
    protected override void PressingAction()
    {
        DataManager.InGameData.RPC_ClearManager();
    }

    protected override void OnAwaking()
    {
        if(RoomData.Instance.PlayerNumber == 2)
        {
            ChangeInteractable();
        }
    }
}
