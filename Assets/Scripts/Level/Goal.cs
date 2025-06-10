using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] bool FinalGoal;
    [SerializeField] string NextScene;
    private void OnTriggerEnter(Collider other)
    {
        if (FinalGoal)
        {
            MainManager.Instance.FinishGame();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game End");
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(NextScene);
    }
}
