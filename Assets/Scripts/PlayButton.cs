using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public Button button;


    private void Start()
    {
        // я не знаю что это делает
        button.onClick.AddListener(LoadGameScene);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }
}
