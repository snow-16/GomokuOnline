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
        var stone = Instantiate(_stonePrefab).GetComponent<StoneController>();
        stone.enabled = true;
        stone.SetColor(color);
        stone.transform.position = pos;

        if(BoardUtil.FilledFive(pos, color))
        {
            Debug.Log("GOMOKU!!!");
        }
    }
}
