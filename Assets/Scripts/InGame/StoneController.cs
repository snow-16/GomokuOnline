using UnityEngine;

public class StoneController : MonoBehaviour
{
    [SerializeField]
    private Sprite _black;
    [SerializeField]
    private Sprite _white;

    public void SetColor(StoneColor color)
    {
        GetComponent<SpriteRenderer>().sprite = color == StoneColor.Black ? _black : _white;
    }
}
