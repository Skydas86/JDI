using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentCoin;
    [SerializeField] private GameObject archer;
    [SerializeField] private Transform spawnArcherPoint;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject pauseGameMenu;

    void Start()
    {
        audioManager.PlayDefaultAudio();
        MainMenu();
    }

    

    public void AddCoin(int coin)
    {
        currentCoin += coin;
        if (coinText != null)
            coinText.text = currentCoin.ToString();
    }
    public void CallArcher()
    {
        if (currentCoin >= 10)
        {
            currentCoin -= 10;
            if (coinText != null)
                coinText.text = currentCoin.ToString();
            Instantiate(archer, transform.position, Quaternion.identity);
        }
    }
    public void MainMenu()
    {
        mainMenu.SetActive(true);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 0f;
    }
    public void GameOverMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 0f;

    }
    public void PauseGameMenu()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0f;

    }
    public void StarGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;

    }
    public void ResumeGame()
    {
        mainMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1f;

    }
}
