using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Determines whether the target is within a sphere
/// 
/// Author:	Brian Brookwell (BRB)
/// </summary>

public class SNSSphere : SNSSensor {

	private static Mesh prefab = null;

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date			Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to add position offset and LOS Check
	/// 2017-10-21	BRB		Conversion to work based on a GameObject

	override public bool CanSee (Transform target) {
		Vector3 delta = target.position - gameObject.transform.position;

		if (delta.sqrMagnitude <= r2)
			return !losCheck || RayCheck (target, delta, gameObject.transform.position);
		return false;
	}
		
	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">GameObject of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date			Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to add position offset and LOS Check
	/// 2017-10-21	BRB		Conversion to work based on a GameObject

	override public bool CanSee (GameObject target) {
		return (target.transform);
	}

	/// <summary>Debug output of Sphere Sensor</summary>
	/// <returns>Debug string giving Sphere Sensor settings</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-07	BRB		Initial Testing

	override public string ToString() {
		return "Sphere " + name;
	}

	/// <summary>
	/// Returns the mesh associated with the sphere sensor
	/// </summary>
	/// <returns>The mesh for a sphere.</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-07	BRB		Initial Testing
	/// 2017-10-21	BRB		Modification to use pre-created asset instead of primitive

	override public Mesh CreateMesh () {
		if (prefab == null) {
			GameObject sphere = Resources.Load ("SensorySystem/Meshes/Sphere") as GameObject;
			prefab = sphere.GetComponent<MeshFilter>().sharedMesh;
		}

		return prefab;
	}
}
