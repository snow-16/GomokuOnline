using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RelayManager : MonoBehaviour
{
    public static NetworkRunner NetworkRunner { get; private set;}

    public static async Task<int> JoinMatch(string code)
    {
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

    public static async void JoinLobby()
    {
        await NetworkRunner.JoinSessionLobby(SessionLobby.Custom, "Lobby");
        SceneManager.LoadScene("Lobby");
    }

    public static void CreateRunner(NetworkRunner runnerPrefab)
    {
        NetworkRunner = Instantiate(runnerPrefab);
    }
}
