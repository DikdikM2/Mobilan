using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

	public Text timerText;
	private float startTime;

	bool keepTiming;
	float timer;
	bool trigger;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
       // score = 0;
        //UpdateScore();
        StartCoroutine(SpawnWaves());
		StartTimer();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
				Destroy (GameObject.Find ("Game Controller"));
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
			//DontDestroyOnLoad(gameObject);
        }
		if (gameOver)
		{
			restartText.text = "Press 'R' for Restart";
			restart = true;
			//break;
		}
		if(trigger){
			Debug.Log("Timer stopped at " + TimeToString(StopTimer()));
			//timerText.text = " " + TimeToString(timer);
		}

		if(keepTiming){
			UpdateTime();
		}
    }
	void UpdateTime(){
		timer = Time.time - startTime;
		timerText.text = TimeToString(timer);
	}

	public float StopTimer(){
		keepTiming = false;
		return timer;

	}
	void StartTimer(){
		keepTiming = true;
		startTime = Time.time;
	}

	string TimeToString(float t){
		string minutes = ((int) t / 60).ToString();
		string seconds = (t % 60 ).ToString("f2");
		return "Time = " + minutes + ":" + seconds;
	}

	private void OnTriggerEnter(Collider other){
		trigger = true;
		//restart = true;
	}

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            
        }
    }

    /*public void AddScore(int newScoreValue)
    {
        score += 1;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }*/

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}