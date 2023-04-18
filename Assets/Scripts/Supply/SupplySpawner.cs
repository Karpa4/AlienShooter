using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SupplySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _supply;
    [SerializeField] private float _timeToSupply;
    private Vector3 _randomPoint;
    private Vector3 _spawnPoint;

    private void Awake()
    {
        StartCoroutine("SpawnSupply");
    }

    /// <summary>
    /// Вызывается при подборе аптечки или патронов
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnSupply()
    {
        yield return new WaitForSeconds(_timeToSupply);
        FindSpawnPoint();
        _supply.transform.position = _spawnPoint;
        _supply.SetActive(true);
    }

    private void FindSpawnPoint()
    {
        _randomPoint.x = Random.Range(9f, 35f);
        _randomPoint.z = Random.Range(0f, 27f);
        NavMeshHit Hit;
        NavMesh.SamplePosition(_randomPoint, out Hit, 3, NavMesh.AllAreas);
        _spawnPoint = Hit.position;
        _spawnPoint.y = 0.285f;
    }
}
