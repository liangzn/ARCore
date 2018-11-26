using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    NONE,
    SEARCHING,
    CREATING,
    FIGHTING,
    VICTORY,
    DEFEAT
}

public class GameManager : BaseManager<GameManager>, IRestart
{
    private GameState state = GameState.NONE;

    private void Awake()
    {
        instance = this;
        UIManager.RestartHandler += Restart;
    }

    private void OnDestroy()
    {
        UIManager.RestartHandler -= Restart;
    }

    public void SetGameState(GameState _state)
    {
        state = _state;
        UIManager.instance.SetUIOnGameState(state);
    }

    public GameState GetGameState()
    {
        return state;
    }

    // Implement IRestart inferface.
    public void Restart()
    {
        SetGameState(GameState.CREATING);
    }
}
