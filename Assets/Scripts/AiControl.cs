using UnityEngine;

public class AiControl : MonoBehaviour {
	public Transform target;
	public float triggerTheshold = 1f;

	private Paddle _paddle;

	private void Awake() {
		_paddle = GetComponent<Paddle>();
	}

	private void Update() {
		Vector2 movement = Vector2.zero;
		if (transform.position.x - target.position.x > triggerTheshold) {
			movement.x = -1;
		} else if (transform.position.x - target.position.x < -triggerTheshold) {
			movement.x = 1;
		}

		_paddle.SetMovement(movement);
	}
}