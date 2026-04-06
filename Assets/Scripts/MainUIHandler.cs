#if UNITY_EDITOR
using TMPro;
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField NameInputField;
    public TextMeshProUGUI TopScoreText;

    public void NameTextChanged(string name)
    {
        Debug.Log($"Name changed to: {name}");
        DataManager.Instance.Name = name;
    }

    private void Start()
    {
        NameInputField.text = DataManager.Instance.Name;
        UpdateTopScoreText(DataManager.Instance.TopScore);
        NameInputField.onValueChanged.AddListener(NameTextChanged);
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowHighscores()
    {
        SceneManager.LoadScene(2);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
        DataManager.Instance.SaveName();
    }

    public void SaveNameClicked()
    {
        DataManager.Instance.SaveName();
    }

    public void LoadColorClicked()
    {
        DataManager.Instance.LoadName();
    }
    public void SaveTopScore()
    {
        DataManager.Instance.SaveTopScore();
    }

    public void LoadTopScore()
    {
        DataManager.Instance.LoadTopScore();
        UpdateTopScoreText(DataManager.Instance.TopScore);
    }

    public void UpdateTopScoreText(int score)
    {
        TopScoreText.text = $"Best Score: {score}";

    }
}
