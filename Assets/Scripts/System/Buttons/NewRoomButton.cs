using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class NewwRoomButton : CustomButton
{
    [SerializeField]
    private InputField _codeInputField;

    protected override void PressingAction()
    {
        if(_codeInputField.text.Length == 0)
        {
            RelayManager.CreateRoom();
            gameObject.SetActive(false);
        }
        else if(_codeInputField.text.Length == 6)
        {
            if(!Regex.IsMatch(_codeInputField.text, @"^[A-Z]+$"))
            {
                Debug.Log("コードはアルファベット大文字で統一してください。");
            }
            else if(!LobbyData._sessionPassList.Contains(_codeInputField.text))
            {
                RelayManager.CreateRoom(_codeInputField.text);
                gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("そのコードは既に存在します。");
            }
        }
        else if(_codeInputField.text.Length > 6)
        {
            Debug.Log("部屋コードが長すぎます。コードはアルファベット大文字で6文字です。");
        }
        else
        {
            Debug.Log("部屋コードが短すぎます。コードはアルファベット大文字で6文字です。");
        }
    }
}
