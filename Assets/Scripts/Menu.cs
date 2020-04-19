using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	public GameObject tutorial;
	public GameObject Story;
    void Start() {
		tutorial.SetActive(false);
		Story.SetActive(false);
    }

	public void OpenTutorial() {
		tutorial.SetActive(true);
	}

	public void CloseTutorial() {
		tutorial.SetActive(false);
	}

	public void OpenStory() {
		Story.SetActive(true);
	}

	public void CloseStory() {
		Story.SetActive(false);
	}

	public void StartGame() {
		SceneManager.LoadScene("Game");
	}

	public void ExitGame() {
		Application.Quit();
	}

    void Update() {
			
    }
}
