using UnityEngine;
using TMPro;

public class StayingLobbyTimeCounter : MonoBehaviour
{
    private TextMeshProUGUI _countingText;

    void Start()
    {
        _countingText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _countingText.text = $"滞在可能時間: {LobbyData._stayingLobbyTime:0.0}";
    }
}
