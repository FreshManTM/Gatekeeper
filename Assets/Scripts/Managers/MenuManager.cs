using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button[] _levelButtons;

    GameManager _gameManager;

    [SerializeField] TextMeshProUGUI _coinsText;
    [SerializeField] TextMeshProUGUI[] _upgradeLvlText;
    [SerializeField] TextMeshProUGUI[] _upgradeCostText;
    [SerializeField] int[] _upgradeCosts;
    [SerializeField] int[] _upgradeLvls;

    void Start()
    {
        _gameManager = GameManager.Instance;
        if (!PlayerPrefs.HasKey("CompletedLevel"))
            PlayerPrefs.SetInt("CompletedLevel", 1);

        int lastCompletedLevel = PlayerPrefs.GetInt("CompletedLevel");
        for (int i = _levelButtons.Length - 1; i >= lastCompletedLevel; i--)
        {
            _levelButtons[i].interactable = false;
        }
        LoadUpgrades();
    }

    private void Update()
    {
        _coinsText.text = _gameManager.Coins.ToString();
    }

    public void UpgradeButton(int upgradeNumber)
    {
        if (_gameManager.Coins >= _upgradeCosts[upgradeNumber])
        {
            switch (upgradeNumber)
            {
                case 0:
                    _gameManager.UpgradeKeeperSize(_upgradeCosts[upgradeNumber]);
                    PlayerPrefs.SetInt("KeeperSizeLvl", _upgradeLvls[upgradeNumber]);
                    break;
                case 1:
                    _gameManager.UpgradeKeeperAmount(_upgradeCosts[upgradeNumber]);
                    PlayerPrefs.SetInt("KeeperAmountLvl", _upgradeLvls[upgradeNumber]);
                    break;
                case 2:
                    _gameManager.UpgradeKeeperSpeed(_upgradeCosts[upgradeNumber]);
                    PlayerPrefs.SetInt("KeeperSpeedLvl", _upgradeLvls[upgradeNumber]);
                    break;
            }

            _upgradeLvls[upgradeNumber] += 1;
            _upgradeCosts[upgradeNumber] += 100;
            _upgradeLvlText[upgradeNumber].text = $"LVL {_upgradeLvls[upgradeNumber]}";
            _upgradeCostText[upgradeNumber].text = _upgradeCosts[upgradeNumber].ToString();
        }
    }

    void LoadUpgrades()
    {
        _upgradeLvls[0] = PlayerPrefs.GetInt("KeeperSizeLvl");
        _upgradeLvls[1] = PlayerPrefs.GetInt("KeeperAmountLvl");
        _upgradeLvls[2] = PlayerPrefs.GetInt("KeeperSpeedLvl");
        for (int i = 0; i < _upgradeLvls.Length; i++)
        {
            _upgradeCosts[i] += _upgradeLvls[i] * 100;
            _upgradeLvlText[i].text = $"LVL {_upgradeLvls[i]}";
            _upgradeCostText[i].text = _upgradeCosts[i].ToString();
        }
    }
}
