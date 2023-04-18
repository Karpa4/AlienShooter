using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int MaxHealth;
    private int _currentHealth;

    public delegate void HealthHandler(int currentHealth);
    public event HealthHandler HealthChangeEvent;

    public delegate void DeathHandler();
    public event DeathHandler DeathEvent;

    private void OnEnable()
    {
        _currentHealth = MaxHealth;
    }

    /// <summary>
    /// Метод принятия урона
    /// </summary>
    /// <param name="damage">Кол-во урона</param>
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if (_currentHealth > 0)
        {
            HealthChangeEvent?.Invoke(_currentHealth);
        }
        else if (_currentHealth == 0)
        {
            DeathEvent?.Invoke();
        }
    }

    /// <summary>
    /// Метод для взаимодействия с аптечкой
    /// </summary>
    /// <returns>true - аптечка была использована , false - у игрока полное здоровье</returns>
    public bool HealByMed()
    {
        bool _medPackUsed;
        if (_currentHealth == 3)
        {
            _medPackUsed = false;
        }
        else
        {
            _currentHealth++;
            HealthChangeEvent?.Invoke(_currentHealth);
            _medPackUsed = true;
        }
        return _medPackUsed;
    }
}
