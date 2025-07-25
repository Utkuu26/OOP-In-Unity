using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathMenu;

    private bool isGameOver = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        isGameOver = false;
        Cursor.visible = false;
    }

    public void PlayerDied()
    {
        if (isGameOver) return;

        isGameOver = true;
        Time.timeScale = 0f; 
        deathMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
