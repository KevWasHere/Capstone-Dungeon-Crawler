using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*-------------------------------------------------------------------------*
  # INTR Group 2
  # Student's Name: Kevin Ho, Myles Hangen, Shane Weerasuriya, 
  #					Tianqi Xiao, Yan Zhang, Yunzheng Zhou
  # CMPT 498 capstone
  # CharMenuControl.cs
  # Dialogue
*-----------------------------------------------------------------------*/
/*
Creator: Yan Zhang, Myles Hagen
DragToMove, move a game object with mouse dragging
direction: the axis that the game object will move 
rb: rigidbody that attached in the game object
hits: array of transforms that this object hits
mouseMouse: the position where mouse clicked
puzzle Camera: the Camera that look at the puzzle
mouse_offset: the offset of the mouse cicked and the center of the object
centerRelativeToPosition: The distance of the center of the game object to the position that will move
*/
public class DragToMove : MonoBehaviour {
	public string direction;
	public Rigidbody rb;
	public List<Transform> hits;
	public Vector3 mouseDown;
	public Camera puzzleCamera;

	private Vector3 mouse_offset;
	private Vector3 centerRelativeToPosition;

	void Start(){
		hits = new List<Transform>();
		centerRelativeToPosition = GetComponent<Collider>().bounds.center - transform.position;
	}

	//add a transform to his
	//Creator: Yan Zhang, Myles Hagen
	void OnTriggerEnter(Collider other){
		hits.Add (other.transform);
	}
	//remove a transform to hit
	//Creator: Yan Zhang, Myles Hagen
	void OnTriggerExit(Collider other){
		hits.Remove (other.transform);
	}

	//retrive the mouseDown and offset when Mouse is clicked
	//Creator: Yan Zhang, Myles Hagen
	void OnMouseDown(){
		//Debug.Log ("mouse down" + Input.mousePosition.x);
		mouseDown = Input.mousePosition;
		mouse_offset = transform.position - getWorldMousePosition();
	}

	//Get the mouse position relativ to the camera
	//Creator: Yan Zhang
	Vector3 getWorldMousePosition()
	{
		float distance_to_screen = puzzleCamera.WorldToScreenPoint(gameObject.transform.position).z;
		return puzzleCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
	}

	//Move the object with mouse drag 
	// Block the movement if a game object is on the way
	//Creator: Yan Zhang
	void OnMouseDrag()
	{
		//get the rigid body
		rb = GetComponent<Rigidbody> ();
		Vector3 pos_move = getWorldMousePosition ();
		var movePosition = new Vector3 ();
		//switch the movement along the axis base on the direction
		//x axis or z axis
		switch (direction)
		{
		//move along x axis
		case "x":	
			movePosition = new Vector3 (pos_move.x + mouse_offset.x, transform.position.y, transform.position.z);

			foreach (var item in hits) {
				var thisCollider = GetComponent<Collider> ();
				var otherCollider = item.GetComponent<Collider> ();

				var sign = otherCollider.bounds.center.x > thisCollider.bounds.center.x ? -1f : 1f;
				var newCenter = otherCollider.bounds.center + Vector3.right * sign * (otherCollider.bounds.extents.x + thisCollider.bounds.extents.x);

				var newPosition = newCenter - centerRelativeToPosition;

				if (sign < 0 && movePosition.x > transform.position.x || sign > 0 && movePosition.x < transform.position.x) {
					movePosition.x = newPosition.x;	
				}
			}

			transform.position = movePosition;

			//rb.MovePosition (transform.position  * Time.deltaTime);
			//print ("Why hello there good sir! Let me teach you about Trigonometry!");
			break;
		//move along z axis
		case "z":
			movePosition = new Vector3 (transform.position.x, transform.position.y, pos_move.z + mouse_offset.z);

			foreach (var item in hits) {
				var thisCollider = GetComponent<Collider> ();
				var otherCollider = item.GetComponent<Collider> ();

				var sign = otherCollider.bounds.center.z > thisCollider.bounds.center.z ? -1f : 1f;
				var newCenter = otherCollider.bounds.center + Vector3.forward * sign * (otherCollider.bounds.extents.z + thisCollider.bounds.extents.z);

				var newPosition = newCenter - centerRelativeToPosition;

				if (sign < 0 && movePosition.z > transform.position.z || sign > 0 && movePosition.z < transform.position.z) {
					movePosition.z = newPosition.z;	
				}
			}

			transform.position = movePosition;

			break;
		default:
			//print ("Incorrect intelligence level.");
			break;
		}
	}


}
