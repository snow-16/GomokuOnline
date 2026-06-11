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
