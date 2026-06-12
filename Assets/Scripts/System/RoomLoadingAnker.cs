using System.Threading.Tasks;
using Fusion;
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
        RPC_AwaitingInitiationLoad();
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private async void RPC_AwaitingInitiationLoad()
    {
        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 1)
        {
            await RelayManager.NetworkRunner.SpawnAsync(_playerDataManagerPrefab);
            await RelayManager.NetworkRunner.SpawnAsync(_roomDataManagerPrefab);
        }
    }

    void Update()
    {
        if(DataManager.PlayerData != null && DataManager.RoomData != null)
        {
            _player1SettingUI.Initiation();
            _player2SettingUI.Initiation();
            _roomCodeUI.Initiation();

            Destroy(gameObject);
        }
    }
}
