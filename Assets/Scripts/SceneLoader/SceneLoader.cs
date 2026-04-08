using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : DebugMonoBehaviour
{
	public static SceneLoader instance { get; private set; }

    [SerializeField] private SceneLoaderAnimator sceneLoaderAnimator;
    public string CurrentScene { get; private set; }

    void Awake()
    {
        /// Creates the instance for the manager if there isn't already one
        if (instance != null)
        {
            Debug.LogWarning("SceneManager.cs: found more than one manager in the scene, the newest manager will be destroyed");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadScene(string name)
    {
        sceneLoaderAnimator.FadeOut();
        SceneManager.LoadScene(name, LoadSceneMode.Single);
        sceneLoaderAnimator.FadeIn();
    }

    public void LoadLocation(Vector3 position)
    {
        sceneLoaderAnimator.FadeOut();
        // move player
        sceneLoaderAnimator.FadeIn();
    }
}
