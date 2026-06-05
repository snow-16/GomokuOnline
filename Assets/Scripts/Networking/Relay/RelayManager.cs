using System.Threading.Tasks;
using Fusion;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    public static async Task<bool> JoinMatch(NetworkRunner runnerPrefab, string code)
    {
        var networkRunner = Instantiate(runnerPrefab);
        
        // 共有モードのセッションに参加する．同じパスワードを入力した人同士でしかマッチングしない．
        var result = await networkRunner.StartGame(new StartGameArgs {
            GameMode = GameMode.Shared,
            SessionName = code,
            PlayerCount = 4,
            IsVisible = false
        });
        
        return result.Ok;
    }
}
