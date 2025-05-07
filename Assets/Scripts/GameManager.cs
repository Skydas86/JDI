using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int currentCoin;
    [SerializeField] private GameObject archer;
    [SerializeField] private Transform spawnArcherPoint;
    [SerializeField] private TextMeshProUGUI coinText;
    void Start()
    {
        
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
            Instantiate(archer, transform.position, Quaternion.identity);
        }
    }
}
