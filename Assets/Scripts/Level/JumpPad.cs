using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] float JumpBoostValue;
    private GameObject Player;
    bool _startTimer;
    float _timer;
    void Start()
    {
        _timer = 0;
        _startTimer = false;
    }
    private void Update()
    {
        if (_startTimer)
        {
            _timer += 1 * Time.deltaTime;
            if (_timer > 3)
            {
                _timer = 0;
                _startTimer = false;
                Player.GetComponent<PlayerController>().ResetJumpBoost();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = other.gameObject;
            other.GetComponent<PlayerController>().JumpBoost(JumpBoostValue);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _startTimer = true;
    }
}
