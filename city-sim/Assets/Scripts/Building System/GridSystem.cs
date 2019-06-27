using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {

	public float gridSize;
	public GameObject target;
	public GameObject prefab;
	Vector3 truePos;
 
	private void LateUpdate () {
		truePos.x = (Mathf.Floor(target.transform.position.x / gridSize) * gridSize);
		truePos.y = (Mathf.Floor(target.transform.position.y / gridSize) * gridSize);
		truePos.z = (Mathf.Floor(target.transform.position.z / gridSize) * gridSize);

		prefab.transform.position = truePos;
	}
}
