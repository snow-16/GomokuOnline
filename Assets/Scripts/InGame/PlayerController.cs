using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Sprite _black;
    [SerializeField]
    private Sprite _white;

    private GameObject _myStone;

    public StoneColor _myColor;

    void Awake()
    {
        ObjectAcceser.PlayerController = this;
        _myStone = transform.GetChild(0).gameObject;
        _myColor = PlayerData.Players[RoomData.OwnNumberIndex()].PlayerColor;
        _myStone.GetComponent<SpriteRenderer>().sprite = _myColor == StoneColor.Black ? _black : _white;

        BoardSelfData.ResetBoard();
    }

    void Update()
    {
        if(DataManager.InGameData == null)
        {
            return;
        }

        _myStone.SetActive(false);

        if(DataManager.InGameData.Turn == RoomData.Instance.PlayerNumber)
        {
            var overingPos = BoardUtil.PositionToCell(Camera.main.ScreenToWorldPoint(Mouse.current.position.value));
            if(overingPos != null && BoardSelfData.IsNone(overingPos.Value))
            {
                _myStone.SetActive(true);

                _myStone.transform.position = overingPos.Value;
                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    BoardController.Instance.DecisionPutStone(overingPos.Value, _myColor);
                }
            }
        }

        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 1)
        {
            DisconnectedSession();
        }
    }

    private async void DisconnectedSession()
    {
        await RelayManager.NetworkRunner.Shutdown();
        Destroy(RelayManager.NetworkRunner.gameObject);
        SceneManager.LoadScene("Title");
    }
}
