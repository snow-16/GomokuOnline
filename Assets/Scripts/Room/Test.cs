using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_InputField _inputField;
    [SerializeField]
    private TMPro.TextMeshProUGUI _codeView;

    void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            CreateServer();
            // GameManager.TransitionScene(SceneType.InGame);
        }
        if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            JoinServer(_inputField.text);
            // GameManager.TransitionScene(SceneType.InGame);
        }
    }

    private async void CreateServer()
    {
        _codeView.text = await RelayManager.CreateRelay();
    }

    private async void JoinServer(string code)
    {
        await RelayManager.JoinRelay(code);
    }
}
