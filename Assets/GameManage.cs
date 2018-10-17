using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManage _instance;
    private Vector3 originPos;//小鸟初始位置


    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;

    private void Awake()
    {
        _instance = this;
        if( birds.Count >0) {
            originPos = birds[0].transform.position;
        }
    }

    public void Start(  )
     {
        Initalized();
     }

    private void Initalized()
    {
        for( int i=0; i<birds.Count; ++i) {
            if( i==0) {//第一只小鸟
                birds[0].transform.position = originPos;
                birds[i].enabled = true;
                birds[i].sp.enabled = true;
            } else {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    /// <summary>
    /// 判定游戏是否结束
    /// </summary>
    public void NextBird()
    {
        if (pigs.Count > 0) {
            if (birds.Count >0) {//下一只小鸟准备
                Initalized();
            } else {//输了
                lose.SetActive(true);
            }
        } else {//赢了
            win.SetActive( true );
        }
    }

    public void showStar(   )
    {
        StartCoroutine( "show" );
    }

    IEnumerator show()
    {
        for (int i = 0; i < birds.Count + 1; ++i) {
            yield return new WaitForSeconds(0.2f);
            stars[i].SetActive(true);
        }
    }
	
    public void Replay()
    {
        Debug.Log(2);
        SceneManager.LoadScene( 2 ) ;
    }

    public void Home()
    {
        Debug.Log(1);
        SceneManager.LoadScene(1);
    }


	// Update is called once per frame
	void Update () {
		
	}
}
