using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

	public void Show()
    {
        Debug.Log("win");
        GameManage._instance.showStar(  );
    }
}
