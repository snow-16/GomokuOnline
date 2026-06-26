using UnityEngine;
using TMPro;

public class MembersViewer : MonoBehaviour
{
    private TextMeshProUGUI _membersViewText;
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
