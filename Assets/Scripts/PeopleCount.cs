using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleCount : MonoBehaviour {
	Text people;
	[HideInInspector]
	public int nrPeople = 100;
    void Start() {
		people = GetComponentInChildren<Text>();
    }

    void Update() {
		people.text = "" + nrPeople;

    }
}
