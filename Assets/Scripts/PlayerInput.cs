using UnityEngine;

public class PlayerInput : MonoBehaviour {
	private Paddle _paddle;

	private void Awake() {
		_paddle = GetComponent<Paddle>();
	}

	private void Update() {
		Vector2 movement = Vector2.zero;
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			movement.x = -1;
		} else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			movement.x = 1;
		}

		_paddle.SetMovement(movement);
	}
}