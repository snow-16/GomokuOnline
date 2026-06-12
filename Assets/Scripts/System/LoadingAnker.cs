using UnityEngine;

public class LoadingAnker : MonoBehaviour
{
    void Awake()
    {
        GameManager.EndLoading();
        WhenLoaded();
    }

    protected virtual void WhenLoaded()
    {
        
    }
}
