/// <summary>
/// クライアント側でのプレイヤーデータ保存用クラス
/// </summary>
public class PlayerData
{
    /// <summary> プレイヤーごとの個別データ </summary>
    public static PlayerData[] Players { get; private set; } = new PlayerData[2];

    /// <summary>
    /// データ初期化
    /// </summary>
    public static void SetInstance()
    {
        Players[0] = new PlayerData();
        Players[1] = new PlayerData();
    }


    /// <summary> プレイヤー名 </summary>
    public string PlayerName { get; private set; }

    /// <summary> プレイヤーの石の割り当て色 </summary>
    public StoneColor PlayerColor { get; private set; }

    /// <summary> プレイヤーが接続されているか </summary>
    public bool IsExist { get; private set; }

    /// <summary>
    /// プレイヤーデータ更新
    /// </summary>
    public void UpdateData(string name, StoneColor color, bool exist)
    {
        PlayerName = name;
        PlayerColor = color;
        IsExist = exist;
    }

    /// <summary>
    /// プレイヤーデータ更新
    /// </summary>
    public void UpdateData(PlayerDataManager.AnPlayerData playerData)
    {
        UpdateData(playerData.PlayerName.Value, playerData.PlayerColor, playerData.IsExist);
    }
}
