using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SickPeople : MonoBehaviour {
	public int nrSick = 10;
	Text sick;
    void Start() {
		sick = GetComponentInChildren<Text>();
    }

    void Update() {
		sick.text = "" + nrSick;
    }
}
