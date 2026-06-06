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
        if(_memberCount != RoomData._playerCount)
        {
            _membersViewText.text = $"残り接続可能人数: {_memberCount = 18 - RoomData._playerCount}人";
        }
    }
}
