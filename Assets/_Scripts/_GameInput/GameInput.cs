using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    public event EventHandler OnSpaceBarPressed;
    public event EventHandler OnLeftMousePressed;
    public event EventHandler OnEPressed;
    public event EventHandler OnEscapePressed;
    public event EventHandler OnRPressed;

    //camera
    Vector2 _mouseInputVector;
    float _mouseSensitivity = 25;

    bool _mouseIsLocked;
    bool _isSprinting;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
    }

    private void OnEnable()
    {
        _playerInput.player.Enable();

        _playerInput.player.jump.performed += Jump_performed;
        _playerInput.player.shoot.performed += Shoot_performed;
        _playerInput.player.hack.performed += Hack_performed;
        _playerInput.player.esc.performed += Esc_performed;
        _playerInput.player.reload.performed += Reload_performed;
        _playerInput.player.sprint.performed += Sprint_performed;
        _playerInput.player.sprint.canceled += Sprint_canceled; ;

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        _playerInput.player.jump.performed -= Jump_performed;
        _playerInput.player.shoot.performed -= Shoot_performed;
        _playerInput.player.hack.performed -= Hack_performed;
        _playerInput.player.esc.performed -= Esc_performed;
        _playerInput.player.reload.performed -= Reload_performed;
    }

    private void Sprint_performed(InputAction.CallbackContext obj)
    {
        _isSprinting = true;
    }
    private void Sprint_canceled(InputAction.CallbackContext obj)
    {
        _isSprinting = false ;
    }

    private void Reload_performed(InputAction.CallbackContext obj)
    {
        OnRPressed?.Invoke(this, EventArgs.Empty);
    }
    private void Esc_performed(InputAction.CallbackContext obj)
    {
        OnEscapePressed?.Invoke(this, EventArgs.Empty);
    }


    private void Hack_performed(InputAction.CallbackContext obj)
    {
        OnEPressed?.Invoke(this, EventArgs.Empty);
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        OnLeftMousePressed?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnSpaceBarPressed?.Invoke(this, EventArgs.Empty);
    }

    public Vector3 GetInputVectorNormalized()
    {
        Vector2 inputDir = _playerInput.player.move.ReadValue<Vector2>();

        return new Vector3(inputDir.x, 0, inputDir.y);
    }
    public Vector2 GetMouseDelta()
    {
        //if (_mouseIsLocked)

            _mouseInputVector = _playerInput.player.mouse.ReadValue<Vector2>();
            return new Vector2(_mouseInputVector.x * _mouseSensitivity * Time.deltaTime, _mouseInputVector.y * _mouseSensitivity * Time.deltaTime);
  
        //return Vector2.zero;
    }
    public bool PlayerIsSprinting()
    {
        return _isSprinting;
    }
}
