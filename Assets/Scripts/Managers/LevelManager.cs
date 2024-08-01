using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _losePanel;

    [SerializeField] TextMeshProUGUI _lvlText;
    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _missText;
    [SerializeField] TextMeshProUGUI _saveText;

    GameManager _gameManager;
    float _delayBeforeLevel = 5f;
    int _coinsReward = 50;

    float _timer;
    int _saves;
    int _misses;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _gameManager = GameManager.Instance;

        _lvlText.text = $"LVL {_gameManager.CurrentLevelInfo.LevelNumber}";
        _timer = _gameManager.CurrentLevelInfo.LevelTime + _delayBeforeLevel;
        StartCoroutine(ITimer());  
    }

    IEnumerator ITimer()
    {
        _timer--;
        _timerText.text = _timer.ToString();
        yield return new WaitForSeconds(1f);
        if (_timer > 0)
            StartCoroutine(ITimer());
        else
            LevelComplete();
    }

    void LevelComplete()
    {
        if (_saves > _misses)
        {
            _winPanel.SetActive(true);
            _winPanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
            PlayerPrefs.SetInt("CompletedLevel", GameManager.Instance.CurrentLevelInfo.LevelNumber);
            _gameManager.AddCoins(_coinsReward);
        }
        else
        {
            _losePanel.SetActive(true);
            _losePanel.GetComponent<CanvasGroup>().DOFade(1, 1f);
        }
    }

    public void BallMiss()
    {
        _misses++;
        _missText.text = $"Misses: {_misses}";
    }

    public void BallSave()
    {
        _saves++;
        _saveText.text = $"Saves: {_saves}";
    }

    public void PauseButton()
    {
        Time.timeScale = 0f;
    }
    public void ResumeButton()
    {
        Time.timeScale = 1f;
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void LoadNextLevel()
    {
        _gameManager.LoadLevel(_gameManager.CurrentLevelInfo.LevelNumber + 1);
    }
}
