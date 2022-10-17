using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Brick : MonoBehaviour {
	public int maxlives = 1;
	public int lives = 1;
	public Color[] levelColors;
	public int hitPoints = 10;
	public int destroyPoints = 50;

	private Wall wall;
	private SpriteRenderer _spriteRenderer;

	private void Awake() {
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void Initialize(Wall wall, int lives) {
		this.wall = wall;
		this.lives = lives;
		this.maxlives = lives;

		UpdateColor();
	}

	public void Reset() {
		lives = maxlives;
		UpdateColor();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag != "Ball") return;

		lives--;

		if (lives <= 0) {
			wall.DestroyedBrick(this);
		} else {
			wall.HitBrick(this);
			UpdateColor();
		}
	}

	private void UpdateColor() {
		if (levelColors.Length < lives) {
			_spriteRenderer.color = Color.white;
			return;
		}

		_spriteRenderer.color = levelColors[lives - 1];
	}
}