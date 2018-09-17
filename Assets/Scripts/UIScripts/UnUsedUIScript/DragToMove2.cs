using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToMove2 : MonoBehaviour {

//	public string direction;
//	public Rigidbody rb;
//	public List<Transform> hits;
//	public Vector3 mouseDown;
//
//	private Vector3 mouse_offset;
//	private Vector3 centerRelativeToPosition;
//
//	void Start(){
//		hits = new List<Transform>();
//		centerRelativeToPosition = GetComponent<Collider>().bounds.center - transform.position;
//	}
//
//	void OnTriggerEnter(Collider other){
//		hits.Add (other.transform);
//	}
//	void OnTriggerExit(Collider other){
//		hits.Remove (other.transform);
//	}
//
//	void OnMouseDown(){
//		Debug.Log ("mouse down" + Input.mousePosition.x);
//		mouseDown = Input.mousePosition;
//		mouse_offset = transform.position - getWorldMousePosition();
//	}
//
//	Vector3 getWorldMousePosition()
//	{
//		float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
//		return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen ));
//	}
//
//	void OnMouseDrag()
//	{
//
//		rb = GetComponent<Rigidbody> ();
//		//		float distance_to_screen = Camera.main.WorldToScreenPoint (gameObject.transform.position).z;
//		//		transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
//		Vector3 pos_move = getWorldMousePosition ();
//		var movePosition = new Vector3 ();
//		//transform.position = new Vector3( pos_move.x, transform.position.y, pos_move.z );
//		//transform.position = new Vector3( transform.position.x, transform.position.y, pos_move.z );
//		//switch (direction)
//		//{
//		//case "x":	
//		movePosition = new Vector3 (pos_move.x + mouse_offset.x, transform.position.y, pos_move.z + mouse_offset.z);
////
//		foreach (var item in hits) {
//			var thisCollider = GetComponent<Collider> ();
//			var otherCollider = item.GetComponent<Collider> ();
//
//			var sign = Vector3.Distance(otherCollider.bounds.center, thisCollider.bounds.center)>0 ? -1f : 1f;
//			//var signz = otherCollider.bounds.center.z > thisCollider.bounds.center.z ? -1f : 1f;
//			var newCenter = otherCollider.bounds.center + Vector3.right * sign * (otherCollider.bounds.extents.x + thisCollider.bounds.extents.x)+ Vector3.forward * sign * (otherCollider.bounds.extents.z + thisCollider.bounds.extents.z);
//			var newCenterz = otherCollider.bounds.center + Vector3.forward * signz * (otherCollider.bounds.extents.z + thisCollider.bounds.extents.z);
//			var newPosition = newCenter - centerRelativeToPosition;
//			var newPositionz = newCenterz - centerRelativeToPosition;
//			if (sign < 0 && movePosition.x > transform.position.x || sign > 0 && movePosition.x < transform.position.x) {
//				movePosition.x = newPosition.x;	
//			}
//			else if (signz < 0 && movePosition.z > transform.position.z || signz > 0 && movePosition.z < transform.position.z) {
//								movePosition.z = newPositionz.z;	
//							}
//			}
//
//		transform.position = movePosition;
//
//		//rb.MovePosition (transform.position  * Time.deltaTime);
//		//print ("Why hello there good sir! Let me teach you about Trigonometry!");
////			break;
////		case "z":
////		movePosition = new Vector3 (transform.position.x, transform.position.y, pos_move.z + mouse_offset.z);
////
////		foreach (var item in hits) {
////			var thisCollider = GetComponent<Collider> ();
////			var otherCollider = item.GetComponent<Collider> ();
////
////			var sign = otherCollider.bounds.center.z > thisCollider.bounds.center.z ? -1f : 1f;
////			var newCenter = otherCollider.bounds.center + Vector3.forward * sign * (otherCollider.bounds.extents.z + thisCollider.bounds.extents.z);
////
////			var newPosition = newCenter - centerRelativeToPosition;
////
////			if (sign < 0 && movePosition.z > transform.position.z || sign > 0 && movePosition.z < transform.position.z) {
////				movePosition.z = newPosition.z;	
////			}
////		}
////
////		transform.position = movePosition;
////			//transform.position = new Vector3( transform.position.x, transform.position.y, pos_move.z + mouse_offset.z );
////			//rb.MovePosition (transform.position - new Vector3 (transform.position.x, transform.position.y, pos_move.z) * Time.deltaTime);
////			//print ("Hello and good day!");
////			break;
////		default:
////			//print ("Incorrect intelligence level.");
////			break;
////		}


	//}

	public float distanceFromCamera;
	Vector3 lastPos;
	Rigidbody rb;
	void Start(){
		distanceFromCamera = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;;
		rb = GetComponent<Rigidbody> ();

	}

	void FixedUpdate(){
		//Debug.Log ("update");
		if (Input.GetMouseButton (0)) {
			//Debug.Log ("upda");
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distanceFromCamera ));
			//pos.z = distanceFromCamera;
			//pos = Camera.main.ScreenToWorldPoint (pos);
			rb.velocity = (pos - transform.position) * 10;
		}

	}


}
