using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] Animator _animator;
    [SerializeField] private PlayerInput _playerInput;
    private InputActionAsset _inputActions;

    // Cameras and canvas
    [SerializeField] CinemachineVirtualCamera twoDCam;
    [SerializeField] CinemachineFreeLook threeDCam;
    [SerializeField] Canvas twoDCanvas;

    // Gravity
    [SerializeField] private float Gravity;
    private Vector3 GravityVelocity = Vector3.zero;

    // Jump
    private Vector3 JumpVelocity;
    private InputAction JumpAction;
    private float JumpHeight = 3.0f;

    // Movement 
    public float Speed = 3;

    bool twoDimension = false;

    // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;

        twoDCam.enabled = twoDimension;
        threeDCam.enabled = !twoDimension;
        twoDCanvas.enabled = twoDimension;

        _inputActions = _playerInput.actions;
        JumpAction = _playerInput.actions["Jump"];
    }

    //Update is called once per frame
    void Update()
    {
        #region Movement
        Physics.SyncTransforms();
        Physics2D.SyncTransforms();
        if (twoDimension)
        {
            // 2D Movement
            gameObject.transform.rotation = Quaternion.identity;
            float input = _inputActions["2DMove"].ReadValue<float>();
            Vector3 moveDirection = new Vector3(input, 0, 0);
            _characterController.Move((JumpVelocity + moveDirection * Speed) * Time.deltaTime);
        }
        else
        {
            // 3D Movement
            Vector2 input = _inputActions["3DMove"].ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(input.x, 0, input.y);

            if (moveDirection.magnitude > 0)
            {
                moveDirection = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up) * moveDirection;
                // Rotate the character facing towards the move direction
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * 1000f);
            }

            _characterController.Move((JumpVelocity + moveDirection * Speed) * Time.deltaTime);
        }

        // Jump & Fall
        Jump();
        Fall();

        #endregion

        _animator.SetBool("IsWalking", false);

        if (_inputActions["3DMove"].IsPressed())
            _animator.SetBool("IsWalking", true);
        if (_inputActions["2DMove"].IsPressed())
            _animator.SetBool("IsWalking", true);

        SwitchCamera();
    }
    public void Fall()
    {
        // Falling
        GravityVelocity.y += Gravity * Time.deltaTime;
        if (_characterController.isGrounded && JumpVelocity.y < 0)
        {
            JumpVelocity.y = -2f;
        }
        JumpVelocity.y += Gravity * Time.deltaTime;
    }
    void Jump()
    {
        // Jumping
        if (JumpAction.IsPressed() && _characterController.isGrounded)
        {
            JumpVelocity.y = Mathf.Sqrt(JumpHeight * -2f * Gravity);
            AudioManager.Instance.PlaySFX("Jump");
        }
    }

    private void SwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            twoDimension = !twoDimension;

            twoDCam.enabled = twoDimension;
            threeDCam.enabled = !twoDimension;
            twoDCanvas.enabled = twoDimension;

            if (twoDimension) Camera.main.orthographic = true;
            else Camera.main.orthographic = false;
        }
    }
    public bool CheckTwoDimensions()
    {
        return twoDimension;
    }

    public void TeleportTwoD(float z)
    {
        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, z);
        _characterController.enabled = false;
        transform.position = newPos;
        _characterController.enabled = true;
    }
    public void JumpBoost(float value)
    {
        JumpHeight = value;
    }
    public void ResetJumpBoost()
    {
        JumpHeight = 3;
    }
}
