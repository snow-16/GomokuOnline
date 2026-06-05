using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    [SerializeField]
    private NetworkRunner _networkRunnerPrefab;
    [SerializeField]
    private TMPro.TMP_InputField _inputField;
    [SerializeField]
    private TMPro.TextMeshProUGUI _codeView;

    void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            JoinServer(_codeView.text);
            // GameManager.TransitionScene(SceneType.InGame);
        }
    }

    private async void JoinServer(string code)
    {
        _codeView.text = (await RelayManager.JoinMatch(_networkRunnerPrefab, code)).ToString();
    }
}
