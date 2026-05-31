using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager
{
    private static SceneType _nowScene = SceneType.Title;
    private static SceneType _scheduleScene;

    public static void TransitionScene(SceneType scene)
    {
        _nowScene = SceneType.Loading;
        _scheduleScene = scene;
        SceneManager.LoadScene(scene.ToString());
    }

    public static void EndLoading()
    {
        _nowScene = _scheduleScene;
    }
}
