using UnityEngine;

/// <summary>
/// 各シーンのロード完了感知用基底クラス
/// </summary>
[DefaultExecutionOrder(100)]
public class LoadingAnker : MonoBehaviour
{
    void Awake()
    {
        WhenLoaded();
    }

    /// <summary>
    /// ロード完了時のアクション
    /// </summary>
    protected virtual void WhenLoaded()
    {
        
    }
}
