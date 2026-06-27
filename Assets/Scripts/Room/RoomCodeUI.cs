using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomCodeUI : MonoBehaviour
{
    private TextMeshProUGUI _roomCodeText;

    public void Initiation()
    {
        _roomCodeText = GetComponent<TextMeshProUGUI>();
        _roomCodeText.text = DataManager.RoomData.RoomCode.Value;
    }
}
