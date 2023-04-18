using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    private bool onPauseButton = false; // Находится ли курсор мыши на кнопке паузы
    [SerializeField] private int _ammo;
    [SerializeField] private float _shootDelayTime; // Задержка между выстрелами
    [SerializeField] private float _maxDistance;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private Text _ammoText;
    [SerializeField] private AudioSource _audioSource;
    private bool _isCanShoot;
    private RaycastHit _enemyHit;
    private Vector3 _rayOffset;

    private void Awake()
    {
        _rayOffset = new Vector3(0, 1.3f, 0);
        _isCanShoot = true;
    }

    /// <summary>
    /// Свойство onPauseButton
    /// </summary>
    public bool OnPauseButton
    {
        set { this.onPauseButton = value; }
    }

    /// <summary>
    /// Используется для стрельбы и при подборе боеприпасов
    /// </summary>
    /// <param name="ammoCount">Кол-во патронов</param>
    public void ChangeAmmo(int ammoCount)
    {
        _ammo -= ammoCount;
        _ammoText.text = _ammo.ToString();
    }

    /// <summary>
    /// Стрельба
    /// </summary>
    public void Shoot()
    {
        if (!onPauseButton)
        {
            if (_ammo > 0 && _isCanShoot)
            {
                _isCanShoot = false;
                _muzzleFlash.Play();
                _audioSource.Play();
                if (Physics.Raycast(transform.position + _rayOffset, transform.forward, out _enemyHit, _maxDistance, _enemyLayer))
                {
                    _enemyHit.transform.gameObject.GetComponent<Health>().TakeDamage(1);
                }
                ChangeAmmo(1);
                StartCoroutine(DelayBetweenShots());
            }
        }
    }

    private IEnumerator DelayBetweenShots()
    {
        yield return new WaitForSeconds(_shootDelayTime);
        _isCanShoot = true;
    }
}
