using UnityEngine;
using UnityEngine.UI;

public class JoinRoomButton : CustomButton
{
    [SerializeField]
    private InputField _codeInputField;

    protected override void PressingAction()
    {
        if(LobbyData._sessionPassList.Contains(_codeInputField.text))
        {
            RelayManager.JoinRoom(_codeInputField.text);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("部屋コードが間違っています。");
        }
    }
}
