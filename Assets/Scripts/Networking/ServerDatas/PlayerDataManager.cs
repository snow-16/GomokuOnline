using System;
using Fusion;

/// <summary>
/// プレイヤーデータ同期用クラス
/// </summary>
public class PlayerDataManager : NetworkBehaviour
{
    /// <summary> クライアント側へのデータ保存が完了したクライアントの数 </summary>
    [Networked]
    public int SaveFinished { get; private set; }
    /// <summary> プレイヤー個別のデータ </summary>
    [Networked, Capacity(2)]
    public NetworkArray<AnPlayerData> Players { get; }

    public override void Spawned()
    {
        RPC_SetPlayer(RoomData.OwnNumberIndex(), InitiationSetData(PlayerData.Players[RoomData.OwnNumberIndex()]));

        DataManager.PlayerData = this;
        SaveFinished = 0;
    }

    /// <summary>
    /// プレイヤーデータをローカルデータからコピー
    /// </summary>
    /// <param name="setData">ローカルデータ</param>
    /// <returns></returns>
    private AnPlayerData InitiationSetData(PlayerData setData)
    {
        var color = RoomData.OwnNumber() == 2 ? (StoneColor)(((int)Players[0].PlayerColor + 1) % 2) : setData.PlayerColor;

        var outputData = new AnPlayerData().ChangeName(setData.PlayerName).ChangeColor(color).ChangeIsExist(setData.IsExist);

        return outputData;
    } 

    /// <summary>
    /// プレイヤーの退室処理。
    /// プレイヤー2のデータをリセットする
    /// </summary>
    public void LeftPlayer()
    {
        RPC_SetPlayer(RoomData.OpponentsNumberIndex(), Players[RoomData.OpponentsNumberIndex()].ChangeIsExist(false));
    }

    /// <summary>
    /// プレイヤーデータをプレイヤー1の枠へ移行する
    /// </summary>
    public void TransferOwnToOne()
    {
        RPC_SetPlayer(0, Players[RoomData.OwnNumberIndex()]);
        RoomData.Instance.SwitchToOne();
    }

    /// <summary>
    /// プレイヤー名変更
    /// </summary>
    /// <param name="num">変更するプレイヤーの番号</param>
    /// <param name="name">変更先の名前</param>
    public void ChangeName(int num, string name)
    {
        ChangeData(num - 1, data => data.ChangeName(name));
    }

    /// <summary>
    /// プレイヤーの石の割り当て色切り替え
    /// </summary>
    public void ChangeColor()
    {
        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 2)
        {
            ChangeData(1, data => data.ChangeColor(Players[0].PlayerColor));
        }
        
        ChangeData(0, data => data.ChangeColor((StoneColor)(((int)Players[0].PlayerColor + 1) % 2)));
    }

    /// <summary>
    /// プレイヤーデータの変更
    /// </summary>
    /// <param name="index">変更するプレイヤーのインデックス</param>
    /// <param name="changeDataFunc">データ変更関数</param>
    public void ChangeData(int index, Func<AnPlayerData, AnPlayerData> changeDataFunc)
    {
        RPC_SetPlayer(index, changeDataFunc(Players[index]));
    }

    /// <summary>
    /// プレイヤーデータ変更・ネットワーク同期
    /// </summary>
    /// <param name="index">変更するプレイヤーのインデックス</param>
    /// <param name="playerData">変更先のデータ</param>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetPlayer(int index, AnPlayerData playerData)
    {
        Players.Set(index, playerData);
    }

    /// <summary>
    /// プレイヤーデータをローカルデータに保存
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SaveData()
    {
        PlayerData.Players[0].UpdateData(Players[0]);
        PlayerData.Players[1].UpdateData(Players[1]);
        DataManager.PlayerData = null;

        RPC_EndSave();
    }

    /// <summary>
    /// データ保存の完了を通達。
    /// 全て完了すればインゲームシーンへ移行
    /// </summary>
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_EndSave()
    {
        SaveFinished++;

        if(SaveFinished == 2)
        {
            RelayManager.NetworkRunner.LoadScene("InGame");
        }
    }

    /// <summary>
    /// プレイヤー番号からプレイヤーデータを取得
    /// </summary>
    /// <param name="num">プレイヤー番号</param>
    /// <returns></returns>
    public AnPlayerData GetDataByNumber(int num)
    {
        return Players[num - 1];
    }

    /// <summary>
    /// プレイヤー番号から相手プレイヤーのデータを取得
    /// </summary>
    /// <param name="num">プレイヤー番号</param>
    /// <returns></returns>
    public AnPlayerData GetOpponentsDataByNumber(int num)
    {
        return GetDataByNumber((num % 2) + 1);
    }

    /// <summary>
    /// プレイヤーデータ用構造体
    /// </summary>
    public struct AnPlayerData: INetworkStruct
    {
        /// <summary> プレイヤー名 </summary>
        public NetworkString<_16> PlayerName { get; private set; }
        /// <summary> プレイヤーの石の割り当て色 </summary>
        public StoneColor PlayerColor { get; private set; }
        /// <summary> プレイヤーが接続されているか </summary>
        public NetworkBool IsExist { get; private set; }

        /// <summary>
        /// プレイヤー名変更
        /// </summary>
        public AnPlayerData ChangeName(string name)
        {
            PlayerName = name;
            return this;
        }

        /// <summary>
        /// プレイヤーの石の割り当て色変更
        /// </summary>
        /// <param name="color">変更先の色</param>
        /// <returns></returns>
        public AnPlayerData ChangeColor(StoneColor color)
        {
            PlayerColor = color;
            return this;
        }

        /// <summary>
        /// プレイヤーの接続状態変更
        /// </summary>
        /// <param name="exist">該当プレイヤーが接続されているか</param>
        /// <returns></returns>
        public AnPlayerData ChangeIsExist(bool exist)
        {
            IsExist = exist;
            return this;
        }
    }
}
