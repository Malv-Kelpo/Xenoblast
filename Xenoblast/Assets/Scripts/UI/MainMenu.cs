using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        Debug.Log("MainMenu start called");
        AudioManager.Instance.PlayMusic("MainMenuTheme");    
    }
    public void StartGame()
    {
        AudioManager.Instance.StopMusic("MainMenuTheme");
        SceneManager.LoadScene("GameScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
