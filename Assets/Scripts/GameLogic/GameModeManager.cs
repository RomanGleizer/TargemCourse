using System;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager Instance { get; private set; }

    public enum Mode { Map, Battle }

    private Mode _currentMode = Mode.Map;
    public Mode CurrentMode => _currentMode;

    public event Action<Mode> OnModeChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Debug.Log($"[GameModeManager] initialized in {_currentMode} mode");
            OnModeChanged?.Invoke(_currentMode);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMode(Mode newMode)
    {
        if (_currentMode == newMode) return;
        _currentMode = newMode;
        Debug.Log($"[GameModeManager] mode changed to {_currentMode}");
        OnModeChanged?.Invoke(_currentMode);
    }
}
