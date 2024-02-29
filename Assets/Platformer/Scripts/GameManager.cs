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
    private int intTime;
    private float time = 5f;

    void Update()
    {
        time -= Time.deltaTime;
        string timeStr = $"Time:\n " + Mathf.RoundToInt(time);
        timerText.text = timeStr;
        coinText.text = "Coins:\n" + coinAmount.ToString("00");
        scoreText.text = "Score:\n" + score.ToString("000000");

        if (time < 1)
        {
            Debug.Log("You could not finish the Level in Time");
            mario.transform.position = new Vector3(21, 2, 0);
            ResetTimer();
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
        score += 100;
    }

    public int GetTimer()
    {
        return intTime;
    }
}