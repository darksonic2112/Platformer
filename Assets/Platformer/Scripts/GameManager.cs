using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI scoreText;
    public GameObject mario;
    public MusicPlayer music;

    private int coinAmount = 0;
    private int score = 0;
    private float comboTimer = 0f;
    private int comboMultiplier = 1;
    private int intTime;
    private float time = 100f;

    void Update()
    {
        UpdateTimer();
        time -= Time.deltaTime;
        string timeStr = $"Time:\n " + Mathf.RoundToInt(time);
        timerText.text = timeStr;
        coinText.text = "Coins:\n" + coinAmount.ToString("00");
        scoreText.text = "Score:\n" + score.ToString("000000");

        if (time == 0)
        {
            Debug.Log("You could not finish the Level in Time");
            mario.transform.position = new Vector3(21, 2, 0);
            ResetTimer();
        }
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

    public void ResetTimer()
    {
        time = 100;
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

    public int GetTimer()
    {
        return intTime;
    }
}