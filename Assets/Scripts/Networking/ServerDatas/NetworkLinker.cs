using Fusion;
using UnityEngine;

/// <summary>
/// 各種データの初期化・NetworkRunner生成用クラス
/// </summary>
[DefaultExecutionOrder(-100)]
public class NetworkLinker : MonoBehaviour
{
    /// <summary> NetworkRunnerをアタッチしたプレハブ </summary>
    [SerializeField]
    private NetworkRunner _networkRunnerPrefab;
    
    void Awake()
    {
        if (FindObjectsByType<NetworkLinker>(FindObjectsSortMode.None).Length == 1)
        {
            RoomData.SetInstance();
            PlayerData.SetInstance();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        RelayManager.CreateRunner(_networkRunnerPrefab);
    }
}
