using UnityEngine;
using UnityEngine.UI;

public class MembersViewer : MonoBehaviour
{
    private Text _membersViewText;
    private int _memberCount = 0;

    void Start()
    {
        _membersViewText = GetComponent<Text>();
    }

    void Update()
    {
        if(_memberCount != LobbyData._playerCount)
        {
            _membersViewText.text = $"残り接続可能人数: {_memberCount = 16 - LobbyData._playerCount}人";
        }
    }
}
