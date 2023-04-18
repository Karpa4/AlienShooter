using UnityEngine;

public class MedBox : MonoBehaviour
{
    [SerializeField] GameObject _mainObject;
    [SerializeField] private SupplySpawner _supply;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health))
        {
            if (health.HealByMed())
            {
                _audioSource.Play();
                _supply.StartCoroutine("SpawnSupply");
                _mainObject.SetActive(false);
            }
        }
    }
}
