using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Singleton object used when Creature has no sensor assigned
/// </summary>
///
///	Author:	Brian Brookwell (BRB)

public class SNSNone : SNSSensor {

	/// <summary>
	/// Determines whether this sensor can see the indicated target (always false)
	/// </summary>
	/// <param name="target"> Transform/GameObject for the target </param>
	/// 
	/// Date		Author	Description
	/// 2017-10-06	BRB		Initial Testing

	override public bool CanSee (Transform target) { return false; }
	override public bool CanSee (GameObject target) { return false; }

	/// <summary>
	/// Returns an empty GameObject for the mesh as a SensorNone object has no associated
	/// visibility region
	/// </summary>
	/// <returns> The empty game object returned as the sensor visbility region </returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-06	BRB		Initial Testing

	override public Mesh CreateMesh() {return null; }
}
