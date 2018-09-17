/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 Final Project
  # SceneController.cs
*-----------------------------------------------------------------------*/
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/*Author: Yan Zhang, Tianqi Xiao
 * Class : SceneController
 * 
 * Description:
 *              SceneController is working on load scene according to the sceneName     
 *              the fading the current scene and load the specific scene
 */
public class SceneController : MonoBehaviour
{
    public event Action BeforeSceneUnload;                      // action for before scene load
    public event Action AfterSceneLoad;                         // action for ater scene load 

    public CanvasGroup faderCanvasGroup;                        // fade canvas group
    public float fadeDuration = 1f;                             // fader duration
    public string startingSceneName = "MylesTest";              // scene name
    public string initialStartingPositionName = "DoorToMarket";     // scene name
    public SaveData playerSaveData;                             // player local data

	private bool isFading;                                      // boolean value determine fading

    // start load the scene name and set active
    private IEnumerator Start ()
    {
        faderCanvasGroup.alpha = 1f;

        //playerSaveData.Save (PlayerMovement.startingPositionKey, initialStartingPositionName);

		yield return StartCoroutine (LoadSceneAndSetActive (startingSceneName));

		StartCoroutine (Fade (0f));
    }

    // Fade the current scene and load a scene
    public void FadeAndLoadScene (SceneReaction sceneReaction)
    {
		if (!isFading) {
			StartCoroutine (FadeAndSwitchScenes (sceneReaction.sceneName));
		}
    }

    /// <summary>
    /// load between scene
    /// </summary>
    /// <param name="sceneName"> the name of scene</param>
    /// <returns></returns>
	private IEnumerator FadeAndSwitchScenes(string sceneName)
	{
		yield return StartCoroutine (Fade (1f));

		if (BeforeSceneUnload != null) {
			BeforeSceneUnload ();
		}

		yield return SceneManager.UnloadSceneAsync (SceneManager.GetActiveScene ().buildIndex);

		yield return StartCoroutine (LoadSceneAndSetActive (sceneName));

		if (AfterSceneLoad != null) {
			AfterSceneLoad ();
		}

		yield return StartCoroutine (Fade (0f));

	}

    // load the scene and set it active
	private IEnumerator LoadSceneAndSetActive(string sceneName)
	{
		yield return SceneManager.LoadSceneAsync (sceneName, LoadSceneMode.Additive);

		Scene newlyLoadedScene = SceneManager.GetSceneAt (SceneManager.sceneCount - 1);
		SceneManager.SetActiveScene (newlyLoadedScene);

	}

    // fade the canvas
	private IEnumerator Fade(float finalAlpha)
	{
		isFading = true;
		faderCanvasGroup.blocksRaycasts = true;

		float fadeSpeed = Mathf.Abs (faderCanvasGroup.alpha - finalAlpha) / fadeDuration;

		while (!Mathf.Approximately(faderCanvasGroup.alpha, finalAlpha)) {

			faderCanvasGroup.alpha = 
				Mathf.MoveTowards (faderCanvasGroup.alpha, finalAlpha, fadeSpeed * Time.deltaTime);

			yield return null;
		}

		isFading = false;
		faderCanvasGroup.blocksRaycasts = false;
	}
}
