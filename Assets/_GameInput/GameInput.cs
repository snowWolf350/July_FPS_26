using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;

    PlayerInput _playerInput;

    public event EventHandler OnSpaceBarPressed;

    //camera
    Vector2 _mouseInputVector;
    float _mouseSensitivity = 25;

    bool _mouseIsLocked;

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

        Cursor.lockState = CursorLockMode.Locked;
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
}
