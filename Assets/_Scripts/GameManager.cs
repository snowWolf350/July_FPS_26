using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public event EventHandler OnGameStateChanged;

    enum GameState
    {
        inGame,Paused,finish
    }

    GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        GameInput.Instance.OnEscapePressed += GameInput_OnEscapePressed; 
        _currentGameState = GameState.inGame;
    }
    private void OnDestroy()
    {
        GameInput.Instance.OnEscapePressed -= GameInput_OnEscapePressed;
    }
    private void GameInput_OnEscapePressed(object sender, System.EventArgs e)
    {
        if (_currentGameState == GameState.inGame)
        {
            //pause the game
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            SetGameState(GameState.Paused);
            return;
        }
        else if (_currentGameState == GameState.Paused)
        {
            //unpause the game
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            SetGameState(GameState.inGame);
            return;
        }
    }

    private void Update()
    {
        switch (_currentGameState)
        {
            case GameState.inGame:
                break;
            case GameState.Paused:
                break;
            case GameState.finish:
                break;
        }
    }
    void SetGameState(GameState gameState)
    {
        _currentGameState = gameState;

        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool GameIsPaused()
    {
        return _currentGameState == GameState.Paused;
    }
}
