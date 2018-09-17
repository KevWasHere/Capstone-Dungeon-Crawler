using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 498 Final Project
 # LoadingScreenControl.cs
 # Load the loading screen
 *-----------------------------------------------------------------------*/

/*
 * Creator: Yunzheng Zhou, Tianqi Xiao, Myles Hagen
 *
 * loadingScreenObj: reference to a loadingScreen GameObject
 * slider: reference to Slider
 * async: reference to AsyncOperation
 */
public class LoadingScreenControl : MonoBehaviour {

	public GameObject loadingScreenObj;
	public Slider slider;

	AsyncOperation async;

	/*
	 * Creator: Yunzheng Zhou
	 * Start Coroutine to load the LoadingScreen
	 */
	public void LoadScreenExample() {

		StartCoroutine(LoadingScreen());
	}

	/*
	 * Creator: Yunzheng Zhou, Tianqi Xiao, Myles Hagen
	 * Load loadingScrenn Asynchronously.
	 */
	IEnumerator LoadingScreen() {

		loadingScreenObj.SetActive (true);
		async = SceneManager.LoadSceneAsync (0);
		async.allowSceneActivation = false;
		while (async.isDone == false) {

			slider.value = async.progress;
			if (async.progress == 0.9f) {

				slider.value = 1f;
				async.allowSceneActivation = true;
			}

		}
		yield return null;
	}
}
