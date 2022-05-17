using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

	//属性值
	public int lifeValue = 3;
	public int playerScore = 0;
    public bool isDie = false;
    public GameObject born;
    public bool isDefeat = false;
    public Text textPlayerScore;
    public Text textPlayerLifeValue;
    public Text textDefeatScore;
    public GameObject isDefeatUI;

	//单例
	private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }

        set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 2f);
            return;
        }
		if(isDie)
        {
            Recover();
        }
        textPlayerScore.text = playerScore.ToString();
        textPlayerLifeValue.text = lifeValue.ToString();
        textDefeatScore.text = ": "+playerScore.ToString();
	}

    private void Recover()
    {
        if(lifeValue <= 0)
        {
            isDefeat = true;
            isDefeatUI.SetActive(true);
            Invoke("ReturnToTheMainMenu", 2f);
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(0, -6, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDie = false;
        }
    }

    private void ReturnToTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
