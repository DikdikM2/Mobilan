using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Waktu : MonoBehaviour
{
	public Text timerText;
	private float startTime;

	bool keepTiming;
	float timer;
	bool trigger;

	/*void Awake(){
		DontDestroyOnLoad (gameObject);
		DontDestroyOnLoad (GameObject.Find ("Canvas"));
	}
*/
	void Start () {
		StartTimer();
	}

	void Update () {
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

	float StopTimer(){
		keepTiming = false;
		return timer;
	}

	/*void ResumeTimer(){
		keepTiming = true;
		startTime = Time.time-timer;
	}*/

	void StartTimer(){
		keepTiming = true;
		startTime = Time.time;
	}

	string TimeToString(float t){
		string minutes = ((int) t / 60).ToString();
		string seconds = (t % 60 ).ToString("f2");
		return "Waktu Anda = " + minutes + ":" + seconds;
	}

	private void OnTriggerEnter(Collider col){
		trigger = true;
	}
}
