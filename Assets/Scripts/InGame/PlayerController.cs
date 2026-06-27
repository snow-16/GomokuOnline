using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

/// <summary>
/// プレイヤー側の操作・勝敗表示クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary> 黒色の石のスプライト </summary>
    [SerializeField]
    private Sprite _black;
    /// <summary> 白色の石のスプライト </summary>
    [SerializeField]
    private Sprite _white;
    /// <summary> 勝敗表示用UI </summary>
    [SerializeField]
    private GameObject _resultUI;
    /// <summary> 接続切断を伝えるUI </summary>
    [SerializeField]
    private GameObject _disconnectedUI;

    /// <summary> 石の設置石目安用の飾りオブジェクト </summary>
    private GameObject _myStone;
    /// <summary> 自身の石の割り当て色 </summary>
    private StoneColor _myColor;

    /// <summary> 現在、石の設置後のターン切り替え待機中であるか </summary>
    public bool WaitChangeTurn { get; private set; } = false;

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

    /// <summary>
    /// 石の設置位置確定。
    /// 待機状態へ移行する
    /// </summary>
    /// <param name="pos"></param>
    public void DecisionPutStone(Vector2 pos)
    {
        BoardController.Instance.DecisionPutStone(pos, _myColor);
        WaitChangeTurn = true;
    }
}
