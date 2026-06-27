using System.Text;
using System.Threading.Tasks;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// サーバー接続・セッション入退室管理用クラス
/// </summary>
public class RelayManager : MonoBehaviour
{
    /// <summary> 生成されたNetworkRunner取得用プロパティ </summary>
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

    /// <summary>
    /// セッションへの参加
    /// </summary>
    /// <param name="code">部屋コード</param>
    /// <returns></returns>
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

    /// <summary>
    /// NetworkRunner生成
    /// </summary>
    /// <param name="runnerPrefab">NetworkRunnerがアタッチされたプレハブ</param>
    public static void CreateRunner(NetworkRunner runnerPrefab)
    {
        NetworkRunner = Instantiate(runnerPrefab);
    }

    /// <summary>
    /// ロビーへの参加
    /// </summary>
    public static async void JoinLobby()
    {
        await NetworkRunner.JoinSessionLobby(SessionLobby.Custom, "Lobby");
        LobbyData._stayingLobbyTime = 30;
        SceneManager.LoadScene("Lobby");
    }

    /// <summary>
    /// コードを指定して部屋を立てる
    /// </summary>
    /// <param name="code">指定するコード</param>
    public static async void CreateRoom(string code)
    {
        await JoinMatch(code);

        UpdateData();

        await NetworkRunner.LoadScene("Room");
    }

    /// <summary>
    /// ランダムなコードで部屋を立てる
    /// </summary>
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

    /// <summary>
    /// 立てられた部屋への入室
    /// </summary>
    /// <param name="code">対象の部屋のコード</param>
    public static async void JoinRoom(string code)
    {
        await JoinMatch(code);

        UpdateData();
    }

    /// <summary>
    /// 各データの初期化
    /// </summary>
    private static void UpdateData()
    {
        RoomData.Instance.UpdateData(NetworkRunner.SessionInfo.Name, NetworkRunner.SessionInfo.PlayerCount, 1, StoneColor.None);
        PlayerData.Players[RoomData.OwnNumberIndex()].UpdateData("NoName", (StoneColor)RoomData.OwnNumberIndex(), true);
    }
}
