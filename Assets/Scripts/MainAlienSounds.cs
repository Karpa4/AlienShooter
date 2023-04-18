using System.Collections;
using UnityEngine;

public class MainAlienSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private int _minDelay;
    [SerializeField] private int _maxDelay;
    private AudioSource _audioSource;
    private int _currentDelay;
    private int _index;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        while (true)
        {
            _currentDelay = Random.Range(_minDelay, _maxDelay);
            _index = Random.Range(0, _clips.Length);
            yield return new WaitForSeconds(_currentDelay);
            _audioSource.clip = _clips[_index];
            _audioSource.Play();
        }
    }
}
