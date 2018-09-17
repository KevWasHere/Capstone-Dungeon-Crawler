using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public abstract class SNSBase : MonoBehaviour {

	protected MeshFilter meshFilter;

	void Awake () {
		Init();
	}

	virtual protected void Init() {
		meshFilter = GetComponent<MeshFilter>();
	}

	// Abstract Methods implemented by a sub-class sensor

	public abstract bool CanSee (Transform target);
	public abstract bool CanSee (GameObject target);

	public abstract Mesh CreateMesh();
}
