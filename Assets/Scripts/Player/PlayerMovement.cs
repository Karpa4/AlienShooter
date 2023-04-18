using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private AudioSource _footAudio;
    [SerializeField] private float _soundDelay;
    [SerializeField] private AudioClip[] _audioClips;
    private bool CanPlaySound = true;
    private CharacterController _controller;
    private Vector3 _direction;


    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Движение игрока. Вызывается из PlayerInput
    /// </summary>
    /// <param name="x">Значение горизонтальной оси</param>
    /// <param name="y">Значение вертикальной оси</param>
    public void Move(float x, float y)
    {
        _direction = new Vector3(x, 0, y);
        _direction = Vector3.ClampMagnitude(_direction, 1);
        _controller.Move(_direction * _speed * Time.deltaTime);
    }

    /// <summary>
    /// Вызывается в евенте анимации ходьбы
    /// </summary>
    public void PlayFootStep()
    {
        if (CanPlaySound && _direction != Vector3.zero)
        {
            _footAudio.clip = _audioClips[Random.Range(0, _audioClips.Length)];
            _footAudio.Play();
            StartCoroutine(SoundDelay());
        }
    }

    private IEnumerator SoundDelay()
    {
        CanPlaySound = false;
        yield return new WaitForSeconds(_soundDelay);
        CanPlaySound = true;
    }
}
