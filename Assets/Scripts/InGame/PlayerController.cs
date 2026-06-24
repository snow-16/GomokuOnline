using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameObject _myStone;

    public StoneColor _myColor;

    void Awake()
    {
        ObjectAcceser.PlayerController = this;
        _myStone = transform.GetChild(0).gameObject;
        // _myColor = PlayerData.Players[RoomData.OwnNumberIndex()].PlayerColor;
        _myColor = StoneColor.Black;

        BoardSelfData.ResetBoard();
    }

    void Update()
    {
        var overingPos = BoardUtil.PositionToCell(Camera.main.ScreenToWorldPoint(Mouse.current.position.value));
        if(overingPos != null)
        {
            if(BoardSelfData.IsNone(overingPos.Value))
            {
                _myStone.SetActive(true);

                _myStone.transform.position = overingPos.Value;
                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    BoardController.Instance.DecisionPutStone(overingPos.Value, _myColor);
                }
            }
            else
            {
                _myStone.SetActive(false);
            }
        }
        else
        {
            _myStone.SetActive(false);
        }
    }
}
