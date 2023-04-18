using UnityEngine;

[RequireComponent(typeof(PlayerMovement))] 
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] Camera _mainCamera;
    private Transform _cameraTransform;
    private Animator _animator;
    private Shooting _shooting;
    private PlayerMovement _playerMovement;
    private Vector3 _vertical;
    private Vector3 _horizontal;
    private Vector3 _movement;
    float _horizontalAxis;
    float _verticalAxis;

    private void Awake()
    {
        _shooting = GetComponent<Shooting>();
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
        _cameraTransform = _mainCamera.GetComponent<Transform>();
    }

    void Update()
    {
        _horizontalAxis = Input.GetAxis(GameData.HORIZONTAL_AXIS);
        _verticalAxis = Input.GetAxis(GameData.VERTICAL_AXIS);
        _playerMovement.Move(_horizontalAxis, _verticalAxis);
        _vertical = _verticalAxis * _cameraTransform.up;
        _horizontal = _horizontalAxis * _cameraTransform.right;
        _movement = _horizontal + _vertical;
        _movement.y = 0;
        Animate();

        Plane PlayerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        float HitDist = 0;

        if (PlayerPlane.Raycast(ray, out HitDist))
        {
            Vector3 Target = ray.GetPoint(HitDist);
            Quaternion TargetQ = Quaternion.LookRotation(Target - transform.position);
            TargetQ.x = 0;
            TargetQ.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetQ, _rotationSpeed * Time.deltaTime);
        }

        if (Input.GetMouseButtonDown(0))
        {
            _shooting.Shoot();
        }
    }

    private void Animate()
    {
        Vector3 LocalMove = transform.InverseTransformDirection(_movement);
        _animator.SetFloat("Forward", LocalMove.z, 0.1f, Time.deltaTime);
        _animator.SetFloat("Turn", LocalMove.x, 0.1f, Time.deltaTime);
    }
}
