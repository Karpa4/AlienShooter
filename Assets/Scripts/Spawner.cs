using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.AI;

[System.Serializable]
public class Enemy
{
    public int count;
    public GameObject Prefab;
}

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private float _checkTime; // Через сколько проверять список активных врагов? (для переноса выключенных врагов в список всех врагов)
    [SerializeField] private float _timeToNewEnemies;
    [SerializeField] private int _enemiesCount;
    [SerializeField] private int _enemiesIncrease;
    [SerializeField] private Transform _player;
    [SerializeField] private float _spawnRadius;
    [SerializeField] private Transform _enemyParent;
    private List<GameObject> _activeEnemies = new List<GameObject>();
    private List<GameObject> _allEnemies = new List<GameObject>();
    private Vector3 _randomPoint;
    private Vector3 _spawnPoint;

    private void Awake()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            for (int j = 0; j < _enemies[i].count; j++)
            {
                GameObject EnemyObject = Instantiate(_enemies[i].Prefab, _enemyParent);
                _allEnemies.Add(EnemyObject);
            }
        }

        _randomPoint.y = 0.1f;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
        StartCoroutine(CheckActiveEnemies());
    }

    /// <summary>
    /// Находим точку которая лежит на навмеше и находится на заданном расстоянии от игрока
    /// </summary>
    private void FindSpawnPoint()
    {
        do
        {
            _randomPoint.x = Random.Range(6.5f, 38.5f);
            _randomPoint.z = Random.Range(-2.5f, 29.5f);
        } while (Vector3.Distance(_randomPoint, _player.position) < _spawnRadius);

        NavMeshHit Hit;
        NavMesh.SamplePosition(_randomPoint, out Hit, 3, NavMesh.AllAreas);
        _spawnPoint = Hit.position;
    }

    /// <summary>
    /// Спаун врагов. Из списка всех врагов мы переносим нужное кол-во врагов в список активных врагов, задаем позицию и активируем
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            FindSpawnPoint();
            if (_enemiesCount > _allEnemies.Count)
            {
                _enemiesCount = _allEnemies.Count;
            }

            for (int i = 0; i < _enemiesCount; i++)
            {
                int Index = Random.Range(0, _allEnemies.Count);
                _activeEnemies.Add(_allEnemies[Index]);
                _allEnemies.RemoveAt(Index);
                int ActiveIndex = _activeEnemies.Count - 1;
                _activeEnemies[ActiveIndex].transform.position = _spawnPoint;
                _activeEnemies[ActiveIndex].transform.LookAt(_player);
                _activeEnemies[ActiveIndex].SetActive(true);
            }

            _enemiesCount += _enemiesIncrease;
            yield return new WaitForSeconds(_timeToNewEnemies);
        }
    }

    /// <summary>
    /// Проверяем список активных врагов. Если враг выключен, то переносим его в список всех врагов
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckActiveEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(_checkTime);
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                if (!_activeEnemies[i].activeSelf)
                {
                    _allEnemies.Add(_activeEnemies[i]);
                    _activeEnemies.RemoveAt(i);
                }
            }
        }
    }
}
