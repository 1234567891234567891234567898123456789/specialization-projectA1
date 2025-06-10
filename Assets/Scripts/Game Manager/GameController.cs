using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController Player;
    [SerializeField] GameObject LevelPlatforms;
    [SerializeField] GameObject PlayerObject;
    [SerializeField] GameObject Instruction2D, Instruction3D;
    [SerializeField] TMP_Text PlayerLives;

    Transform[] Platforms;
    private void Start()
    {
        PlayerLives.text = "Lives: " + MainManager.Instance.GetLives();
    }
    private void Update()
    {

        if (Player.CheckTwoDimensions())
        {
            // Get platforms in the level
            Platforms = LevelPlatforms.GetComponentsInChildren<Transform>();

            // 2D collisions
            for (int i = 0; i < Platforms.Length; i++)
            {
                //if (PlayerObject.transform.position.x - PlayerObject.transform.localScale.x < Platforms[i].position.x + Platforms[i].transform.localScale.x &&
                //    PlayerObject.transform.position.x + PlayerObject.transform.localScale.x > Platforms[i].transform.position.x - Platforms[i].transform.localScale.x &&
                //    PlayerObject.transform.position.y < Platforms[i].transform.position.y + Platforms[i].transform.localScale.y&&
                //    PlayerObject.transform.position.y + PlayerObject.transform.localScale.y > Platforms[i].transform.position.y)

                Vector3 playerMin = PlayerObject.transform.position - PlayerObject.transform.localScale * 0.5f;
                Vector3 playerMax = PlayerObject.transform.position + PlayerObject.transform.localScale * 0.5f;

                Vector3 platformMin = Platforms[i].position - Platforms[i].transform.localScale * 0.5f;
                Vector3 platformMax = Platforms[i].position + Platforms[i].transform.localScale * 0.5f;

                if (playerMin.x < platformMax.x &&
                    playerMax.x > platformMin.x &&
                    playerMin.y < platformMax.y &&
                    playerMax.y > platformMin.y)
                {
                    // If Colliding with playform
                    if (Platforms[i].gameObject.layer == 6)
                    {
                        // If colliding with platform layer
                        //PlayerObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, Platforms[i].transform.position.z);
                        Player.TeleportTwoD(Platforms[i].transform.position.z);
                        Debug.Log(Platforms[i]);
                    }
                    if (Platforms[i].gameObject.layer == 8)
                        PlayerDie();
                }
            }
        }

        // Active Instructions
        Instruction2D.SetActive(Player.CheckTwoDimensions());
        Instruction3D.SetActive(!Player.CheckTwoDimensions());

        // If player out of bounds
        if (PlayerObject.transform.position.y < -5)
            PlayerDie();

        if (MainManager.Instance.GetLives() == 0)
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game End");


        // Testing purposes
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Player.TeleportTwoD(5);
        }
    }

    private void PlayerDie()
    {
        Player.transform.position = Vector3.zero;
        MainManager.Instance.LoseLive();
        PlayerLives.text = "Lives: " + MainManager.Instance.GetLives();
    }
}
