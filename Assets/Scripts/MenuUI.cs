using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;    
#endif

public class MenuUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TextMeshProUGUI nameBSText;
    [SerializeField] private TextMeshProUGUI greenBSText;
    [SerializeField] private TextMeshProUGUI redBSText;
    [SerializeField] private TextMeshProUGUI blueBSText;

    private void Start()
    {  
        MainManager.Instance.LoadPoints();
        LoadPoints();
    }

    public void StartGame()
    {
        if (string.IsNullOrEmpty(inputName.text))
        {
            inputName.placeholder.GetComponent<TextMeshProUGUI>().text = "...Please enter a name ";
        }
        else
        {
            MainManager.Instance.name = inputName.text;
            SceneManager.LoadScene(1);
        }
        
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void LoadPoints()
    {
        nameBSText.SetText(MainManager.Instance.name);
        greenBSText.SetText($"green: {MainManager.Instance.greenPoint.ToString()}");
        redBSText.SetText($"red: {MainManager.Instance.redPoint.ToString()}");
        blueBSText.SetText($"blue: {MainManager.Instance.bluePoint.ToString()}");
        
    }
}
