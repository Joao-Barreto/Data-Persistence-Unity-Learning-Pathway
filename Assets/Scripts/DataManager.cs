using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string Name;
    public int TopScore;
    public List<HighscoreEntry> Highscores = new List<HighscoreEntry>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
        LoadTopScore();
        LoadHighScores();
    }

    [System.Serializable]
    class SaveData
    {
        public string Name;
        public int TopScore;
        public List<HighscoreEntry> Highscores;
    }

    public void SaveName()
    {
        SaveData data = new SaveData();
        data.Name = Name;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Name = data.Name;
        }
    }
    public void SaveTopScore()
    {
        SaveData data = new SaveData();
        data.TopScore = TopScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void LoadTopScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            TopScore = data.TopScore;
        }
    }

    public void SaveHighScores(string name, int score)
    {
        SaveData data = new SaveData();

        // 1. Add new entry
        Highscores.Add(new HighscoreEntry { Name = name, Score = score });

        // 2. Sort descending
        Highscores.Sort((a, b) => b.Score.CompareTo(a.Score));

        // 3. Keep only top 10
        if (Highscores.Count > 10)
        {
            Highscores.RemoveRange(10, Highscores.Count - 10);
        }

        // 4. Save
        data.Highscores = Highscores;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Debug.Log(json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Highscores = data.Highscores;
        }
    }
}
