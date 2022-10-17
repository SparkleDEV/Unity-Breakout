using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour {
	public float baseSpeed = 50f;
	public float horizontalMultiplier = .5f;
	public float resetTime = 3f;
	public float horizontalResetThreshold = .005f;
	public float levelSpeedMultiplier = .05f;

	public new Rigidbody2D rigidbody { get; private set; }
	public Vector3 initialPosition { get; private set; }

	private bool _isResetting = true;
	private float _critYTime = 0;

	private float speed {
		get {
			return baseSpeed * (1 + (levelSpeedMultiplier * (GameManager.instance.level - 1)));
		}
	}

	private void Awake() {
		rigidbody = GetComponent<Rigidbody2D>();
		initialPosition = transform.position;
	}

	public void StartAcceleration() {
		Invoke(nameof(Accelerate), resetTime);
	}

	private void FixedUpdate() {
		if (Mathf.Abs(rigidbody.velocity.y) <= horizontalResetThreshold && !_isResetting) {
			_critYTime += Time.fixedDeltaTime;
		} else if (Mathf.Abs(rigidbody.velocity.y) > horizontalResetThreshold) {
			_critYTime = 0;
		}

		if (_critYTime >= 5f) {
			ResetBall();
			_critYTime = 0;
		}
	}

	public void ResetBall() {
		_isResetting = true;
		rigidbody.velocity = Vector2.zero;
		transform.position = initialPosition;
		gameObject.SetActive(true);
		StartAcceleration();
	}

	private void Accelerate() {
		Vector2 acceleration = Vector2.down;

		acceleration.x = (Random.value * 2) - 1;
		acceleration.Normalize();

		rigidbody.AddForce(acceleration * speed);
		Invoke(nameof(StopResetState), 3f);
	}

	private void StopResetState() {
		_isResetting = false;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Paddle") {
			CollideWithPaddle(other);
		} else if (other.gameObject.tag == "Border Out") {
			GameManager.instance.BallOut();
		}
	}

	private void CollideWithPaddle(Collision2D other) {
		Vector3 direction = transform.position - other.transform.position;
		direction.z = 0;
		direction.Normalize();
		direction.y = 0;

		direction *= horizontalMultiplier;

		Vector3 velocity = rigidbody.velocity.normalized;

		Vector3 force = (velocity + direction).normalized;

		rigidbody.velocity = Vector2.zero;

		rigidbody.AddForce(force * speed);
	}
}