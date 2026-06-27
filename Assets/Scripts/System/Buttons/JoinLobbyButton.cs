using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyButton : CustomButton
{
    [SerializeField]
    private GameObject loadingText;

    protected override void PressingAction()
    {
        RelayManager.JoinLobby();
        loadingText.SetActive(true);
        gameObject.SetActive(false);
    }
}
