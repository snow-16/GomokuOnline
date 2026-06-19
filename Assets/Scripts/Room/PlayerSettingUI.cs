using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettingUI: MonoBehaviour
{
    [SerializeField]
    private int _showPlayerNumber;
    [SerializeField]
    private Sprite[] _stoneSprities;

    private PlayerDataManager _playerData;

    private GameObject _waitingText;
    private GameObject _playerSettingsObj;
    private InputField _playerNameField;
    private Image _playerColorImage;

    private StoneColor _myColor;

    public void Initiation()
    {
        _playerData = DataManager.PlayerData;

        _playerSettingsObj = transform.GetChild(1).gameObject;
        _playerNameField = _playerSettingsObj.transform.GetChild(0).GetComponent<InputField>();
        _playerColorImage = _playerSettingsObj.transform.GetChild(1).GetComponent<Image>();
        _myColor = _playerData.GetDataByNumber(_showPlayerNumber).PlayerColor;
        _playerColorImage.sprite = _stoneSprities[(int)_myColor];

        if(_showPlayerNumber == 2)
        {
            _waitingText = transform.GetChild(2).gameObject;
        }
    }

    async void Update()
    {
        if(!RelayManager.NetworkRunner.IsRunning || _playerData == null)
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
