using UnityEngine;
using TMPro;

/// <summary>
/// 現在のサーバー接続人数表示用クラス
/// </summary>
public class MembersViewer : MonoBehaviour
{
    /// <summary> 接続人数表示用テキストUI </summary>
    private TextMeshProUGUI _membersViewText;
    /// <summary> 現在のサーバー接続人数 </summary>
    private int _memberCount = 0;

    void Start()
    {
        _membersViewText = GetComponent<TextMeshProUGUI>();
        _membersViewText.text = $"残り接続可能人数: 16人";
    }

    void Update()
    {
        if(_memberCount != LobbyData._playerCount)
        {
            _membersViewText.text = $"残り接続可能人数: {_memberCount = 16 - LobbyData._playerCount}人";
        }
    }
}
