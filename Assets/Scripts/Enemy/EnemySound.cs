using UnityEngine;

public class EnemySound : MonoBehaviour
{
    private AudioSource _audioSource;
    private Health _health;

    // Start is called before the first frame update
    void Start()
    {
        _health = GetComponent<Health>();
        _health.DeathEvent += PlayDeathSound;
        _health.HealthChangeEvent += PlayHitSound;
        _audioSource = GetComponent<AudioSource>();
    }

    private void PlayDeathSound()
    {
        _audioSource.Play();
    }

    private void PlayHitSound(int currentHealth)
    {
        _audioSource.Play();
    }
}
