using UnityEngine;
using UnityEngine.UI;

public class JoinLobbyButton : CustomButton
{
    protected override void PressingAction()
    {
        RelayManager.JoinLobby();
        gameObject.SetActive(false);
    }
}
