using UnityEngine;
using System.Collections;

/**
 * Camera Control: Handles the detection and signalling for objects in LOS (Visibility Control)
 * 
 * Author: 	B. Brookwell
 * Date:	11-02-2015
 * 
 * Public		Description
 * 
 * RADIUS		Radius to use if sphere casting is being used
 * CHECK_TIME	Time in seconds between visibility checks.  Increased CHECK_TIME reduces cost at expense of
 * 				how sensitive the LOS is.
 * useSphere	If true, SphereCast is used.  Otherwise, RayCast is used
 * everyFrame	Check the visibility every frame
 * 
 * Variable		Description
 * 
 * player		Link to player GameObject
 * mainCam		Transform data for main camera
 * limitTime	Time when the visibility limits are next to be checked
 */

public class CameraFadeController : MonoBehaviour {
	public GameObject 	player;
	private Transform	mainCam;
	private float 		limitTime = 0f;

	public bool 	useSphere	= false;
	public bool		everyFrame	= false;
	public float 	RADIUS		= 1.0f;
	public float 	CHECK_TIME 	= 0.1f;

	// Use this for initialization
	void Start () {
		//player = GameObject.Find ("Player");
		mainCam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (everyFrame || Time.time >= limitTime) {
			Vector3 playerPosition = player.transform.position;
			playerPosition.y += 0.1f;
			//Vector3 delta = mainCam.position - player.transform.position;
			Vector3 delta = mainCam.position - playerPosition;
			//Debug.Log("Cam: " + mainCam.position);
			//Debug.Log("Delta: " + mainCam.position);
			RaycastHit[] hit;

			if (useSphere) {
				hit = Physics.SphereCastAll (mainCam.position, RADIUS, -delta, delta.magnitude-RADIUS);
				/*
				//Look into capsule sizing
				Vector3 p1 = player.transform.position + (Vector3.up * 0.5f);
				//p1.y -= 1.0f;
				Vector3 p2 = player.transform.position + (Vector3.up * -0.5f);
				//p2.y += 1.0f;
				hit = Physics.CapsuleCastAll (p1, p2, RADIUS, -delta, delta.magnitude-RADIUS);
				*/
			}
			else
				hit = Physics.RaycastAll (new Ray(mainCam.position, -delta), delta.magnitude);

			foreach (RaycastHit h in hit) {
				if (h.collider.gameObject != player) {
					//Debug.Log("HITTING!!!!");
					VisibilityScript vs = h.collider.gameObject.GetComponent<VisibilityScript>();
					if (vs)
						vs.MakeInvisible();
				} else
					break;
			}

			limitTime = Time.time + CHECK_TIME;
		}
	}
}
