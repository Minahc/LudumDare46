using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour {
	public Sprite Healthy;
	public Sprite Sick;
	public Morale morale;
	public PeopleCount people;
	public SickPeople sick;
	public GameObject Replay;
	public GameObject Titel;
	public GameObject Person;
	public GameObject SickPerson;
	public Transform peopleList;
	float[] timers = new float[8];
	List<GameObject> amountOfPeople = new List<GameObject>();
	bool victory = false;
	bool loss = false;
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
		for (int i = 0; i < people.nrPeople - sick.nrSick; i++) {
			amountOfPeople.Add(Instantiate(Person, peopleList));
		}
		for (int j = 0; j < sick.nrSick; j++) {
			amountOfPeople.Add(Instantiate(SickPerson, peopleList));
		}
	}

	public void KillSickPerson() {
		for (int i = 0; i < amountOfPeople.Count; i++) {
			if (peopleList.GetChild(i).name == "Sick(Clone)") {
				Destroy(peopleList.GetChild(i).gameObject);
				amountOfPeople.RemoveAt(i);
				return;
			}
		}
	}

	public void MakeSickPerson() {
		for (int i = 0; i < amountOfPeople.Count; i++) {
			if (peopleList.GetChild(i).name == "Person(Clone)") {
				peopleList.GetChild(i).name = "Sick(Clone)";
				peopleList.GetChild(i).GetComponent<SpriteRenderer>().sprite = Sick;
				return;
			}
		}
	}

	public void HealPerson() {
		for (int i = 0; i < amountOfPeople.Count; i++) {
			if (peopleList.GetChild(i).name == "Sick(Clone)") {
				peopleList.GetChild(i).name = "Person(Clone)";
				peopleList.GetChild(i).GetComponent<SpriteRenderer>().sprite = Healthy;
				return;
			}
		}
	}

	public void KillPerson() {
		Destroy(peopleList.GetChild(0).gameObject);
		amountOfPeople.RemoveAt(0);
	}

	public void ToTitle() {
		SceneManager.LoadScene("Start screen");
	}

	public void Win() {
		victory = true;
		Text winObj = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
		winObj.text = "You Defeated The Epidemic";
		Replay.SetActive(true);
		Titel.SetActive(true);
	}

	public void Restart() {
		for (int i = 0; i < timers.Length; i++) {
			timers[i] = 0;
		}
		people.nrPeople = 100;
		sick.nrSick = 20;
		morale.morale = 50;
		Replay.SetActive(false);
		Titel.SetActive(false);
		if (victory) {
			Text winObj = GameObject.FindGameObjectWithTag("Win").GetComponent<Text>();
			winObj.text = "";
			victory = false;
		} else if (loss) {
			Text loseObj = GameObject.FindGameObjectWithTag("Lose").GetComponent<Text>();
			loseObj.text = "";
			loss = false;
		}
		active = ActiveModifier.NONE;
	}

	public void Lose() {
		loss = true;
		Text loseObj = GameObject.FindGameObjectWithTag("Lose").GetComponent<Text>();
		if (people.nrPeople == 0 || sick.nrSick == people.nrPeople) {
			loseObj.text = "You lost. The epidemic has taken over";
		} else if (morale.morale == 0) {
			loseObj.text = "Morale is too low, people have started rioting";
		}
		Replay.SetActive(true);
		Titel.SetActive(true);
	}

    void Update() {
		if (!victory && !loss) {
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

					if (timers[0] > 0.48) {
						sick.nrSick--;
						HealPerson();
						timers[0] = 0;
					}

					if (timers[1] > 0.7) {
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
						KillSickPerson();
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
				MakeSickPerson();
				timers[5] = 0;
			}

			if (timers[6] > 2) {
				people.nrPeople--;
				sick.nrSick--;
				KillSickPerson();
				timers[6] = 0;
			}

			if (morale.morale < 1.5) {
				if (timers[7] > 7) {
					people.nrPeople--;
					KillPerson();
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