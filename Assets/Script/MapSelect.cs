using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelect : MonoBehaviour {
    public int starsNum = 0;
    private bool isSelect = false;


    public GameObject locks;
    public GameObject stars;

    public GameObject panel;
    public GameObject map;

    private void Start()
    {
        if( PlayerPrefs.GetInt( "totalNum", 0 )  >= starsNum ) {
            isSelect = true;
        }

        if( isSelect) {
            locks.SetActive(false);
            stars.SetActive( true );

            //text显示
        }
    }

    public void Selected()
    {
        if (isSelect) {
            panel.SetActive(true);
            map.SetActive( false );
        }
    }
}
