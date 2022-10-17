using UnityEngine;
using TMPro;
using LedCSharp;

public class GameManager : MonoBehaviour {
	public static GameManager instance { get; private set; }

	public TMP_Text scoreUI;
	public TMP_Text livesUI;
	public Wall wall;
	public Ball ball;
	public Paddle paddle;

	public int score;
	public int lives = 3;
	public int deathPenalty = 100;

	public int level { get; private set; } = 1;

	private void Awake() {
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;
	}

	private void Start() {
		ball.StartAcceleration();
		scoreUI.text = score.ToString();
		livesUI.text = lives.ToString();
	}

	public void AddScore(int value) {
		score += value;
		scoreUI.text = score.ToString();
	}

	public void BallOut() {
		lives--;
		livesUI.text = lives.ToString();

		if (lives <= 0) {
			GameOver();
		} else {
			ball.ResetBall();
			AddScore(-deathPenalty);
		}
	}

	public void GameOver() {
		ball.gameObject.SetActive(false);
		Invoke(nameof(ResetGame), 5f);
	}

	private void ResetGame() {
		level = 1;
		score = 0;
		lives = 3;
		ball.gameObject.SetActive(true);
		ball.ResetBall();
		paddle.Reset();
		wall.ResetField();
		scoreUI.text = score.ToString();
		livesUI.text = lives.ToString();
	}

	public void NextLevel() {
		level++;
		ball.gameObject.SetActive(false);
		ball.ResetBall();
		paddle.Reset();
		wall.ResetField();
	}
}