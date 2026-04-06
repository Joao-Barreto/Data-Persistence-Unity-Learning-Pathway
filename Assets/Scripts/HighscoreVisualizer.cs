using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighscoreVisualizer : MonoBehaviour
{
    public List<HighscoreEntry> Highscores = new List<HighscoreEntry>();

    public Transform contentParent;      // The ScrollView Content
    public GameObject scoreEntryTemplate; // Your ScoreEntry GameObject

    void Start()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.LoadHighScores();
            Highscores = DataManager.Instance.Highscores;
            // Sort by score descending
            Highscores.Sort((a, b) => b.Score.CompareTo(a.Score));
            Highscores.ForEach(highscore => Debug.Log($"HighScoreEntry: {highscore.Name}, {highscore.Score}"));
            DisplayHighscores();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    void DisplayHighscores()
    {
        // Clear old entries (optional)
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (HighscoreEntry entry in Highscores)
        {
            // Instantiate a copy of the template
            GameObject item = Instantiate(scoreEntryTemplate, contentParent);
            item.SetActive(true); // Make sure it’s visible

            // Set the left and right texts
            TextMeshProUGUI nameText = item.transform.Find("NameText").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI scoreText = item.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();

            nameText.text = entry.Name;
            scoreText.text = entry.Score.ToString();
        }
    }
}