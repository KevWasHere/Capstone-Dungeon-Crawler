using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Implements a half cylinder sensor
/// </summary>
/// 
/// Author:	Brian Brookwell (BRB)

public class SNSHemiCylinder : SNSCylinder {
	private static Mesh prefab = null;

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-17	BRB		Initial Testing

	override public bool CanSee (Transform target) {
		Vector3 x1x0 = target.position - gameObject.transform.position;
		Vector3 x2x1 = gameObject.transform.forward * height;

		float t = Vector3.Dot (x2x1, x1x0) * h2;

		if (t >= -EPSILON && t <= 1f + EPSILON) {
			Vector3 separation = x1x0 - (x2x1 * t);
			if (separation.sqrMagnitude <= r2) {
				Vector3 rotated = Quaternion.Inverse (gameObject.transform.rotation) * x1x0;

				if (rotated.y > -EPSILON) {
					return !losCheck || RayCheck (target, x2x1, gameObject.transform.position);
				}
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
	/// 2017-10-13	BRB		Initial Testing

	override public Mesh CreateMesh() { 
		if (prefab == null) {
			GameObject hemiCylinder = Resources.Load ("SensorySystem/Meshes/HemiCylinder") as GameObject;
			prefab = hemiCylinder.GetComponent<MeshFilter>().sharedMesh;
		}
		return prefab;
	}
}
