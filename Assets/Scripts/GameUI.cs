using UnityEngine;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    public void StarGame()
    {
        gameManager.StarGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ContinueGame()
    {
        gameManager.ResumeGame();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}

