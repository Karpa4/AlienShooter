using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    public const string VERTICAL_AXIS = "Vertical";
    public const string HORIZONTAL_AXIS = "Horizontal";
    public static GameData instance;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _endGameScoreText;
    private int _currentScore = 0;
    private int _highScore;

    private void Start()
    {
        if (instance == null) instance = this;
        LoadData();
    }

    /// <summary>
    /// Вызывается при смерти врага
    /// </summary>
    /// <param name="score">Кол-во очков начисляемых за убийство</param>
    public void ChangeScore(int score)
    {
        _currentScore += score;
        _scoreText.text = _currentScore.ToString();
    }

    /// <summary>
    /// Вывод итогового кол-ва очков после смерти игрока. Вызывается из UIStates
    /// </summary>
    public void OutputScore()
    {
        if (_currentScore > _highScore)
        {
            _endGameScoreText.text = $"Congrats! You have set a new highscore!\n{_currentScore}";
            SaveData();
        }
        else
        {
            _endGameScoreText.text = $"You score: {_currentScore}\nHighscore: {_highScore}";
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("Score", _currentScore);
        PlayerPrefs.Save();
    }

    private void LoadData()
    {
        _highScore = PlayerPrefs.GetInt("Score");
    }
}
