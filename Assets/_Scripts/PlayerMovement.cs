using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float _moveSpeed = 15;
    float gravity = -9.8f;
    float jumpHeight = 10;
    float fallSpeed = 5;

    bool _isGrounded;

    CharacterController _characterController;

    Vector3 _upwardVelocity;

    //camera
    private float xRotation = 0;
    private float yRotation = 0;

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
        HandleCamera();
    }

    private void FixedUpdate()
    {
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
        Vector3 moveDir = GameInput.Instance.GetInputVectorNormalized();

        _isGrounded = _characterController.isGrounded;

        _characterController.Move(transform.TransformDirection(moveDir) * _moveSpeed * Time.fixedDeltaTime);

        if (_isGrounded && _upwardVelocity.y < 0)
            _upwardVelocity.y = -2f;

        _upwardVelocity.y += gravity * fallSpeed * Time.fixedDeltaTime;

        _characterController.Move(_upwardVelocity * Time.fixedDeltaTime);
    }

    private void GameInput_OnSpaceBarPressed(object sender, System.EventArgs e)
    {
        if (_isGrounded)
            _upwardVelocity.y = Mathf.Sqrt(-2 * gravity * jumpHeight);
    }

}
