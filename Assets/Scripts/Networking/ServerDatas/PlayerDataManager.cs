using System;
using Fusion;
using UnityEngine;

public class PlayerDataManager : NetworkBehaviour
{
    [Networked, Capacity(2)]
    public NetworkArray<AnPlayerData> Players { get; }

    public override void Spawned()
    {
        RPC_SetPlayer(RoomData.OwnNumberIndex(), InitiationSetData(PlayerData.Players[RoomData.OwnNumberIndex()]));

        DataManager.PlayerData = this;
    }

    private AnPlayerData InitiationSetData(PlayerData setData)
    {
        var color = RoomData.OwnNumber() == 2 ? (StoneColor)(((int)Players[0].PlayerColor + 1) % 2) : setData.PlayerColor;

        var outputData = new AnPlayerData
        {
            PlayerName = setData.PlayerName,
            PlayerColor = color,
            IsExist = setData.IsExist
        };

        return outputData;
    } 

    public void LeftPlayer()
    {
        RPC_SetPlayer(RoomData.OpponentsNumberIndex(), Players[RoomData.OpponentsNumberIndex()].ChangeIsExist(false));
    }

    public void TransferOwnToOne()
    {
        RPC_SetPlayer(RoomData.OwnNumberIndex(), Players[RoomData.OpponentsNumberIndex()].ChangeIsExist(false));
        PlayerData.Players[RoomData.OpponentsNumberIndex()].UpdateData(Players[RoomData.OpponentsNumberIndex()]);
    }

    public void ChangeName(int num, string name)
    {
        ChangeData(num - 1, data => data.ChangeName(name));
    }

    public void ChangeColor()
    {
        var playerOneColor = Players[0].PlayerColor;
        ChangeData(0, data => data.ChangeColor(Players[1].PlayerColor));
        ChangeData(1, data => data.ChangeColor(playerOneColor));
    }

    public void ChangeData(int index, Func<AnPlayerData, AnPlayerData> changeDataFunc)
    {
        RPC_SetPlayer(index, changeDataFunc(Players[index]));
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_SetPlayer(int index, AnPlayerData playerData)
    {
        Players.Set(index, playerData);
    }



    public AnPlayerData GetDataByNumber(int num)
    {
        return Players[num - 1];
    }

    public AnPlayerData GetOpponentsDataByNumber(int num)
    {
        return GetDataByNumber((num % 2) + 1);
    }

    public struct AnPlayerData: INetworkStruct
    {
        public NetworkString<_16> PlayerName { get; set; }
        public StoneColor PlayerColor { get; set; }
        public NetworkBool IsExist { get; set; }

        public AnPlayerData ChangeName(string name)
        {
            PlayerName = name;
            return this;
        }

        public AnPlayerData ChangeColor(StoneColor color)
        {
            PlayerColor = color;
            return this;
        }

        public AnPlayerData ChangeIsExist(bool exist)
        {
            IsExist = exist;
            return this;
        }
    }
}
