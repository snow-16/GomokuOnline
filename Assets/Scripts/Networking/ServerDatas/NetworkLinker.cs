using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class NetworkLinker : MonoBehaviour
{
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
}
