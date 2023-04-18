using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private Collider _secondCollider;
    [SerializeField] private int _score;
    private Collider _enemyCol;
    private Health _health;

    private void Awake()
    {
        _enemyCol = GetComponent<Collider>();
        _health = GetComponent<Health>();
        _health.DeathEvent += EnemyIsDead;
    }

    private void OnEnable()
    {
        _enemyCol.enabled = true;
        _secondCollider.enabled = true;
    }

    private IEnumerator Deactivate()
    {
        _enemyCol.enabled = false;
        _secondCollider.enabled = false;
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }

    private void EnemyIsDead()
    {
        StartCoroutine(Deactivate());
        GameData.instance.ChangeScore(_score);
    }
}
