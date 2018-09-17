using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Creates a sensor that has multiple SNSSensor components.  All are aligned
/// along the forward axis in their default orientations
/// 
/// Field		Description
/// 
/// sensor		Array of sensors 
/// 
/// Author:		Brian Brookwell (BRB)
/// </summary>

public class SNSMultiple : SNSBase {
	public GameObject[] sensor;
	protected SNSBase[] sense;

	/// <summary>
	/// Determines whether this instance can see the specified target.
	/// </summary>
	/// <returns><c>true</c> if this instance can see the specified target; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	/// 
	/// Date		Author	Description
	/// 2017-11-02	BRB		Initial Testing

	override public bool CanSee (Transform target) {
		foreach (SNSBase s in sense)
			if (s.CanSee (target))
				return true;
		return false;
	}

	/// <summary>
	/// Determines whether this instance can see the specified target.
	/// </summary>
	/// <returns><c>true</c> if this instance can see the specified target; otherwise, <c>false</c>.</returns>
	/// <param name="target">Target.</param>
	/// 
	/// Date		Author	Description
	/// 2017-11-02	BRB		Initial Testing

	override public bool CanSee (GameObject target) {
		return CanSee (target.transform);
	}

	override protected void Init() {
		base.Init();

		sense = new SNSBase[sensor.Length];
		for (int i=0;i < sensor.Length;i++) { 
			sense[i] = sensor[i].GetComponent<SNSBase>();
		}
	}

	/// <summary>
	/// Creates the mesh for a multiple sensor (None)
	/// </summary>
	/// <returns>The mesh.</returns>
	/// 
	/// Date		Author	Description
	/// 2017-11-02	BRB		Initial Testing

	public override Mesh CreateMesh () {
		return null;
	}

	public SNSBase[] GetSensors() {
		return sense;
	}
}
