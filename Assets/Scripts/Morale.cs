using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Morale : MonoBehaviour {
	public int morale = 50;
	Image moraleImage;
	Text moraleNr;
	public Sprite happy;
	public Sprite neutral;
	public Sprite sad;
    void Start() {
		moraleImage = GetComponentInChildren<Image>();
		moraleNr = GetComponentInChildren<Text>();
    }


    void Update() {
		moraleNr.text = "" + morale;
		if (morale > 75) {
			moraleImage.sprite = happy;
		}
		if (morale <= 75 && morale >= 20) {
			moraleImage.sprite = neutral;
		}
		if (morale < 20) {
			moraleImage.sprite = sad;
		}
		
    }
}
