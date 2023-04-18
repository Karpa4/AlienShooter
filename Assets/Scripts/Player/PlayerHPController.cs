using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPController : MonoBehaviour
{
    [SerializeField] private UIStates _uIStates;
    [SerializeField] private Image _hpImage;
    [SerializeField] private AudioSource _audioSource;
    private Health _health;
    private PlayerInput _playerInput;
    private Animator _animator;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.DeathEvent += PlayerIsDead;
        _health.HealthChangeEvent += PlayerChangeHealth;
        _playerInput = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
    }

    private void PlayerChangeHealth(int currentHealth)
    {
        _audioSource.Play();
        float temp = currentHealth;
        _hpImage.fillAmount = temp / 3;
    }

    private void PlayerIsDead()
    {
        PlayerChangeHealth(0);
        _animator.SetTrigger("Death");
        _playerInput.enabled = false;
        StartCoroutine(DelayAfterDeath());
    }

    private IEnumerator DelayAfterDeath()
    {
        yield return new WaitForSeconds(2);
        _uIStates.ActiveDeathScreen();
    }
}
