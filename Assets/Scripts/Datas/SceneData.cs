using UnityEngine;

public class SceneData
{
    public static SceneType NowScene { get; private set; } = SceneType.Title;

    private static SceneType NextScene { get; set; }
}
