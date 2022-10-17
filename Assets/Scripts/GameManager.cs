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
	public Color[] liveColors;

	public int score;
	public int maxlives = 3;
	public int deathPenalty = 100;

	public int level { get; private set; } = 1;

	private int lives;

	private void Awake() {
		if (instance != null && instance != this)
			Destroy(this);
		else
			instance = this;

		LogitechGSDK.LogiLedInit();
	}

	private void Start() {
		lives = maxlives;
		ball.StartAcceleration();
		scoreUI.text = score.ToString();
		livesUI.text = lives.ToString();
		SetLiveColor();
		Cursor.visible = false;
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
			SetLiveColor();
		}
	}

	public void GameOver() {
		ball.gameObject.SetActive(false);
		Invoke(nameof(ResetGame), 5f);
	}

	private void ResetGame() {
		level = 1;
		score = 0;
		lives = maxlives;
		ball.gameObject.SetActive(true);
		ball.ResetBall();
		paddle.Reset();
		wall.ResetField();
		scoreUI.text = score.ToString();
		livesUI.text = lives.ToString();
		SetLiveColor();
	}

	public void NextLevel() {
		level++;
		lives = maxlives;
		livesUI.text = lives.ToString();
		ball.gameObject.SetActive(false);
		ball.ResetBall();
		paddle.Reset();
		wall.ResetField();
		SetLiveColor();
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.Quit();
		}
	}

	private void SetLiveColor() {
		Color color = lives > liveColors.Length || lives <= 0 ? Color.white : liveColors[lives - 1];
		LogitechGSDK.LogiLedSetLighting(Mathf.RoundToInt(color.r * 100), Mathf.RoundToInt(color.g * 100), Mathf.RoundToInt(color.b * 100));
	}

	private void OnApplicationQuit() {
		LogitechGSDK.LogiLedShutdown();
	}
}