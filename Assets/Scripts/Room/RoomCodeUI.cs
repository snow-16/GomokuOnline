using UnityEngine;
using UnityEngine.UI;

public class RoomCodeUI : MonoBehaviour
{
    private Text _roomCodeText;

    public void Initiation()
    {
        _roomCodeText = GetComponent<Text>();
        _roomCodeText.text = DataManager.RoomData.RoomCode.Value;
    }
}
