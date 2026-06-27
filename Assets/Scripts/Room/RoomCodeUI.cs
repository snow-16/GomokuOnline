using UnityEngine;
using TMPro;

/// <summary>
/// ルームコード表示用クラス
/// </summary>
public class RoomCodeUI : MonoBehaviour
{
    /// <summary> 部屋コード表示テキストUI </summary>
    private TextMeshProUGUI _roomCodeText;

    /// <summary>
    /// データ初期化
    /// </summary>
    public void Initiation()
    {
        _roomCodeText = GetComponent<TextMeshProUGUI>();
        _roomCodeText.text = DataManager.RoomData.RoomCode.Value;
    }
}
