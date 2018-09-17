using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang,Yunzheng Zhou
Moving a game object from one point to another smoothly
puzzle: is the game object it that need to move
bridge: enable the bridge when the game win
player: the player
puzzleCam: the puzzle camera that looking at the puzzle
main: the main camera
start:the start point of the object moving
end: the end point of the boject moving
distance: the terget distanceof moving
lerpTime: the lerping time from start to end
currentTime: the current time when moving start
ready: the winning check bool
*/
public class LerpMoving : MonoBehaviour {
	public GameObject puzzle;
	public GameObject bridge;
	public GameObject player;
	public Camera puzzleCam;
	public Camera main;

	private Vector3 start;
	private Vector3 end;
	//total distance
	public float distance = 50f;
	//total time from start to end
	private float lerpTime = 5;
	//update the lerp time
	private float currentTime = 0;

	private bool ready = false;
	// Use this for initialization
	void Start () {
		start = puzzle.transform.position;
		end = puzzle.transform.position + Vector3.down * distance;
	}
	
	/* Creator: Yan, Yunzheng Zhou
	 */
	void Update () {
		//if ready is true move the puzzle 
		if (ready == true) {
			currentTime += Time.deltaTime;
			if (currentTime == lerpTime) {
				currentTime = lerpTime;
				Debug.Log (puzzle.transform.position);
			}
			float p = currentTime / lerpTime;
			puzzle.transform.position = Vector3.Lerp (start, end, p);		
		}
		//if game finisheddisble the puzzle and enable the bridge
		if (puzzle.transform.position == end) {
			//Debug.Log ("stop");
			puzzle.SetActive (false);
			bridge.SetActive (true);
			main.enabled = true;
			puzzleCam.enabled = false;

		}
		if (puzzle.activeSelf == false) {
			
		}
	}

	/*Creator: Yan, Yunzheng Zhou
	 * set ready to true when collider is triggered
	 */
	void OnTriggerEnter(Collider other){
		//Debug.Log ("end");
		ready = true;

	}
}
