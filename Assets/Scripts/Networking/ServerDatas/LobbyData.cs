using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// サーバー上の各セッションデータ保管用クラス
/// </summary>
public class LobbyData : MonoBehaviour, INetworkRunnerCallbacks
{
    /// <summary> 全セッション情報のリスト </summary>
    public static List<SessionInfo> _sessionList;
    /// <summary> 全セッションの部屋コードのリスト </summary>
    public static List<string> _sessionPassList = new();
    /// <summary> 現在のサーバー接続人数 </summary>
    public static int _playerCount;
    /// <summary> ロビー内での滞在時間 </summary>
    public static float _stayingLobbyTime = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// セッションが更新されたらデータも更新
    /// 接続人数を20以下に抑えるため、接続人数が16人を超えたら切断する。
    /// </summary>
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
        
        _sessionList = sessionList;
        _sessionPassList.Clear();

        _playerCount = 0;
        foreach(var session in _sessionList)
        {
            _sessionPassList.Add(session.Name);
            _playerCount += session.PlayerCount;
        }
        
        if(_playerCount >= 16)
        {
            runner.Shutdown();
            SceneManager.LoadScene("Title");
        }
    }

    //不要
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){}

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player){}

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player){}

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player){}

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason){}

    void INetworkRunnerCallbacks.OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason){}

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token){}

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason){}

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message){}

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data){}

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress){}

    public void OnInput(NetworkRunner runner, NetworkInput input){}

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input){}

    void INetworkRunnerCallbacks.OnConnectedToServer(NetworkRunner runner){}

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data){}

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken){}

    public void OnSceneLoadDone(NetworkRunner runner){}

    public void OnSceneLoadStart(NetworkRunner runner){}
}
