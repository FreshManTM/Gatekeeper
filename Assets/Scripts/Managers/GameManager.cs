using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int coins;
    [SerializeField] LevelInfo[] _levelInfo;
    [SerializeField] int _currentLevel;
    

    public LevelInfo CurrentLevelInfo;
    public int _maxKeepers;
    LevelManager _lvlManager;


    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public void LoadLevel(int levelNumber)
    {
        if (levelNumber > 10)
            levelNumber = Random.Range(1, 11);
        _currentLevel = levelNumber;
        CurrentLevelInfo = _levelInfo[_currentLevel - 1];

        SceneManager.LoadScene(1);
    }
}
