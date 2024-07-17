using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] LevelInfo[] _levelInfo;
    [SerializeField] int _currentLevel;
    public LevelInfo CurrentLevelInfo;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        CurrentLevelInfo = _levelInfo[_currentLevel-1];
    }

    private void Start()
    {
    }
}
