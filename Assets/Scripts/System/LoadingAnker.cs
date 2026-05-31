using UnityEngine;

public class LoadingAnker : MonoBehaviour
{
    void Awake()
    {
        GameManager.EndLoading();
        WhenLoaded();
        Destroy(gameObject);
    }

    protected virtual void WhenLoaded()
    {
        
    }
}
