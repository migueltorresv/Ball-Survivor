using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public string name;
    public int greenPoint;
    public int bluePoint;
    public int redPoint;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPoints();
    }
    
    [System.Serializable]
    class SaveData
    {
        public string name;
        public int greenPoint;
        public int bluePoint;
        public int redPoint;
    }

    public void SavePoints()
    {
        SaveData data = new SaveData();
        data.name = name;
        data.greenPoint = greenPoint;
        data.bluePoint = bluePoint;
        data.redPoint = redPoint;

        string json = JsonUtility.ToJson(data);
        string path = Application.persistentDataPath + "/savefile.json";
        File.WriteAllText(path, json);
    }

    public void LoadPoints()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            name = data.name;
            greenPoint = data.greenPoint;
            bluePoint = data.bluePoint;
            redPoint = data.redPoint;
        }
    }
}
