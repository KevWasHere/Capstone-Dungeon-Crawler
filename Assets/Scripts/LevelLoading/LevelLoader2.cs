/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hagen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # LevelLoader2.cs
*-----------------------------------------------------------------------*/
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
* loadingScreen - referennce to UI loading screen object
* slider - reference to slider that represents the loading bar
*
* Creator: Myles Hagen
*/
public class LevelLoader2 : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider slider;

	// load the level that is stored in the globalcontrol newSceneID
	void Start() {
		LoadLevel(GlobalControl.Instance.newSceneID);
	}

	/*
	 * Function: LoadLevel
	 * Parameters - sceneIndex
	 * Description: start corouting to load the scene with a given
	 * index.
	 * 
	 * Creator: Myles Hagen
	 */
	public void LoadLevel(int sceneIndex) {

		StartCoroutine (LoadAsync(sceneIndex));

	}

	/*
	 * Function: LoadAsync
	 * Parameters: SceneIndex
	 * Descrption: Coroutine to load the new level, track the progress of level
	 * being loaded and increase the loading bar to reflect the progress of the level being loaded.
	 * 
	 * Creator: Myles Hagen
	 */
	IEnumerator LoadAsync(int sceneIndex) {

		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneIndex);
		loadingScreen.SetActive (true);

		while (!operation.isDone) {

			float progress = Mathf.Clamp01 (operation.progress / 0.9f);
			slider.value = progress;
			//Debug.Log (progress);
			yield return null;
		}
	}
}
