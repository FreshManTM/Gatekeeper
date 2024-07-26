using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button[] _levelButtons;


    void Start()
    {
        PlayerPrefs.SetInt("CompletedLevel", 5);
        int lastCompletedLevel = PlayerPrefs.GetInt("CompletedLevel");
        for (int i = _levelButtons.Length - 1; i >= lastCompletedLevel; i--)
        {
            _levelButtons[i].interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
