using UnityEngine.SceneManagement;

/// <summary>
/// 対戦中の接続切断時のタイトル帰還ボタンクラス
/// </summary>
public class BackToTitleButton : CustomButton
{

    protected override async void PressingAction()
    {
        await RelayManager.NetworkRunner.Shutdown();
        Destroy(RelayManager.NetworkRunner.gameObject);
        SceneManager.LoadScene("Title");
    }
}
