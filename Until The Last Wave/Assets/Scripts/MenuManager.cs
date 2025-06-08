using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject howToPlayPanel;
    public GameObject menuPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowHowToPlay()
    {
        howToPlayPanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void HideHowToPlay()
    {
        howToPlayPanel.SetActive(false);
        menuPanel.SetActive(true);
    }
}
