using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// プレイヤーデータ表示・設定UI用クラス
/// </summary>
public class PlayerSettingUI: MonoBehaviour
{
    /// <summary> データを表示するプレイヤーの番号 </summary>
    [SerializeField]
    private int _showPlayerNumber;
    /// <summary> 石のスプライト </summary>
    [SerializeField]
    private Sprite[] _stoneSprities;

    /// <summary> サーバープレイヤーデータの保持用 </summary>
    private PlayerDataManager _playerData;

    /// <summary> 接続待機テキストUI </summary>
    private GameObject _waitingText;
    /// <summary> プレイヤーデータ表示用UI </summary>
    private GameObject _playerSettingsObj;
    /// <summary> プレイヤー名入力フィールド </summary>
    private TMP_InputField _playerNameField;
    /// <summary> プレイヤーの石表示用画像UI </summary>
    private Image _playerColorImage;

    /// <summary> プレイヤーの石の割り当て色 </summary>
    private StoneColor _myColor;

    /// <summary>
    /// 各データの初期化
    /// </summary>
    public void Initiation()
    {
        _playerData = DataManager.PlayerData;

        _playerSettingsObj = transform.GetChild(1).gameObject;
        _playerNameField = _playerSettingsObj.transform.GetChild(0).GetComponent<TMP_InputField>();
        _playerColorImage = _playerSettingsObj.transform.GetChild(1).GetComponent<Image>();
        _myColor = _playerData.GetDataByNumber(_showPlayerNumber).PlayerColor;
        _playerColorImage.sprite = _stoneSprities[(int)_myColor];

        if(_showPlayerNumber == 2)
        {
            _waitingText = transform.GetChild(2).gameObject;
        }

        if(_showPlayerNumber == RoomData.Instance.PlayerNumber)
        {
            _playerNameField.text = PlayerData.Players[RoomData.OwnNumberIndex()].PlayerName;
        }
    }

    async void Update()
    {
        if(!RelayManager.NetworkRunner.IsRunning || _playerData == null || DataManager.PlayerData == null)
        {
            return;
        }
        
        var playerSetting = _playerData.GetDataByNumber(_showPlayerNumber);

        if(playerSetting.IsExist && _playerNameField.text != playerSetting.PlayerName)
        {
            if(_showPlayerNumber == RoomData.Instance.PlayerNumber)
            {
                _playerData.ChangeName(_showPlayerNumber, _playerNameField.text);
            }
            else
            {
                _playerNameField.text = playerSetting.PlayerName.Value;
            }
        }

        if(_myColor != playerSetting.PlayerColor)
        {
            _myColor = playerSetting.PlayerColor;
            _playerColorImage.sprite = _stoneSprities[(int)_myColor];
        }

        if(_showPlayerNumber == 2)
        {
            if(playerSetting.IsExist && !_playerSettingsObj.activeSelf)
            {
                _waitingText.SetActive(false);
                _playerSettingsObj.SetActive(true);
            }
            else if(!playerSetting.IsExist && _playerSettingsObj.activeSelf)
            {
                _waitingText.SetActive(true);
                _playerSettingsObj.SetActive(false);
            }
        }

        if(_showPlayerNumber != RoomData.Instance.PlayerNumber)
        {
            _playerNameField.interactable = false;
        }
        else
        {
            _playerNameField.interactable = true;
        }

        if(_playerData.GetOpponentsDataByNumber(RoomData.Instance.PlayerNumber).IsExist && RelayManager.NetworkRunner.SessionInfo.PlayerCount == 1)
        {
            if(RoomData.Instance.PlayerNumber == 2)
            {
                _playerData.Object.RequestStateAuthority();
                DataManager.RoomData.Object.ReleaseStateAuthority();
                while (_playerData.HasStateAuthority == false)
                {
                    await System.Threading.Tasks.Task.Delay(100); 
                }
                _playerData.TransferOwnToOne();
            }

            _playerData.LeftPlayer();
        }
    }
}
