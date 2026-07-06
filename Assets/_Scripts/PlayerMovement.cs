using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    float _walkSpeed = 10;
    float _sprintSpeed = 15;
    float _moveSpeed;
    float _gravity = -9.8f;
    float _jumpHeight = 10;
    float _fallSpeed = 5;

    bool _isGrounded;
    float _jumpBufferTimer;

    CharacterController _characterController;

    Vector3 _upwardVelocity;

    //camera
    private float xRotation = 0;
    private float yRotation = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        GameInput.Instance.OnSpaceBarPressed += GameInput_OnSpaceBarPressed;
    }

    private void OnDisable()
    {
        GameInput.Instance.OnSpaceBarPressed -= GameInput_OnSpaceBarPressed;
    }
    private void Update()
    {

        if (GameManager.Instance.GameIsPlaying() == false) return;

        HandleCamera();


        //handle jump buffer 
        if (_jumpBufferTimer < 0) return;

        _jumpBufferTimer -= Time.deltaTime;

        if (_jumpBufferTimer > 0 && _isGrounded)
        {
            _upwardVelocity.y = Mathf.Sqrt(-2 * _gravity * _jumpHeight);
            _jumpBufferTimer = 0;
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.GameIsPlaying() == false) return;

        HandleMovement();
    }
    void HandleCamera()
    {
        Vector2 mouseInput = GameInput.Instance.GetMouseDelta();

        // vertical look
        xRotation -= mouseInput.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseInput.x;

        // horizontal look
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    void HandleMovement()
    {
        _moveSpeed = GameInput.Instance.PlayerIsSprinting() ? _sprintSpeed : _walkSpeed;

            Vector3 moveDir = GameInput.Instance.GetInputVectorNormalized();

        _isGrounded = _characterController.isGrounded;

        _characterController.Move(transform.TransformDirection(moveDir) * _moveSpeed * Time.fixedDeltaTime);

        if (_isGrounded && _upwardVelocity.y < 0)
            _upwardVelocity.y = -2f;

        _upwardVelocity.y += _gravity * _fallSpeed * Time.fixedDeltaTime;

        _characterController.Move(_upwardVelocity * Time.fixedDeltaTime);
    }

    private void GameInput_OnSpaceBarPressed(object sender, System.EventArgs e)
    {
        _jumpBufferTimer = 0.25f;
    }

    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }
}
