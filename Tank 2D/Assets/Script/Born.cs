using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

	public GameObject PlayerPrefabs;
	public GameObject[] EnemyPrefabs;

	public bool createPlayer = false;

	// Use this for initialization
	void Start () {
		Invoke("BornTank", 1f);
		Destroy(gameObject, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void BornTank()
    {
		if(createPlayer)
        {
			Instantiate(PlayerPrefabs, transform.position, Quaternion.identity);
		}
        else
        {
			int num = Random.Range(0, 2);
			Instantiate(EnemyPrefabs[num], transform.position, Quaternion.identity);
        }
    }
}
