using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoop : MonoBehaviour {
	public Morale morale;
	public PeopleCount people;
	public SickPeople sick;
	public GameObject Replay;
	float[] timers = new float[8];
	bool victory = false;
	bool loss = false;
	bool startScreen = false;
	public enum ActiveModifier {
		NONE,
		HOSPITAL,
		MILITARY,
		CHRUCH,
		NUM
	}

	public ActiveModifier active;

	public void SetActive(int active) {
		if (this.active == (ActiveModifier)active) {
			this.active = ActiveModifier.NONE;
		} else { 
			this.active = (ActiveModifier)active;
			}
	}

    void Start() {
		
    }

	public void Win() {
		victory = true;
		Text winObj = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
		winObj.text = "You Defeated The Epidemic";
		Replay.SetActive(true);
	}

	public void Restart() {
		for (int i = 0; i < timers.Length; i++) {
			timers[i] = 0;
		}
		people.nrPeople = 100;
		sick.nrSick = 1;
		morale.morale = 100;
		if (victory) {
			Text winObj = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
			winObj.text = "";
			Replay.SetActive(false);
			victory = false;
		} else if (loss) {
			Text winObj = GameObject.FindGameObjectWithTag("Lose").GetComponent<Text>();
			winObj.text = "";
			Replay.SetActive(false);
			loss = false;
		}
		active = ActiveModifier.NONE;
	}

	public void Lose() {
		loss = true;
		Text winObj = GameObject.FindGameObjectWithTag("Lose").GetComponent<Text>();
		winObj.text = "You lost. The epidemic has taken over";
		Replay.SetActive(true);
	}

    void Update() {
		if (!victory && !loss && !startScreen) {
			switch (active) {
				case ActiveModifier.HOSPITAL:
					if (sick.nrSick <= 0) {
						Win();
					}

					if (people.nrPeople <= 0) {
						Lose();
					}

					if (sick.nrSick == people.nrPeople) {
						Lose();
					}

					if (timers[0] > 0.45) {
						sick.nrSick--;
						timers[0] = 0;
					}

					if (timers[1] > 1) {
						morale.morale--;
						timers[1] = 0;
					}

					break;
				case ActiveModifier.MILITARY:
					if (sick.nrSick <= 0) {
						Win();
					}

					if (people.nrPeople <= 0) {
						Lose();
					}

					if (sick.nrSick == people.nrPeople) {
						Lose();
					}

					if (timers[2] > 0.2) {
						sick.nrSick--;
						people.nrPeople--;
						timers[2] = 0;
					}

					if (timers[3] > 0.1) {
						morale.morale--;
						timers[3] = 0;
					}

					break;
				case ActiveModifier.CHRUCH:
					if (people.nrPeople <= 0) {
						Lose();
					}

					if (sick.nrSick == people.nrPeople) {
						Lose();
					}

					if (timers[4] > 0.2) {
						morale.morale++;
						timers[4] = 0;
					}

					break;
			}
			if (people.nrPeople <= 0) {
				Lose();
			}

			if (sick.nrSick == people.nrPeople) {
				Lose();
			}

			if (timers[5] > 0.5) {
				sick.nrSick++;
				timers[5] = 0;
			}

			if (timers[6] > 2) {
				people.nrPeople--;
				sick.nrSick--;
				timers[6] = 0;
			}

			if (morale.morale < 1.5) {
				if (timers[7] > 7) {
					people.nrPeople--;
					timers[7] = 0;
				}
			}

			if (morale.morale <= 0) {
				Lose();
			}

			for (int i = 0; i < timers.Length; i++) {
				timers[i] += Time.deltaTime;
			}
		}
    }
}