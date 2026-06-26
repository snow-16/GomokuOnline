using UnityEngine;

public class InGameLoadingAnker : LoadingAnker
{
    [SerializeField]
    private GameObject _inGameDataManagerPrefab;

    protected override void WhenLoaded()
    {
        if(RoomData.Instance.PlayerNumber == 1)
        {
            RelayManager.NetworkRunner.SpawnAsync(_inGameDataManagerPrefab);
        }
    }
}
