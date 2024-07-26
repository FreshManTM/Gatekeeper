using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    [SerializeField] GameObject _winPanel;
    [SerializeField] GameObject _losePanel;

    [SerializeField] TextMeshProUGUI _timerText;
    [SerializeField] TextMeshProUGUI _missText;
    [SerializeField] TextMeshProUGUI _saveText;

    float _timer;
    int _saves;
    int _misses;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
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
            PlayerPrefs.SetInt("CompletedLevel", GameManager.Instance.CurrentLevelInfo.LevelNumber);
        }
        else
        {
            _losePanel.SetActive(true);
        }
    }

    public void BallMiss()
    {
        _misses++;
        _missText.text = _misses.ToString();
    }

    public void BallSave()
    {
        _saves++;
        _saveText.text = _saves.ToString();
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
