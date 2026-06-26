using UnityEngine;
using TMPro;

public class JoinRoomButton : CustomButton
{
    [SerializeField]
    private TMP_InputField _codeInputField;

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
