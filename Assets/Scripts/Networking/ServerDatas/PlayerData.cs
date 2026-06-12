using UnityEngine;

public class PlayerData
{
    public static PlayerData[] Players { get; private set; } = new PlayerData[2];

    public static void SetInstance()
    {
        Players[0] = new PlayerData();
        Players[1] = new PlayerData();
    }



    public string PlayerName { get; private set; }

    public StoneColor PlayerColor { get; private set; }

    public bool IsExist { get; private set; }

    public void UpdateData(string name, StoneColor color, bool exist)
    {
        PlayerName = name;
        PlayerColor = color;
        IsExist = exist;
    }

    public void UpdateData(PlayerDataManager.AnPlayerData playerData)
    {
        UpdateData(playerData.PlayerName.Value, playerData.PlayerColor, playerData.IsExist);
    }
}
