using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// インゲームシーン内のUI統括クラス
/// </summary>
public class InGameUI : MonoBehaviour
{
    /// <summary> 黒色の石のスプライト </summary>
    [SerializeField]
    private Sprite _black;
    /// <summary> 白色の石のスプライト </summary>
    [SerializeField]
    private Sprite _white;

    /// <summary> 自ターン中のプレイヤーの持ち時間 </summary>
    private float _haveTime;

    /// <summary> ターン表示用テキストUI </summary>
    private TextMeshProUGUI _turnText;
    /// <summary> 持ち時間表示用テキストUI </summary>
    private TextMeshProUGUI _timeText;
    /// <summary> 自分の石の色表示用画像UI </summary>
    private Image _stoneImage;

    void Awake()
    {
        _turnText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        _timeText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _stoneImage = transform.GetChild(2).GetComponent<Image>();
        _stoneImage.sprite = PlayerData.Players[RoomData.OwnNumberIndex()].PlayerColor == StoneColor.Black ? _black : _white;

        _haveTime = 30;
    }

    void Update()
    {
        if(DataManager.InGameData == null)
        {
            return;
        }

        if(DataManager.InGameData.Turn == RoomData.OwnNumber())
        {
            if(!ObjectAcceser.PlayerController.WaitChangeTurn)
            {
                _haveTime -= Time.deltaTime;
            }

            if(_haveTime > 0)
            {
                _timeText.gameObject.SetActive(true);
                _timeText.text = $"持ち時間:残り{_haveTime:0}秒";
                _turnText.text = "あなたのターンです";
            }
            else
            {
                ObjectAcceser.PlayerController.DecisionPutStone(BoardUtil.RandomEmptyCell());
            }
        }
        else if(_timeText.gameObject.activeSelf)
        {
            _timeText.gameObject.SetActive(false);
            _haveTime = 30;
            _turnText.text = "相手のターンです";
        }
    }
}
