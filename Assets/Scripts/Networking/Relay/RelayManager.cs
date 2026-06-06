using System.Text;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelayManager : MonoBehaviour
{

    public static NetworkRunner NetworkRunner { get; private set;}

    void Update()
    {
        if(RoomData._stayingLobbyTime > 0)
        {
            RoomData._stayingLobbyTime -= Time.deltaTime;

            if(RoomData._stayingLobbyTime <= 0)
            {
                RoomData._stayingLobbyTime = 0;
                NetworkRunner.Shutdown();
                SceneManager.LoadScene("Title");
            }
        }
    }

    public static async Task<int> JoinMatch(string code)
    {
        RoomData._stayingLobbyTime = 0;

        await NetworkRunner.StartGame(new StartGameArgs {
            GameMode = GameMode.Shared,
            SessionName = code,
            PlayerCount = 2,
            CustomLobbyName = NetworkRunner.LobbyInfo.Name,
            IsVisible = true,
            IsOpen = true
        });
        
        return NetworkRunner.SessionInfo.PlayerCount;
    }

    public static void CreateRunner(NetworkRunner runnerPrefab)
    {
        NetworkRunner = Instantiate(runnerPrefab);
    }

    public static async void JoinLobby()
    {
        await NetworkRunner.JoinSessionLobby(SessionLobby.Custom, "Lobby");
        RoomData._stayingLobbyTime = 30;
        SceneManager.LoadScene("Lobby");
    }

    public static async void CreateRoom(string code)
    {
        await JoinMatch(code);
        await NetworkRunner.LoadScene("Room");
    }

    public static async void CreateRoom()
    {
        string sessionCode = null;

        while(sessionCode == null || RoomData._sessionPassList.Contains(sessionCode))
        {
            var codeBuilder = new StringBuilder();
            var randomizer = new System.Random();

            for(int i = 0; i < 6; i++)
            {
                codeBuilder.Append(char.ConvertFromUtf32(randomizer.Next('A', 'Z' + 1)));
            }

            sessionCode = codeBuilder.ToString();
        }

        await JoinMatch(sessionCode);
        await NetworkRunner.LoadScene("Room");
    }

    public static async void JoinRoom(string code)
    {
        await JoinMatch(code);
        await NetworkRunner.LoadScene("Room");
    }
}
