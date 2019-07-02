using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Prefab")]
public class PrefabID : ScriptableObject {

	public string prefabName, objectType;
	public GameObject obj;
	public float price;
	public int id;
}
