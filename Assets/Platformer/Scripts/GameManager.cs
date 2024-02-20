using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;

    private int coinAmount = 0;
    private int score = 0;
    private float comboTimer = 0f;
    private int comboMultiplier = 1;

    void Update()
    {
        UpdateTimer();
        int intTime = 400 - (int)Time.realtimeSinceStartup;
        string timeStr = $"Time \n {intTime}";
        timerText.text = timeStr;
        coinText.text = "Coins:\n" + coinAmount;
        scoreText.text = "Score:\n" + score;
    }

    private void UpdateTimer()
    {
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;
            if (comboTimer <= 0)
            {
                comboMultiplier = 1;
            }
        }
    }

    public void UpdateCoins()
    {
        coinAmount += 1;
    }

    public void UpdateScore()
    {
        score += 100 * comboMultiplier;
        if (comboTimer == 0)
        {
            comboTimer += 2f;
            comboMultiplier *= 2;
        }
        else
        {
            comboTimer = 2f;
            comboMultiplier *= 2;
        }
    }
}