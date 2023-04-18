using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.HealthChangeEvent += HealthChanged;
        _health.DeathEvent += IsDead;
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animator.Play("Walk");
    }

    /// <summary>
    /// Если враг дошел до игрока
    /// </summary>
    public void IsAttack()
    {
        _animator.SetBool("IsAttack", true);
    }

    /// <summary>
    /// Если игрок ушел из зоны удара
    /// </summary>
    public void EndAttack()
    {
        _animator.SetBool("IsAttack", false);
    }

    /// <summary>
    /// Вызывается при нанесении несмертельного урона
    /// </summary>
    private void IsHit()
    {
        _animator.SetTrigger("IsHit");
    }

    /// <summary>
    /// Вызывается при смерти врага
    /// </summary>
    private void IsDead()
    {
        _animator.SetTrigger("IsDead");
    }

    private void HealthChanged(int currentHealth)
    {
        IsHit();
    }
}
