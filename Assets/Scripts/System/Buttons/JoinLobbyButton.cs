using UnityEngine;

/// <summary>
/// タイトルからロビーへの入室ボタンクラス
/// </summary>
public class JoinLobbyButton : CustomButton
{
    /// <summary> ロード中表示テキストUI </summary>
    [SerializeField]
    private GameObject loadingText;

    protected override void PressingAction()
    {
        RelayManager.JoinLobby();
        loadingText.SetActive(true);
        gameObject.SetActive(false);
    }
}
