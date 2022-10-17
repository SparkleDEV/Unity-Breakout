using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Paddle : MonoBehaviour {
	public float speed = 5f;

	private Rigidbody2D _rigidbody;
	private Vector2 _movement;
	private Vector3 _initialPosition;

	private void Awake() {
		_rigidbody = GetComponent<Rigidbody2D>();
		_initialPosition = transform.position;
	}

	private void FixedUpdate() {
		_rigidbody.AddForce(_movement * speed);
	}

	public void Reset() {
		_rigidbody.velocity = Vector2.zero;
		transform.position = _initialPosition;
	}

	public void SetMovement(Vector2 input) {
		_movement = input;
	}
}