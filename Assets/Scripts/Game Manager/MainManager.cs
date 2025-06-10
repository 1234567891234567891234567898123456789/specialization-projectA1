using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public int PlayerLives;

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
        Data.FinishedGame = false;
        PlayerLives = 3;
    }
}
