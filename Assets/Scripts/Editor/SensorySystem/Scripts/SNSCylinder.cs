using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Handles a cylindrical sensor region
/// </summary>
/// 
/// Author:		Brian Brookwell (BRB)
/// 
/// Field		Description
/// 
/// height		Height of the cylinder
/// h2			Inverse of height squared

[System.Serializable]
public class SNSCylinder : SNSSensor {
	private static Mesh prefab = null;

	public float height;
	[System.NonSerialized] protected float h2;

	/// <summary>
	/// Sets up the parent game object for the sensor
	/// </summary>
	/// 
	/// Date		Author	Description
	/// 2017-10-28	BRB		Changes the gameObject associated with this Sensor

	override protected void Init() {
		base.Init();

		SetHeight (height);
	}

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-13	BRB		Initial Testing

	override public bool CanSee (Transform target) {
		Vector3 x1x0 = target.position - gameObject.transform.position;
		Vector3 x2x1 = gameObject.transform.forward * height;

		float t = Vector3.Dot (x1x0, x2x1) * h2;

		if (t >= -EPSILON && t <= 1f + EPSILON) {
			Vector3 separation = x1x0 - x2x1 * t;

			if (separation.sqrMagnitude <= r2) {
				return !losCheck || RayCheck (target, x2x1, gameObject.transform.position);

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
	/// Changes the height of the cylinder
	/// </summary>
	/// <param name="ht"> New value for the height of the cylinder sensor </param>
	/// 
	/// Date		Author	Description
	/// 2017-10-07	BRB		Initial Testing

	public void SetHeight (float ht) {
		height = ht;
		h2 = 1f / (ht * ht);

		HandleSensorRegion();
	}

	/// <summary>
	/// Changes the height and radius of the cylinder
	/// </summary>
	/// <param name="rd"> New value for the radius of the cylinder sensor </param>
	/// <param name="ht"> New value for the height of the cylinder sensor </param>
	/// 
	/// Date		Author	Description
	/// 2017-10-07	BRB		Initial Testing

	public void SetSize (float rd, float ht) {
		radius = rd;
		r2 = rd * rd + rd * EPSILON;
		height = ht;
		h2 = 1/(ht * ht);

		HandleSensorRegion();
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
			GameObject cylinder = Resources.Load ("SensorySystem/Meshes/Cylinder") as GameObject;
			prefab = cylinder.GetComponent<MeshFilter>().sharedMesh;
		}

		return prefab;
	}

	/// <summary>
	/// Scale this cylinder using the radius and height values.
	/// </summary>
	/// 
	/// Date		Author	Description
	/// 2017-10-13	BRB		Initial Testing

	override protected Vector3 Scale() {
		return new Vector3 (radius, radius, height);
	}
}
