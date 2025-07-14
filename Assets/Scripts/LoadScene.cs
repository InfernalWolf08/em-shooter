using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public string scene;

    public void load()
    {
        SceneManager.LoadScene(scene);
    }
}