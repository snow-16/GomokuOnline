using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Sprite _black;
    [SerializeField]
    private Sprite _white;
    [SerializeField]
    private GameObject _resultUI;
    [SerializeField]
    private GameObject _disconnectedUI;

    private GameObject _myStone;
    private StoneColor _myColor;

    public bool WaitChangeTurn { get; private set; } = false;

    public static PlayerController Instance { get; private set; }

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
        
        if(DataManager.InGameData.Winner == StoneColor.None && !BoardSelfData.FullyCells())
        {
            if(!WaitChangeTurn)
            {
                if(DataManager.InGameData.Turn == RoomData.Instance.PlayerNumber)
                {
                    var overingPos = BoardUtil.PositionToCell(Camera.main.ScreenToWorldPoint(Mouse.current.position.value));
                    if(overingPos != null && BoardSelfData.IsNone(overingPos.Value))
                    {
                        _myStone.SetActive(true);

                        _myStone.transform.position = overingPos.Value;
                        if(Mouse.current.leftButton.wasPressedThisFrame)
                        {
                            DecisionPutStone(overingPos.Value);
                        }
                    }
                }
            }
            else if(DataManager.InGameData.Turn != RoomData.Instance.PlayerNumber)
            {
                WaitChangeTurn = false;
            }
        }
        else if(!_resultUI.activeSelf)
        {
            _resultUI.SetActive(true);
            string resultText;
            if(DataManager.InGameData.Winner != StoneColor.None)
            {
                resultText = $"{(DataManager.InGameData.Winner == StoneColor.Black ? "黒" : "白")}:{(PlayerData.Players[0].PlayerColor == DataManager.InGameData.Winner ? PlayerData.Players[0].PlayerName : PlayerData.Players[1].PlayerName)} の勝ち";
            }
            else
            {
                resultText = "引き分け";
            }

            _resultUI.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = resultText;
        }

        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 1)
        {
            DataManager.InGameData = null;
            _disconnectedUI.SetActive(true);
        }
    }

    public void DecisionPutStone(Vector2 pos)
    {
        BoardController.Instance.DecisionPutStone(pos, _myColor);
        WaitChangeTurn = true;
    }
}
