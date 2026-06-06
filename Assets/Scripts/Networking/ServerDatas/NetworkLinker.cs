using Fusion;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NetworkLinker : MonoBehaviour
{
    [SerializeField]
    private NetworkRunner _networkRunnerPrefab;

    public static bool _joinedServer = false;
    
    void Awake()
    {
        if (FindAnyObjectByType<NetworkLinker>())
        {
            DataManager.BoardData = gameObject.AddComponent<BoardData>();
            DataManager.PlayerData = gameObject.AddComponent<PlayerData>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        RelayManager.CreateRunner(_networkRunnerPrefab);
    }
}
