using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Data.FinishedGame = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game End");
    }
}
