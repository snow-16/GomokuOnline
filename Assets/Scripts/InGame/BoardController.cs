using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject _stonePrefab;

    public static BoardController Instance { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }
    
    public void DecisionPutStone(Vector2 pos, StoneColor color)
    {
        RelayManager.NetworkRunner.Spawn(_stonePrefab, pos, Quaternion.identity, RelayManager.NetworkRunner.LocalPlayer,
        (runner, obj) =>
        {
            var stoneController = obj.GetComponent<StoneController>();
            stoneController.SetColor(color);
        });

        DataManager.InGameData.RPC_SwitchTurn();

        if(BoardUtil.FilledFive(pos, color))
        {
            Debug.Log("GOMOKU!!!");
        }
    }
}
