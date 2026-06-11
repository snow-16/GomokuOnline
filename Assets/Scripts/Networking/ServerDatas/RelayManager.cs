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
        if(LobbyData._stayingLobbyTime > 0)
        {
            LobbyData._stayingLobbyTime -= Time.deltaTime;

            if(LobbyData._stayingLobbyTime <= 0)
            {
                LobbyData._stayingLobbyTime = 0;
                NetworkRunner.Shutdown();
                SceneManager.LoadScene("Title");
            }
        }
    }

    public static async Task JoinMatch(string code)
    {
        LobbyData._stayingLobbyTime = 0;

        await NetworkRunner.StartGame(new StartGameArgs {
            GameMode = GameMode.Shared,
            SessionName = code,
            PlayerCount = 2,
            CustomLobbyName = NetworkRunner.LobbyInfo.Name,
            Scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex),
            IsVisible = true,
            IsOpen = true
        });
    }

    public static void CreateRunner(NetworkRunner runnerPrefab)
    {
        NetworkRunner = Instantiate(runnerPrefab);
    }

    public static async void JoinLobby()
    {
        await NetworkRunner.JoinSessionLobby(SessionLobby.Custom, "Lobby");
        LobbyData._stayingLobbyTime = 30;
        SceneManager.LoadScene("Lobby");
    }

    public static async void CreateRoom(string code)
    {
        await JoinMatch(code);

        RoomData.Instance.UpdateData(NetworkRunner.SessionInfo.Name, 1);
        PlayerData.Own.UpdateData("NoName", NetworkRunner.SessionInfo.PlayerCount, (StoneColor)(NetworkRunner.SessionInfo.PlayerCount - 1), true);

        await NetworkRunner.LoadScene("Room");
    }

    public static void CreateRoom()
    {
        string sessionCode = null;

        while(sessionCode == null || LobbyData._sessionPassList.Contains(sessionCode))
        {
            var codeBuilder = new StringBuilder();
            var randomizer = new System.Random();

            for(int i = 0; i < 6; i++)
            {
                codeBuilder.Append(char.ConvertFromUtf32(randomizer.Next('A', 'Z' + 1)));
            }

            sessionCode = codeBuilder.ToString();
        }

        CreateRoom(sessionCode);
    }

    public static async void JoinRoom(string code)
    {
        await JoinMatch(code);
        await NetworkRunner.LoadScene("Room");
    }
}
