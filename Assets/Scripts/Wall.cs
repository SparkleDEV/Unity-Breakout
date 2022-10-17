using UnityEngine;
using System.Collections.Generic;

public class Wall : MonoBehaviour {
	public Brick brickPrefab;
	public int rows = 5;
	public int cols = 5;
	public float rowGap = 1;
	public float colGap = 1;
	public int stageCompletePoints = 250;

	private int _destroyedBricks = 0;
	private List<Brick> _bricks;

	private int totalBricks {
		get {
			return rows * cols;
		}
	}

	private float leftPosition {
		get {
			return -((((float)cols / 2f) - .5f) * (float)colGap);
		}
	}

	private void Awake() {
		_bricks = new List<Brick>();
	}

	private void Start() {
		BuildWall();
	}

	public void BuildWall() {
		for (int y = 0; y < rows; y++) {
			float ypos = rowGap * (y + 1);

			for (int x = 0; x < cols; x++) {
				Brick brick = Instantiate(brickPrefab, transform);
				brick.Initialize(this, y + 1);
				float xpos = leftPosition + (colGap * x);
				brick.transform.localPosition = new Vector3(xpos, ypos, 0);
				_bricks.Add(brick);
			}
		}
	}

	public void HitBrick(Brick brick) {
		GameManager.instance.AddScore(brick.hitPoints);
	}

	public void DestroyedBrick(Brick brick) {
		_destroyedBricks++;
		brick.gameObject.SetActive(false);

		GameManager.instance.AddScore(brick.destroyPoints);

		if (_destroyedBricks >= totalBricks) {
			StageComplete();
		}
	}

	public void StageComplete() {
		GameManager.instance.AddScore(stageCompletePoints);
		GameManager.instance.NextLevel();
	}

	public void ResetField() {
		_destroyedBricks = 0;
		foreach (Brick brick in _bricks) {
			brick.gameObject.SetActive(true);
			brick.Reset();
		}
	}
}