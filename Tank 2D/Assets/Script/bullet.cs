using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour {

	public float bulletSpeed = 10;
	public bool isPlayerBullet = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(transform.up*bulletSpeed*Time.deltaTime,Space.World);
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
		switch(collision.tag)
        {
			case "Player":
				if(!isPlayerBullet)
                {
					collision.SendMessage("Die");
					Destroy(this.gameObject);
                }
				break;
			case "Wall":
				Destroy(this.gameObject);
				Destroy(collision.gameObject);
				break;
			case "Enemy":
				if(isPlayerBullet)
                {
					collision.SendMessage("Die");
					Destroy(this.gameObject);
				}
                break;
			case "Barriar":
				if(isPlayerBullet) collision.SendMessage("PlayAudio");
				Destroy(this.gameObject);
				break;
			case "Heart":
				Destroy(this.gameObject);
				collision.SendMessage("Die");
				break;
			default:
				break;
        }
    }
}
