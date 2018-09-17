using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*-------------------------------------------------------------------------*
 # INTR Group 2
 # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya,
 #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
 # CMPT 498 Final Project
 # LevelLoader.cs
 # Load levels
 *-----------------------------------------------------------------------*/

/*
 * Creator: Yan Zhang, Shane Weerasuriya
 *
 * loadingScreen: reference to a loadingScreen GameObject
 * slider: reference to Slider
 * progress: a float variable indicating progress
 */
public class LevelLoader : MonoBehaviour {
    public GameObject loadingScreen;
    public Slider slider;
    float progress;
    
	//New game start from the first level
	void Start()
    {
        LoadLevel(1);
    }

	/*
	 * Creator: Yan Zhang, Shane Weerasuriya
	 * Load level by its scene index.
	 */
	public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    /*void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 40, Screen.height - 500, 30, 30), "Play"))
        {
            LoadLevel(0);
        }
    }*/

	/*
	 * Creator: Yan Zhang
	 * Load Scene Asynchronously.
	 */
    IEnumerator LoadAsynchronously(int sceneIdex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIdex);

        while (!operation.isDone)
        {
            
            slider.value = operation.progress;
            yield return null;
            
        }
        
    }
}
