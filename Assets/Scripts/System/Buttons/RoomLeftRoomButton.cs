using UnityEngine.SceneManagement;

/// <summary>
/// 部屋からの退室ボタンクラス
/// </summary>
public class RoomLeftRoomButton : CustomButton
{
    protected override async void PressingAction()
    {
        await RelayManager.NetworkRunner.Shutdown();
        Destroy(RelayManager.NetworkRunner.gameObject);
        SceneManager.LoadScene("Title");
    }
}
