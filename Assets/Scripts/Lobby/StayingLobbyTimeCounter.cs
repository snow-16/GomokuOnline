using UnityEngine;
using TMPro;

/// <summary>
/// ロビー内での滞在時間表示用クラス
/// </summary>
public class StayingLobbyTimeCounter : MonoBehaviour
{
    /// <summary> 滞在時間表示用テキストUI </summary>
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
