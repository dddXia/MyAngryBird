﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    //在面板中修改数值
    public float maxDis;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;

    public GameObject boom;

    protected TestTrail myTrail;
    [HideInInspector]
    public bool canMove = false;
    public float smooth = 3;


    public AudioClip select;
    public AudioClip fly;
    //public bool ________;

    private bool isClick = false;
    private bool isFly = false;
    [HideInInspector]
    public SpringJoint2D sp;
    protected Rigidbody2D rg;

    public Sprite hurt;
    protected SpriteRenderer render;

    private void OnMouseDown(  )//鼠标按下
    {
        if (canMove) {
            AudioPlay(select);
            isClick = true;
            rg.isKinematic = true;
        }
       
    }

    private void OnMouseUp(  )//鼠标抬起
    {
        if( canMove) {
            
            isClick = false;

            rg.isKinematic = false;
            Invoke("Fly", 0.1f);//延迟关节失效

            //禁用划线组件
            right.enabled = false;
            left.enabled = false;
            canMove = false;
        }
     
    }

    private void Awake()
    {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        myTrail = GetComponent<TestTrail>();
        render = GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start () {
        //Debug.Log(rightPos.position);
    }
	
	// Update is called once per frame
	void Update () {
        
		if( isClick) {//鼠标一直按下，进行位置跟随
            transform.position =  Camera.main.ScreenToWorldPoint( Input.mousePosition );
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);

            if (Vector3.Distance(transform.position, rightPos.position) > maxDis) {
                Vector3 pos = (transform.position - rightPos.position).normalized;//单位化向量
                pos *= maxDis;//最大长度的向量
                transform.position = pos + rightPos.position;//最大长度的向量 + 起点坐标
            }

            Line();
        }

        //相机跟随
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp( Camera.main.transform.position, 
            new Vector3( Mathf.Clamp( posX, 0, 12 ) ,  Camera.main.transform.position.y, Camera.main.transform.position.z),
            smooth*Time.deltaTime);
    
        if( isFly) {
            if( Input.GetMouseButtonDown( 0 )) {
                showSkill();
            }
        }
	}

    void Fly(  )
    {
        AudioPlay(  fly );
        isFly = true;
        myTrail.TrailStart();
        sp.enabled = false;//关节失效
        Invoke("Next", 5);
    }
    /// <summary>
    /// 划线
    /// </summary>

    void Line()
    {
        right.enabled = true;
        left.enabled = true;

        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);

        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }
    /// <summary>
    /// 下一次小鸟飞出
    /// </summary>

    protected virtual void Next()
    {
        GameManage._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity );
        GameManage._instance.NextBird(  );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isFly = false;
        myTrail.Clear(  );
        
    }

    public  void AudioPlay(  AudioClip clip )
    {
        AudioSource.PlayClipAtPoint(  clip, transform.position  );
    }

    /// <summary>
    /// 小鸟技能
    /// </summary>
    public virtual void showSkill()
    {
        isFly = false;
    }

    public void Hurt()
    {
        render.sprite = hurt;
    }
}
