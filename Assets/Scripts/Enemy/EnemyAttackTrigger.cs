using UnityEngine;

/// <summary>
/// У каждого врага есть анимация атаки. В атакующую руку добавлен коллайдер с триггером
/// </summary>
public class EnemyAttackTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health _health))
        {
            _health.TakeDamage(1);
        }
    }
}
