using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManage _instance;
    private Vector3 originPos;//小鸟初始位置

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

            }
        } else {//赢了

        }
    }

	
	// Update is called once per frame
	void Update () {
		
	}
}
