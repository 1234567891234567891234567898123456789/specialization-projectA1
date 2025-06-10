using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}
