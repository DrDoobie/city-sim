using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

	public int day;
	public float hour = 0.0f, minute = 0.0f, second = 0.0f, secondChange, changeMultiplier;
	public float money = 0.0f;
	public Text timeText, moneyText;

	string LeadingZero (int n) {
    	return n.ToString().PadLeft(2, '0');
  	}

	private void Update () {
		TextController();
		TimeController();
	}

	private void TextController () {
		moneyText.text = "Money: " + "$" + money;
	}

	private void TimeController () {
		second += ((secondChange * changeMultiplier) * Time.deltaTime);

		if(second >= 60.0f)
		{
			minute += 1.0f;
			second = 0.0f;
		}

		if(minute >= 60.0f)
		{
			hour += 1.0f;
			minute = 0.0f;
		}

		if(hour >= 24.0f)
		{
			day += 1;
			hour = 0.0f;
			minute = 0.0f;
			second = 0.0f;
		}

		timeText.text = "Day: " + day + " - " + hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
	}
}
