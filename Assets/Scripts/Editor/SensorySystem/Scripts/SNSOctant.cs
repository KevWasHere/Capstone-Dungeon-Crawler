using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Implements a octant sensor
/// </summary>
/// 
/// Author:	Brian Brookwell (BRB)

public class SNSOctant : SNSSphere {

	private static Mesh prefab = null;

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-18	BRB		Initial Testing

	override public bool CanSee (Transform target) {
		Vector3 x1x0 = target.position - gameObject.transform.position;

		if (x1x0.sqrMagnitude <= r2) {
			Vector3 rotated = Quaternion.Inverse (gameObject.transform.rotation) * x1x0;

			if (rotated.y >= -EPSILON && rotated.x >= -EPSILON && rotated.z >= -EPSILON) {
				return !losCheck || RayCheck (target, x1x0.normalized, gameObject.transform.position);
			}
		}

		return false;
	}

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-13	BRB		Initial Testing

	override public bool CanSee (GameObject target) {
		return CanSee (target.transform);
	}

	/// <summary>
	/// Creates the mesh for the cylinder sensor
	/// </summary>
	/// <returns>The sensor visibility mesh.</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-18	BRB		Initial Testing

	override public Mesh CreateMesh() { 
		if (prefab == null) {
			GameObject octant = Resources.Load ("SensorySystem/Meshes/Octant") as GameObject;
			prefab = octant.GetComponent<MeshFilter>().sharedMesh;
		}
		return prefab;
	}
		
	/// <summary>Debug output of Octant Sensor</summary>
	/// <returns>Debug string giving Octant Sensor settings</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-18	BRB		Initial Testing

	override public string ToString() {
		return "Octant " + name;
	}

}
