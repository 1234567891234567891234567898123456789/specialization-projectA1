using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int PlayerLives;
    bool FinishedGame;

    private void Awake()
    {
        PlayerLives = 3;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        FinishedGame = false;
    }

    public void LoseLive()
    {
        PlayerLives--;
    }

    public int GetLives()
    {
        return PlayerLives;
    }

    public void RestartGame()
    {
        FinishedGame = false;
        PlayerLives = 3;
    }
    public void FinishGame()
    {
        FinishedGame = true;
    }
    public bool GetGamePassed()
    {
        return FinishedGame;
    }
}
