using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _checkTargetDelay; // Через какое время надо проверять цель
    private NavMeshAgent _agent;
    private AnimationController _animationController;
    private bool _rotateEnemy; // Нужно ли вращать врага (Если игрок находится в радиусе удара)
    private bool _isAlive; // Жив враг или нет
    private Transform _target;
    private Health _health;

    [SerializeField] private float _attackTime;
    private bool _isAttack = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _health.DeathEvent += StopMoveAfterDeath;
        _agent = GetComponent<NavMeshAgent>();
        _animationController = GetComponent<AnimationController>();
        _target = PlayerPosition.instance.PlayerTransform;
    }

    private void Update()
    {
        if (_rotateEnemy)
        {
            Quaternion lookrotation = Quaternion.LookRotation(_target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookrotation, Time.deltaTime * 2);
        }
    }

    private void OnEnable()
    {
        _rotateEnemy = false;
        _isAlive = true;
        _isAttack = false;
        StartCoroutine("CheckTarget");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out Health _health))
        {
            _health.TakeDamage(1);
        }
    }

    /// <summary>
    /// Вызывается в начале анимации Attack
    /// </summary>
    public void StopMove()
    {
        StartCoroutine("StopMoveCoroutine");
    }

    private void StopMoveAfterDeath()
    {
        StopCoroutine("CheckTarget");
        _isAlive = false;
        _rotateEnemy = false;
        if (_isAttack)
        {
            StopCoroutine("StopMoveCoroutine");
        }
        _agent.isStopped = true;
    }

    private IEnumerator CheckTarget()
    {
        while (_isAlive)
        {
            _agent.SetDestination(_target.position);
            yield return new WaitForSeconds(_checkTargetDelay);

            if (!_agent.pathPending && _agent.remainingDistance < _agent.stoppingDistance)
            {
                _animationController.IsAttack();
                _rotateEnemy = true;
            }
            else
            {
                _rotateEnemy = false;
                _animationController.EndAttack();
            }
        }
    }

    private IEnumerator StopMoveCoroutine()
    {
        _isAttack = true;
        _agent.isStopped = true;
        yield return new WaitForSeconds(_attackTime);
        _agent.isStopped = false;
        _isAttack = false;
    }
}
