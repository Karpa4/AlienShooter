using UnityEngine;

public class UIStates : MonoBehaviour
{
    [SerializeField] private GameObject _firstState;
    [SerializeField] private GameObject _deathState;
    private GameObject _currentState;

    private void Awake()
    {
        ChangeState(_firstState);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Вызывается при смерти игрока
    /// </summary>
    public void ActiveDeathScreen()
    {
        GameData.instance.OutputScore();
        ChangeState(_deathState);
        Time.timeScale = 0;
    }

    /// <summary>
    /// Включение паузы
    /// </summary>
    public void PauseOn()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Выключение паузы
    /// </summary>
    public void PauseOff()
    {
        Time.timeScale = 1;
    }

    /// <summary>
    /// Переключение между стейтами UI
    /// </summary>
    /// <param name="newState">Новый стейт</param>
    public void ChangeState(GameObject newState)
    {
        if (_currentState != null)
        {
            _currentState.SetActive(false);
        }

        _currentState = newState;

        if (_currentState != null)
        {
            _currentState.SetActive(true);
        }
    }
}
