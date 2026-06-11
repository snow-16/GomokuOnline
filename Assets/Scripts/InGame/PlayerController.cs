using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameObject _myStone;

    public int PlayerNo { get; private set; }
    public int Turn { get; private set; }

    public Vector2 OveringCellPos { get; set; }

    void Awake()
    {
        ObjectAcceser.PlayerController = this;
        _myStone = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if(Turn == PlayerNo)
        {
            var overingPos = GomokuStandUtil.PositionToCell(Camera.main.ScreenToWorldPoint(Mouse.current.position.value));
            if(overingPos != null)
            {
                _myStone.SetActive(true);

                _myStone.transform.position = overingPos.Value;
                if(Mouse.current.leftButton.wasPressedThisFrame)
                {
                    DecisionPutStone(overingPos.Value);
                }
            }
            else
            {
                _myStone.SetActive(false);
            }
        }
    }

    public void DecisionPutStone(Vector2 pos)
    {
        // BoardSender.SetCell(pos, (StoneColor)(PlayerNo - 1));
    }

    public void SetTurn(int turn)
    {
        Turn = turn;
    }

    public void SetNo(int no)
    {
        PlayerNo = no;
        _myStone.GetComponent<StoneController>().SetColor((StoneColor)(PlayerNo - 1));
    }
}
