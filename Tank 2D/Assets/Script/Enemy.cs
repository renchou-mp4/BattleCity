using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float speed = 3;
	public Sprite[] tankSprite;//上 右 下 左
	public GameObject enemyBulletPrefab;
	public GameObject explosionPrefab;
	//public GameObject defendPrefab;

	private SpriteRenderer sr;
	private Vector3 bulletEulerAngles;
	private float timeVal;
	private float timeValChangeDirection = 2;
	//private bool isDefended = true;
	//private float isDefendedVal = 3;
	private float h,v = -1;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Update()
	{
		//if (isDefended)
		//{
		//	defendPrefab.SetActive(true);
		//	isDefendedVal -= Time.deltaTime;
		//	if (isDefendedVal <= 0)
		//	{
		//		isDefended = false;
		//		defendPrefab.SetActive(false);
		//	}
		//}
		if (PlayerManager.Instance.isDefeat)
		{
			return;
		}

		if (timeVal > 2f)
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
		//if (isDefended == true) return;
		Instantiate(explosionPrefab, transform.position, transform.rotation);
		Destroy(this.gameObject);
		PlayerManager.Instance.playerScore++;
	}



	//坦克的攻击
	private void Attack()
	{
		Instantiate(enemyBulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
		timeVal = 0;
	}

	//坦克的移动
	private void Move()
	{
		//控制坦克的移动间隔以及移动方向
		if(timeValChangeDirection>=4)
        {
			int num = Random.Range(0, 8);
			if(num>5)
            {
				v = -1;
				h = 0;
            }
			else if(num==0)
            {
				v = 1;
				h = 0;
            }
			else if(num>0&&num<=2)
            {
				v = 0;
				h = -1;
            }
			else if(num>2&&num<=4)
            {
				v = 0;
				h = 1;
            }
			timeValChangeDirection = 0;
        }
        else
        {
			timeValChangeDirection += Time.fixedDeltaTime;
        }

		//控制坦克的水平走向
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

		if (h != 0) return;

		//控制坦克的垂直走向
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
	}

	// 坦克碰撞转向
	private void OnCollisionEnter2D(Collision2D collision)
    {
		timeValChangeDirection = 4;
    }
}
