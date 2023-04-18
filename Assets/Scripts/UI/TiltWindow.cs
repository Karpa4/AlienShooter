using UnityEngine;

public class TiltWindow : MonoBehaviour
{
	private Vector2 _range = new Vector2(5f, 3f);
	private Transform _transform;
	private Quaternion _localRotation;
	private Vector2 _rotate = Vector2.zero;

	private void Start()
	{
		_transform = transform;
		_localRotation = _transform.localRotation;
	}

	private void Update()
	{
		Vector3 pos = Input.mousePosition;

		float halfWidth = Screen.width * 0.5f;
		float halfHeight = Screen.height * 0.5f;
		float x = Mathf.Clamp((pos.x - halfWidth) / halfWidth, -1f, 1f);
		float y = Mathf.Clamp((pos.y - halfHeight) / halfHeight, -1f, 1f);
		_rotate = Vector2.Lerp(_rotate, new Vector2(x, y), Time.deltaTime * 5f);

		_transform.localRotation = _localRotation * Quaternion.Euler(-_rotate.y * _range.y, _rotate.x * _range.x, 0f);
	}
}
