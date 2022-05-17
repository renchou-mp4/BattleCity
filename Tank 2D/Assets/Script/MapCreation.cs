using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {

    //0、老窝 1、墙 2、障碍 3、出生效果 4、河流 5、草 6、空气墙
    public GameObject[] items;
    private List<Vector3> itemPosition = new List<Vector3>();
    private void Awake()
    {
        InitMap();
    }

    private void InitMap()
    {
        itemPosition.Add(new Vector3(0, -6, 0));
        itemPosition.Add(new Vector3(-10, 8, 0));
        itemPosition.Add(new Vector3(0, 8, 0));
        itemPosition.Add(new Vector3(10, 8, 0));
        //实例化老窝
        CreateItems(items[0], new Vector3(0, -8, 0), Quaternion.identity);
        //实例化围墙
        CreateItems(items[1], new Vector3(-1, -8, 0), Quaternion.identity);
        CreateItems(items[1], new Vector3(1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
            CreateItems(items[1], new Vector3(i, -7, 0), Quaternion.identity);
        //实例化空气墙
        for (int i = -11; i < 12; i++)
        {
            GameObject itGo = Instantiate(items[6], new Vector3(i - 0.13f, -9, 0), Quaternion.identity) as GameObject;
            itGo.transform.SetParent(this.gameObject.transform);
        }
        for (int i = -11; i < 12; i++)
        {
            GameObject itGo = Instantiate(items[6], new Vector3(i - 0.13f, 9, 0), Quaternion.identity) as GameObject;
            itGo.transform.SetParent(this.gameObject.transform);
        }
        for (int i = -8; i < 9; i++)
        {
            GameObject itGo = Instantiate(items[6], new Vector3(-11.13f, i, 0), Quaternion.identity) as GameObject;
            itGo.transform.SetParent(this.gameObject.transform);
        }
        for (int i = -8; i < 9; i++)
        {
            GameObject itGo = Instantiate(items[6], new Vector3(11.13f, i, 0), Quaternion.identity) as GameObject;
            itGo.transform.SetParent(this.gameObject.transform);
        }
        //实例化墙、障碍、河流、草
        for (int i = 0; i < 40; i++)
            CreateItems(items[1], RandomPosition(), Quaternion.identity);
        for (int i = 0; i < 20; i++)
            CreateItems(items[2], RandomPosition(), Quaternion.identity);
        for (int i = 0; i < 20; i++)
            CreateItems(items[4], RandomPosition(), Quaternion.identity);
        for (int i = 0; i < 20; i++)
            CreateItems(items[5], RandomPosition(), Quaternion.identity);
        //实例化玩家
        GameObject go = Instantiate(items[3], new Vector3(0, -6, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;
        //实例化敌人
        Instantiate(items[3], new Vector3(-8, 8, 0), Quaternion.identity);
        Instantiate(items[3], new Vector3(0, 8, 0), Quaternion.identity);
        Instantiate(items[3], new Vector3(8, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);
    }
    //实例化方法
    private void CreateItems(GameObject createGameObject,Vector3 createPosition,Quaternion createRotasion)
    {
        GameObject itGo = Instantiate(createGameObject, createPosition, createRotasion);
        itGo.transform.SetParent(this.gameObject.transform);
        itemPosition.Add(createPosition);
    }

    //产生随机位置的方法
    private Vector3 RandomPosition()
    {
        while(true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8),0);
            if (!IsPosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    //判断位置列表中是否有这个位置
    private bool IsPosition(Vector3 createPosition)
    {
        for (int i = 0; i < itemPosition.Count; i++)
            if (createPosition == itemPosition[i])
                return true;
        return false;
    }

    //产生敌人的方法
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num == 0)
        {
            EnemyPos = new Vector3(-8, 8, 0);
        }
        else if(num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else if(num == 2)
        {
            EnemyPos = new Vector3(8, 8, 0);
        }
        Instantiate(items[3], EnemyPos, Quaternion.identity);
    }
}
