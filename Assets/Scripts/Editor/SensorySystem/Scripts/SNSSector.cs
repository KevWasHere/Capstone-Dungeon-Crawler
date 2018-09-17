using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Creates a spherical sector sensor
///
/// Field		Description
/// 
/// angle		Visibility angle of the sector sensor
/// cosine		Cosine of the visibility angle
/// 
/// Author:	Brian Brookwell (BRB)
/// </summary>

[System.Serializable]
public class SNSSector : SNSSensor {
	private const int SIZE = 24;

	public float angle;
	private float cosine;

	override protected void Init() {
		base.Init();

		SetAngle (angle);
	}

	/// <summary>Sets the visibility angle for the sector sensor</summary>
	/// <param name="angle">Visibility angle of the sector sensor</param>
	/// 
	/// Date		Author	Description 
	/// 2017-10-06	BRB		INitial Testing

	public void SetAngle (float angle) {
		this.angle = angle;
		cosine = Mathf.Cos (Mathf.Deg2Rad * angle) - EPSILON;

		meshFilter.sharedMesh = CreateMesh();

		HandleSensorRegion();
	}
		
	public void SetSize (float radius, float angle) {
		SetRadius (radius);
		SetAngle (angle);
	}

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">Transform of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date			Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to add position offset and LOS Check

	override public bool CanSee (Transform target) {
		Vector3 heading = target.position - gameObject.transform.position;

		if (heading.sqrMagnitude <= r2 && Vector3.Dot (heading.normalized, gameObject.transform.forward) >= cosine)
			return !losCheck || RayCheck (target, heading, gameObject.transform.position);
		return false;
	}

	/// <summary>Determines whether this sensor can see the indicated target </summary>
	/// <param name="target">GameObject of the target being tested</param>
	/// <returns>True if the target can be seen from the origin, false otherwise</returns>
	/// 
	/// Date			Author	Description
	/// 2017-10-06	BRB		Initial Testing
	/// 2017-10-08	BRB		Converted to add position offset and LOS Check

	override public bool CanSee (GameObject target) {
		return CanSee (target.transform);
	}

	/// <summary>Debug output of Hemisphere Sensor</summary>
	/// <returns>Debug string giving Hemisphere Sensor settings</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-07	BRB		Initial Testing

	override public string ToString() {
		return "Sector " + name;
	}

	/// <summary>
	/// Creates the gameobject for the visibility region
	/// </summary>
	/// <returns>The game object for the visibility region</returns>
	/// 
	/// Date		Author	Description
	/// 2017-10-17	BRB		Initial Testing

	override public Mesh CreateMesh () {
		Vector2[] ring = new Vector2[SIZE];
		for (int i=0;i < SIZE;i++) {
			float rad = 360f * i * Mathf.Deg2Rad / SIZE;
			ring[i] = new Vector2 (Mathf.Cos(rad), Mathf.Sin(rad));
		}

		// Create mesh and associated point, uv and triangle lists

		Mesh mesh = new Mesh();

		List<Vector3> point = new List<Vector3>();
		List<Vector2> uv = new List<Vector2>();
		List<int> triangle = new List<int>();

		// Add points and uvs for rings of points

		int rings = Mathf.CeilToInt (angle / 15f);
		float delta = Mathf.Deg2Rad * angle / rings;

		float rScale = 1f / rings;
		float iScale = 1f / SIZE;

		Vector3[] fRing = new Vector3[SIZE];
		Vector2[] fUV = new Vector2[SIZE];

		for (int r=1;r <= rings; r++) {
			float phi = r * delta;
			float cosPhi = Mathf.Cos (phi);
			float sinPhi = Mathf.Sin (phi);

			for (int i=0;i < SIZE;i++) {
				Vector3 newPoint = new Vector3 (sinPhi * ring[i].x, sinPhi * ring[i].y, cosPhi);
				Vector2 newUV = new Vector2 (r * rScale, i * iScale);
				point.Add (newPoint);
				uv.Add (newUV);
				if (r == rings) {
					fRing [i] = newPoint;
					fUV [i] = newUV;
				}
			}
		}

		int ringStart = point.Count - 1;
		for (int i = 0; i < SIZE; i++) {
			point.Add (fRing [i]);
			uv.Add (fUV [i]);
		}

		// Add single point for forwardmost point

		point.Add (new Vector3 (0f, 0f, 1f));
		uv.Add (new Vector2 (0, 1f));
		int nose = point.Count - 1;

		// Add single point for sensor origin

		point.Add (Vector3.zero);
		uv.Add (new Vector2 (1f, 0.5f));
		int tail = point.Count - 1;

		// Add triangles for the rings

		for (int r=1;r < rings;r++) {
			for (int i=0;i < SIZE;i++) {
				int a = r * SIZE + i;
				int b = (i+1 == SIZE)?r * SIZE:a + 1;
				int c = a - SIZE;
				int d = b - SIZE;

				triangle.Add (a);
				triangle.Add (d);
				triangle.Add (c);

				triangle.Add (a);
				triangle.Add (b);
				triangle.Add (d);
			}
		}

		// Add triangle fan for front of sensor object

		for (int i=0;i < SIZE;i++) {
			int b = (i + 1) % SIZE;

			triangle.Add (i);
			triangle.Add (b);
			triangle.Add (nose);
		}

		// Add triangle fan for rear cone of sensor object

		for (int i=0;i < SIZE;i++) {
			int a = ringStart + i;
			int b = ringStart + (i + 1) % SIZE;

			triangle.Add (a);
			triangle.Add (tail);
			triangle.Add (b);
		}

		// Set up the mesh vertices, uvs and triangles
		mesh.vertices = point.ToArray();
		mesh.uv = uv.ToArray();
		mesh.triangles = triangle.ToArray();
		mesh.RecalculateNormals();

		mesh.name = "Sector (" + angle + ")";

		return mesh;
	}
}