using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePerson : MonoBehaviour {
	private Vector3 oldPos;
	private Vector3 newPos;
	private float time;
	private float maxTime;
	private float width;
	private float height; 

	void Start() {
        width =  Camera.main.ViewportToWorldPoint(Vector3.one).x;
		height = Camera.main.ViewportToWorldPoint(Vector3.one).y;
		newPos = transform.position = new Vector3((Random.value * 2f - 1) * Camera.main.ViewportToWorldPoint(Vector3.one).x, (Random.value * 2f - 1) * Camera.main.ViewportToWorldPoint(Vector3.one).y);
	}

    void Update() {
		time += Time.deltaTime;
		if (time > maxTime) {
			time = 0;
			oldPos = newPos;
			do {
				newPos += new Vector3(Random.Range(1, 4) * (Random.value >= 0.5 ? 1 : -1), Random.Range(1, 4) * (Random.value >= 0.5 ? 1 : -1));
				if (newPos.x > width) {
					newPos.x = width;
				} else if (newPos.x < -width) {
					newPos.x = -width;
				}
				if (newPos.y > height) {
					newPos.y = height;
				} else if (newPos.y < -height) {
					newPos.y = -height;
				}
				maxTime = (newPos - oldPos).magnitude;
			} while (maxTime < 0.01);
		}
		transform.position = Vector3.Lerp(oldPos, newPos, time / maxTime);
	}
}
