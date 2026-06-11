using UnityEngine;
using UnityEngine.UI;

public class StayingLobbyTimeCounter : MonoBehaviour
{
    private Text _countingText;

    void Start()
    {
        _countingText = GetComponent<Text>();
    }

    void Update()
    {
        _countingText.text = $"滞在可能時間: {LobbyData._stayingLobbyTime:0.0}";
    }
}
