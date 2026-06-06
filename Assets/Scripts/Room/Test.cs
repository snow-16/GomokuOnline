using System.Threading.Tasks;
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

    async void Update()
    {
        if(Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            await JoinServer(_inputField.text);
            // GameManager.TransitionScene(SceneType.InGame);
        }
        else if(Keyboard.current.digit2Key.wasPressedThisFrame)
        {
        }
    }

    private async Task JoinServer(string code)
    {
        _codeView.text = (await RelayManager.JoinMatch(code)).ToString();
    }
}
