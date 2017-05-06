using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour {
	
	public GameObject building;
	public Vector3 spawnValues;
	public int buildingCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	void Start ()
	{
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
	{
			for (int i = 0; i < 400; i += 40)
			{
				Vector3 spawnPosition = new Vector3 (spawnValues.x, 0, spawnValues.z + i);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (building, spawnPosition, building.transform.rotation);
				yield return new WaitForSeconds (spawnWait);
			}
	}
}
