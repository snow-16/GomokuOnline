/// <summary>
/// サーバー同期済みデータ取得用クラス
/// </summary>
public class DataManager
{
    /// <summary> プレイヤー用サーバーデータ </summary>
    public static PlayerDataManager PlayerData { get; set; }
    /// <summary> 部屋用サーバーデータ </summary>
    public static RoomDataManager RoomData { get; set; }
    /// <summary> インゲーム用サーバーデータ </summary>
    public static InGameDataManager InGameData { get; set; }
}
