using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour {
	public Transform trans;
	public Camera mini;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		var wantedPos = mini.WorldToScreenPoint (trans.position + new Vector3(0, trans.position.y, 0));
		transform.position = wantedPos;
	}
}