using UnityEngine;

/// <summary>
/// インゲームシーンロード完了感知用クラス
/// </summary>
public class InGameLoadingAnker : LoadingAnker
{
    /// <summary> インゲームサーバーデータ同期用プレハブ </summary>
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
