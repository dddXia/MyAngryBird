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

    private int starsNum = 0;

    private int totalNum = 2;//一张地图总关卡数

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
                birds[i].canMove = true;
            } else {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
                birds[i].canMove = false;
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
                //此时应该隐藏UI上的暂停按钮，以免在此时暂停了游戏，
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
        for (  ; starsNum < birds.Count + 1; ++starsNum) {
            if ( starsNum >= stars.Length) break;
            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);
        }
    }
	
    public void Replay()
    {
        SaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene( 2 ) ;
    }

    public void Home()
    {
        SaveData();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void SaveData()
    {
        if( starsNum > PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))  ) {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"), starsNum);
        }

        int sum = 0;//存储所有星星数量
        for (int i = 1; i <= totalNum; i++) {
            sum += PlayerPrefs.GetInt("Level" + i.ToString());
        }
        print( sum );
        PlayerPrefs.SetInt("totalNum", sum);
    }
}
