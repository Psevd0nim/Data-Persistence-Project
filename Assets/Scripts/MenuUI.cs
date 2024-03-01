using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI inputText;
    [SerializeField] TextMeshProUGUI bestScore;

    int BestScore;
    string Name;

    private void Awake()
    {
        LoadBestRecord();
        bestScore.text = $"Best Score : {Name} : {BestScore}";
    }

    void LoadBestRecord()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            MainManager.BestRecord data = JsonUtility.FromJson<MainManager.BestRecord>(json);
            BestScore = data.Score;
            Name = data.Name;
        }
    }

    public void StartButton()
    {
        MenuManager.Instance.CurrentPlayer = inputText.text;
        MenuManager.Instance.CurrentBestRecord = BestScore;
        MenuManager.Instance.RecordPlayer = Name;
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}