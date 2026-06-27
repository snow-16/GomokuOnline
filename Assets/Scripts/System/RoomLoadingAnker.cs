using Fusion;
using UnityEngine;

/// <summary>
/// ルームシーンロード完了感知用クラス
/// </summary>
public class RoomLoadingAnker : LoadingAnker
{
    /// <summary> プレイヤーサーバーデータ同期用プレハブ </summary>
    [SerializeField]
    private GameObject _playerDataManagerPrefab;
    /// <summary> ルームサーバーデータ同期用プレハブ </summary>
    [SerializeField]
    private GameObject _roomDataManagerPrefab;
    /// <summary> プレイヤー1のデータ表示用UI </summary>
    [SerializeField]
    private PlayerSettingUI _player1SettingUI;
    /// <summary> プレイヤー2のデータ表示用UI </summary>
    [SerializeField]
    private PlayerSettingUI _player2SettingUI;
    /// <summary> 部屋コード表示用UI </summary>
    [SerializeField]
    private RoomCodeUI _roomCodeUI;

    protected override void WhenLoaded()
    {
        RPC_AwaitingInitiationLoad();
    }

    /// <summary>
    /// サーバーデータ同期用オブジェクト生成
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    private async void RPC_AwaitingInitiationLoad()
    {
        if(RoomData.Instance.PlayerNumber == 1)
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
