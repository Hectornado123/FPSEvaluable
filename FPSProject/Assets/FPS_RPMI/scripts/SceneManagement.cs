using UnityEngine;
using UnityEngine.SceneManagement;

// Script de Unity | 0 referencias
public class SceneManagement : MonoBehaviour
{
    // 0 referencias
    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // 0 referencias
    public void ExitGame()
    {
        Debug.Log("Has cerrado el juego.");
        Application.Quit();
    }
}