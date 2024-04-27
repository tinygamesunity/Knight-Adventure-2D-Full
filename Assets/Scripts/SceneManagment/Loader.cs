using UnityEngine.SceneManagement;

public static class Loader {

    public enum Scene {
        MainMenuScene,
        Level1Scene,
        Level2Scene,
        LoadingScene,
        GameOverScene
    }

    public static Scene targetScene;

    public static void Load(Scene targetScene) {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void InstantLoad(Scene targetScene) {
        SceneManager.LoadScene(targetScene.ToString());
    }

    public static void LoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }

}
