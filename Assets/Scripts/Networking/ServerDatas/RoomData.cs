/// <summary>
/// クライアント側での各セッション固有のデータ保管用クラス
/// </summary>
public class RoomData
{
    /// <summary> データへのアクセス用プロパティ </summary>
    public static RoomData Instance { get; private set; }

    /// <summary>
    /// Instanceを初期化
    /// </summary>
    public static void SetInstance()
    {
        Instance = new RoomData();
    }

    /// <summary>
    /// 自分のプレイヤー番号を所得
    /// </summary>
    public static int OwnNumber()
    {
        return Instance.PlayerNumber;
    }

    /// <summary>
    /// 相手のプレイヤー番号を取得
    /// </summary>
    /// <returns></returns>
    public static int OpponentsNumber()
    {
        return (Instance.PlayerNumber % 2) + 1;
    }

    /// <summary>
    /// 自分のプレイヤー番号をインデックスに変換して取得
    /// </summary>
    /// <returns></returns>
    public static int OwnNumberIndex()
    {
        return OwnNumber() - 1;
    }

    /// <summary>
    /// 相手のプレイヤー番号をインデックスに変換して取得
    /// </summary>
    /// <returns></returns>
    public static int OpponentsNumberIndex()
    {
        return OpponentsNumber() - 1;
    }



    /// <summary> 部屋コード </summary>
    public string RoomCode { get; private set; }
    /// <summary> 自分のプレイヤー番号 </summary>
    public int PlayerNumber { get; private set; }
    /// <summary> 現在のターン </summary>
    public int Turn { get; private set; }
    /// <summary> 対戦の勝者 </summary>
    public StoneColor Winner { get; private set; }

    /// <summary>
    /// データの更新
    /// </summary>
    /// <param name="code">部屋コード</param>
    /// <param name="num">自分のプレイヤー番号</param>
    /// <param name="turn">現在のターン</param>
    /// <param name="winner">対戦の勝者</param>
    public void UpdateData(string code, int num, int turn, StoneColor winner)
    {
        RoomCode = code;
        Turn = turn;
        PlayerNumber = num;
        Winner = winner;
    }

    /// <summary>
    /// 自分のプレイヤ番号を1に変更
    /// </summary>
    public void SwitchToOne()
    {
        PlayerNumber = 1;
    }
}
