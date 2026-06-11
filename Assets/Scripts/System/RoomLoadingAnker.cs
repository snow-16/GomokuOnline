using System.Threading.Tasks;
using UnityEngine;

public class RoomLoadingAnker : LoadingAnker
{
    [SerializeField]
    private GameObject _playerDataManagerPrefab;
    [SerializeField]
    private GameObject _roomDataManagerPrefab;
    [SerializeField]
    private PlayerSettingUI _player1SettingUI;
    [SerializeField]
    private PlayerSettingUI _player2SettingUI;
    [SerializeField]
    private RoomCodeUI _roomCodeUI;

    protected override void WhenLoaded()
    {
        AwaitingInitiationLoad();
    }

    private async void AwaitingInitiationLoad()
    {
        await RelayManager.NetworkRunner.SpawnAsync(_playerDataManagerPrefab);
        await RelayManager.NetworkRunner.SpawnAsync(_roomDataManagerPrefab);

        _player1SettingUI.Initiation();
        _player2SettingUI.Initiation();
        _roomCodeUI.Initiation();
    }
}
