using UnityEditor.SettingsManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenuScript : MonoBehaviour
{
    //Goes on the Global UI Object on every map
    public GameObject mainMenuUI,lobbyUI,settingsUI,pauseUI;
    void Start()
    {
        mainMenuUI.SetActive(true);
        lobbyUI.SetActive(false);
        settingsUI.SetActive(false);
    }
    public void playButton()
    {
        lobbyUI.SetActive(true);
        settingsUI.SetActive(false);
        mainMenuUI.SetActive(false);    
    }
    public void settingsButton()
    {
        //If your on the main menu
        if (SceneManager.GetActiveScene().name == "main_menu"){
            settingsUI.SetActive(true);
            mainMenuUI.SetActive(false);
            lobbyUI.SetActive(false);
        }
        //If your in game
        if (SceneManager.GetActiveScene().name != "main_menu")
        {
            settingsUI.SetActive(true);
        }
    }
    public void backButton()
    {
        //If your on the main menu
        if (SceneManager.GetActiveScene().name == "main_menu"){
            settingsUI.SetActive(false);
            mainMenuUI.SetActive(true);
            lobbyUI.SetActive(false);
        }

        //If your in game
        if (SceneManager.GetActiveScene().name != "main_menu"){
            settingsUI.SetActive(false);
        }
    }
    public void quitButton()
    {
        Application.Quit();
    }
}
