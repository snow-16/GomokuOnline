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

        var outputData = new AnPlayerData().ChangeName(setData.PlayerName).ChangeColor(color).ChangeIsExist(setData.IsExist);

        return outputData;
    } 

    public void LeftPlayer()
    {
        RPC_SetPlayer(RoomData.OpponentsNumberIndex(), Players[RoomData.OpponentsNumberIndex()].ChangeIsExist(false));
    }

    public void TransferOwnToOne()
    {
        RPC_SetPlayer(0, Players[RoomData.OwnNumberIndex()]);
        RoomData.Instance.SwitchToOne();
    }

    public void ChangeName(int num, string name)
    {
        ChangeData(num - 1, data => data.ChangeName(name));
    }

    public void ChangeColor()
    {
        if(RelayManager.NetworkRunner.SessionInfo.PlayerCount == 2)
        {
            ChangeData(1, data => data.ChangeColor(Players[0].PlayerColor));
        }
        
        ChangeData(0, data => data.ChangeColor((StoneColor)(((int)Players[0].PlayerColor + 1) % 2)));
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
        public NetworkString<_16> PlayerName { get; private set; }
        public StoneColor PlayerColor { get; private set; }
        public NetworkBool IsExist { get; private set; }

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
