using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
