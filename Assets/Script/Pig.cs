using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour {

    public float maxSpeed = 10;
    public float minSpeed = 5;
    private SpriteRenderer render;
    public Sprite hurt;
    public GameObject boom;
    public bool isPig;

    public AudioClip hurtClip;
    public AudioClip dead;
    public AudioClip birdCollision;
    public GameObject score;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if( collision.gameObject.tag == "player") {
            AudioPlay( birdCollision );
        }

        //用另一个物体相对于猪的速度做判断(用相对速度)
        if (  collision.relativeVelocity.magnitude > maxSpeed ) {
            Dead();
        } else if(collision.relativeVelocity.magnitude > minSpeed && collision.relativeVelocity.magnitude < maxSpeed ) {
            render.sprite = hurt;
            AudioPlay(hurtClip);
        }
    }

    void Dead()
    {
        if (isPig) {
            GameManage._instance.pigs.Remove(this);
        }

        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
       
        GameObject goScore = Instantiate(score, transform.position + new Vector3( 0,0.5f,0 ) , Quaternion.identity);
        AudioPlay(dead);
        Destroy(goScore, 1.5f);
      
    }

    private void Awake()
    {
        render = GetComponent<SpriteRenderer>( );
    }

    public void AudioPlay(AudioClip clip)
    {
        AudioSource.PlayClipAtPoint(clip, transform.position);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
