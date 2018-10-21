using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasuePanel : MonoBehaviour {

    private Animator anim;
    public GameObject button;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Retry()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    /// <summary>
    /// 点击暂停
    /// </summary>
    public void Pause( )
    {
        //播放Pause动画
        anim.SetBool("isPause", true);
        button.SetActive(false);
    }

    public void Home()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// 点击继续按钮
    /// </summary>
    public void Resume()
    {
        //播放Resume动画 
        Time.timeScale = 1;
        anim.SetBool("isPause", false);
    }

    public void PauseAnimEnd()
    {
        Time.timeScale = 0;
    }

    public void ResumeAnimEnd()
    {
        button.SetActive( true );
    }
}