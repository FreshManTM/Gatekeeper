using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] LevelInfo[] _levelInfo;
    [SerializeField,ReadOnly] int _currentLevel;

    public LevelInfo CurrentLevelInfo;
    public int Coins { get; private set; }
    public float KeeperSize { get; private set; }
    public float KeeperSpeed { get; private set; }
    public int KeeperAmount { get; private set; }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadInfo();
        Coins = 1000;
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber > 10)
            levelNumber = Random.Range(1, 11);
        _currentLevel = levelNumber;
        CurrentLevelInfo = _levelInfo[_currentLevel - 1];

        SceneManager.LoadScene(1);
    }

    public void AddCoins(int number)
    {
        Coins += number;
        PlayerPrefs.SetInt("Coins", Coins);
    }

    public void UpgradeKeeperSize(int cost)
    {
        AddCoins(-cost);
        KeeperSize += .1f;
        PlayerPrefs.SetFloat("KeeperSize", KeeperSize);
    }

    public void UpgradeKeeperSpeed(int cost)
    {
        AddCoins(-cost);
        KeeperSpeed += .5f;
        PlayerPrefs.SetFloat("KeeperSpeed", KeeperSpeed);
    }
    public void UpgradeKeeperAmount(int cost)
    {
        AddCoins(-cost);
        KeeperAmount += 1;
        PlayerPrefs.SetInt("KeeperAmount", KeeperAmount);
    }

    void LoadInfo()
    {
        if (PlayerPrefs.HasKey("Coins"))
        {
            Coins = PlayerPrefs.GetInt("Coins");
            KeeperSize = PlayerPrefs.GetFloat("KeeperSize");
            KeeperSpeed = PlayerPrefs.GetFloat("KeeperSpeed");
            KeeperAmount = PlayerPrefs.GetInt("KeeperAmount");
        }
        else
        {
            KeeperSize = .3f;
            KeeperSpeed = 1f;
            KeeperAmount = 3;

            PlayerPrefs.SetFloat("KeeperSize", KeeperSize);
            PlayerPrefs.SetFloat("KeeperSpeed", KeeperSpeed);
            PlayerPrefs.SetInt("KeeperAmount", KeeperAmount);
        }
    }

}
