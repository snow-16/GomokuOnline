using System;
using Fusion;

public class PlayerDataManager : NetworkBehaviour
{
    [Networked]
    public AnPlayerData Own { get; private set; }
    [Networked]
    public AnPlayerData Opponents { get; private set; }

    public override void Spawned()
    {
        DataManager.PlayerData = this;

        Own = InitiationSetData(PlayerData.Own);
    }

    private AnPlayerData InitiationSetData(PlayerData setData)
    {
        var outputData = new AnPlayerData
        {
            PlayerName = setData.PlayerName,
            PlayerNumber = setData.PlayerNumber,
            PlayerColor = setData.PlayerColor,
            IsExist = setData.IsExist
        };

        return outputData;
    } 

    public void LeftPlayer()
    {
        Opponents = Opponents.ChangeIsExist(false);
    }

    public void TransferOwnToOne()
    {
        Own = Opponents;
        PlayerData.Own.UpdateData(Own);
    }

    public void ChangeName(int num, string name)
    {
        ChangeData(num, data => data.ChangeName(name));
    }

    public void ChangeData(int num, Func<AnPlayerData, AnPlayerData> changeDataFunc)
    {
        if(Own.PlayerNumber == num)
        {
            Own = changeDataFunc(Own);
        }
        else
        {
            Opponents = changeDataFunc(Opponents);
        }
    }

    public AnPlayerData GetDataByNumber(int num)
    {
        return Own.PlayerNumber == num ? Own : Opponents;
    }

    public AnPlayerData GetOpponentsDataByNumber(int num)
    {
        return GetDataByNumber((num % 2) + 1);
    }

    public struct AnPlayerData: INetworkStruct
    {
        public NetworkString<_16> PlayerName { get; set; }
        public int PlayerNumber { get; set; }
        public StoneColor PlayerColor { get; set; }
        public NetworkBool IsExist { get; set; }

        public AnPlayerData ChangeName(string name)
        {
            PlayerName = name;
            return this;
        }

        public AnPlayerData ChangeNumber(int num)
        {
            PlayerNumber = num;
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
