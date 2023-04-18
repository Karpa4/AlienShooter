using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    [SerializeField] private GameObject _mainObject;
    [SerializeField] private SupplySpawner _supply;
    [SerializeField] private AudioSource _audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Shooting>(out Shooting shooting))
        {
            _audioSource.Play();
            shooting.ChangeAmmo(-50);
            _supply.StartCoroutine("SpawnSupply");
            _mainObject.SetActive(false);
        }
    }
}
