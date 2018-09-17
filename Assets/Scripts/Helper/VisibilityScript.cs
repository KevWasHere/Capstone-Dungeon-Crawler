using UnityEngine;
using System.Collections;

/** Visibility Script
 * 
 * This script controls the visibility of an object in the line of sight between the main camera and
 * the player.
 * 
 * Author:  B. Brookwell
 * Date:	11-02-2015
 * 
 * The script's MakeInvisible is called by the camera control script for every frame the object lies between
 * the player and the camera.  Once the object has been untagged for more than FADE_TIME, it will fade back in
 * from INVISIBILITY
 * 
 * +---------+    1    +--------+   2    +---------+
 * | VISIBLE |-------->|FADE_OUT|------->|INVISIBLE|
 * |         |         |        |        |         |
 * +---------+         +--------+        +---------+
 *      |                 ^                   |
 *      |               4 |                   |
 *      |      5       +--------+      3      |
 *      +--------------|FADE_IN |<------------+
 *                     +--------+
 * 
 * 1)  In LOS between player and camera
 * 2)  Fade Out Completed
 * 3)  Not in LOS
 * 4)  In LOS
 * 5)  Not In LOS and Fade In Completed
 * 
 * State		Description
 * 
 * VISIBLE		Object is not in LOS and completely visible
 * FADE_OUT		Object is in LOS and is fading towards invisibility
 * INVISIBLE	Object is in LOS and has alpha = 0
 * FADE_IN		Object is not in LOS and is becoming visible
 * 
 * Constant		Description
 * 
 * FADE_TIME	Time in seconds to fade to invisibility (or the reverse)
 * FADE_RATE	Multiplier used in LERP to scale time
 * MIN_ALPHA	Alpha of object when INVISIBLE
 * 
 * Variable		Description
 * 
 * lastHitTime	Time when object was last declared invisible
 * startFade	Time when object started to fade or started to return to normal
 * current		Initial (non-invisible) color
 * invisible	Invisible color
 * state		State of object (see above)
 */

public class VisibilityScript : MonoBehaviour {
	public enum State {VISIBLE, FADE_OUT, INVISIBLE, FADE_IN};

	private const float FADE_TIME	= 0.2f;
	//private const float FADE_RATE	= 1f / FADE_TIME;
	//private const float MIN_ALPHA  	= 0.25f;

	private float lastHitTime 		= 0;
	private float startFade 		= 0;

	//private Color current, invisible;
	private State state = State.VISIBLE;
	
	Renderer wall;

	/**
	 * Start -- Initialization:  sets up normal and invisible color.  Sets state to normal
	 */
	void Start () {
		wall = this.gameObject.GetComponent<Renderer>();
		//current = GetComponent<Renderer>().material.color;
		//invisible = new Color (current.r, current.g, current.b, MIN_ALPHA);
		state = State.VISIBLE;
	}

	/**
	 * MakeInvisible -- Called to indicate the object is in LOS.
	 * 
	 */

	public void MakeInvisible () {
		switch (state) {
		case State.VISIBLE:
			//wall.enabled = true;
			state = State.FADE_OUT;
			startFade = Time.time;
			break;
		case State.FADE_IN:
			state = State.FADE_OUT;
			startFade = 2f * Time.time - startFade - 1f;
			break;
		case State.INVISIBLE:
		case State.FADE_OUT:
			lastHitTime = Time.time;
			break;
		}
	}

	/**
	 * Update.  Called once per frame to handle changes in transparency due to the object's state.  VISIBLE
	 * state is ignored because no changes are required.  Only MakeVisible handles the VISIBLE state.
	 */

	void Update () {
		//float delta;
		switch (state) {
		case State.FADE_OUT:
			//delta = FADE_RATE * (Time.time - startFade);
			//Debug.Log("Fade Out Delta: " + delta);
			//GetComponent<Renderer>().material.color = Color.Lerp (current, invisible, delta);
			//if (delta >= 1f) {
				wall.enabled = false;
				state = State.INVISIBLE;
				lastHitTime = Time.time;
			//}
			break;
		case State.FADE_IN:
			//delta = FADE_RATE * (Time.time - startFade);
			//Debug.Log("Fade In Delta: " + delta);
			//GetComponent<Renderer>().material.color = Color.Lerp (invisible, current, FADE_RATE * (Time.time - startFade));
			//if (delta >= 1f) {
				wall.enabled = true;
				state = State.VISIBLE;
				lastHitTime = Time.time;
			//}
			break;
		case State.INVISIBLE:
			if (Time.time - lastHitTime > FADE_TIME) {
				//wall.enabled = false;
				state = State.FADE_IN;
				startFade = Time.time;
			}
			break;
		}
	}
}
