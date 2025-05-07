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
    void Start()
    {
        audioManager.PlayDefaultAudio();
    }

    // Update is called once per frame
    void Update()
    {
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
}
