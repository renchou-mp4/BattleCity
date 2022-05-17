using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float speed = 3;
	public Sprite[] tankSprite;//上 右 下 左
	public GameObject bulletPrefab;
	public GameObject explosionPrefab;
	public GameObject defendPrefab;
	public AudioSource moveAudio;
	public AudioClip[] tankAudio;


	private SpriteRenderer sr;
	private Vector3 bulletEulerAngles;
	private float timeVal;
	private bool isDefended = true;
	private float isDefendedVal = 3;

	void Awake()
    {
		sr = GetComponent<SpriteRenderer>();
    }

	void Update()
    {
		if (PlayerManager.Instance.isDefeat)
		{
			return;
		}
		if (isDefended)
        {
			defendPrefab.SetActive(true);
			isDefendedVal -= Time.deltaTime;
			if(isDefendedVal<=0)
            {
				isDefended = false;
				defendPrefab.SetActive(false);
			}
        }

		if(timeVal>0.4f)
        {
			Attack();
		}
        else
        {
			timeVal += Time.deltaTime;
        }
	}

	void FixedUpdate()
    {
		if(PlayerManager.Instance.isDefeat)
        {
			return;
        }
		Move();		
	}

	//坦克的死亡
	private void Die()
    {
		if (isDefended == true) return;
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(this.gameObject);
		PlayerManager.Instance.isDie = true;

    }



	//坦克的攻击
	private void Attack()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
			Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
			timeVal = 0;
		}
			
    }

	//坦克的移动
	private void Move()
    {
		float h = Input.GetAxisRaw("Horizontal");
		transform.Translate(Vector3.right * h * speed * Time.fixedDeltaTime, Space.World);
		if (h > 0)
		{
			sr.sprite = tankSprite[1];
			bulletEulerAngles = new Vector3(0, 0, -90);
		}
		else if (h < 0)
        {
			sr.sprite = tankSprite[3];
			bulletEulerAngles = new Vector3(0, 0, 90);
		}

		if(Mathf.Abs(h)>0.05f)
        {
			moveAudio.clip = tankAudio[1];
			if (!moveAudio.isPlaying) moveAudio.Play();
        }

		if (h != 0) return;

		float v = Input.GetAxisRaw("Vertical");
		transform.Translate(Vector3.up * v * speed * Time.fixedDeltaTime, Space.World);
		if (v > 0)
        {
			sr.sprite = tankSprite[0];
			bulletEulerAngles = new Vector3(0, 0, 0);
		}
		else if (v < 0)
        {
			sr.sprite = tankSprite[2];
			bulletEulerAngles = new Vector3(0, 0, -180);
		}

		if (Mathf.Abs(v) > 0.05f)
		{
			moveAudio.clip = tankAudio[1];
			if (!moveAudio.isPlaying) moveAudio.Play();
		}
		else
		{
			moveAudio.clip = tankAudio[0];
			if (!moveAudio.isPlaying) moveAudio.Play();
		}
	}
}
