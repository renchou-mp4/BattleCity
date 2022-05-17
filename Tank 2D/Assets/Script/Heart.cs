using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {

	public Sprite BrokenSprite;
	public GameObject explosionPrefab;
	public AudioClip dieAudio;

	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
	}
	
	private void Die()
    {
		sr.sprite = BrokenSprite;
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		PlayerManager.Instance.isDefeat = true;
		PlayerManager.Instance.isDefeatUI.SetActive(true);
		AudioSource.PlayClipAtPoint(dieAudio, transform.position);
	}
}
