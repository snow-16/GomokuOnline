using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NetworkLinker : MonoBehaviour
{
    public static bool _joinedServer = false;
    
    void Awake()
    {
        if (DataManager.RoomData == null)
        {
            gameObject.AddComponent<BoardData>();
            gameObject.AddComponent<PlayerData>();
            gameObject.AddComponent<RoomData>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // サーバーとして起動しており、かつホスト（サーバー自身）以外のクライアントがいないかチェック
        if (NetworkManager.Singleton != null && NetworkManager.Singleton.IsServer)
        {
            // NetworkManager.Singleton.ConnectedClientsList にはサーバー自身も含まれます
            if (NetworkManager.Singleton.ConnectedClientsList.Count < 1)
            {
                // 無人状態と判断した後の処理（例：サーバーを閉じる）
                Debug.Log("シャットダウン");
                ShutdownServer();
            }
        }
    }

    // 特定のクライアントをキックする例
    public void KickClient(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.DisconnectClient(clientId);
            Debug.Log($"クライアント {clientId} をキックしました。");
        }
    }

    // サーバーを終了する
    private void ShutdownServer()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            NetworkManager.Singleton.Shutdown();
        }
    }
}
