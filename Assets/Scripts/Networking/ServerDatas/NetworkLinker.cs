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
            DataManager.BoardData = gameObject.AddComponent<BoardData>();
            DataManager.PlayerData = gameObject.AddComponent<PlayerData>();
            DataManager.RoomData = gameObject.AddComponent<RoomData>();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
