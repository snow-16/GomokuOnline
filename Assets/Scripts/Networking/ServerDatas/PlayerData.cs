using UnityEngine;

public class PlayerData
{
    public static PlayerData Own { get; private set; }
    public static PlayerData Opponets { get; private set; }

    public static void SetInstance()
    {
        Own = new PlayerData();
        Opponets = new PlayerData();
    }



    public string PlayerName { get; private set; }

    public int PlayerNumber { get; private set; }

    public StoneColor PlayerColor { get; private set; }

    public bool IsExist { get; private set; }

    public void UpdateData(string name, int num, StoneColor color, bool exist)
    {
        PlayerName = name;
        PlayerNumber = num;
        PlayerColor = color;
        IsExist = exist;
    }

    public void UpdateData(PlayerDataManager.AnPlayerData playerData)
    {
        UpdateData(playerData.PlayerName.Value, playerData.PlayerNumber, playerData.PlayerColor, playerData.IsExist);
    }
}
