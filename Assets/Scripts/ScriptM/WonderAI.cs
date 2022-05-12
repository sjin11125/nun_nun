using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderAI : MonoBehaviour

{

    //콜라이더 충돌하면 방향바꾸기

    SpriteRenderer[] rend;
    SpriteRenderer[] rend2;

    //public Animator animator;
    //추가
    public GameObject ai;
    //public Animator ani;

    public float speed;
    private float waitTime;

    public float startWaitTime;

    public Transform moveSpot;


    float minX;
    float maxX;
    float minY;
    float maxY;




    // Start is called before the first frame update
    void Start()
    {
        //ani = gameObject.GetComponent<Animator>();

        //추가
        rend = GetComponentsInChildren<SpriteRenderer>();
        waitTime = Random.Range(3, 7); // 3~7초동안 기다림 

        //추가 이거를 바꿔서 AI들이 움직이는 범위 지정하면 될거 같음 Good 
        // 이거의 좌표를 지금 맵 전체로 잡아야 할 것 같음. 그리고 벽을 칠해서 거기 닿이면 방향반대로 하면 될 것 같은데 
        //벽만들고 벽/건물충돌시 방향반대, 누니는 무시하기
        minX = Random.Range(-10, 0);
        maxX = Random.Range(0, 10);
        minY = Random.Range(-11, 0);
        maxY = Random.Range(0, 7);
        //Vector3 moveSpot = ai.GameObject.transform.position;


        waitTime = startWaitTime;
        //moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        moveSpot.position = new Vector2(ai.transform.position.x, ai.transform.position.y); //ai의 position에 spot위치 // move 위치찍기

    }



    // Update is called once per frame
    void Update()
    {
        //animator.SetBool("Walk", true);
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
        //충돌하면 X방향 바꾸기
        //ani.Play("nuni_walk");
        if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f) // 거리가 0.2f가안되면 새로운 스팟 찾기
        {
            if (waitTime <= 0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

                if (moveSpot.position.x > ai.transform.position.x)
                {

                    for (int i = 0; i < rend.Length; i++) // 뒤집히기
                    {
                        rend[i].flipX = true;
                    }
                }
                else
                {
                    for (int i = 0; i < rend.Length; i++)
                    {
                        rend[i].flipX = false;
                    }
                }

                waitTime = startWaitTime;




                // wait-deltaTime=0이 되면 다음포지션을 정한다. 
            }
            else
            {
                waitTime -= Time.deltaTime; //wait - deltaTIme=0이 될 때 까지 움직여라


                //얘가 움직이는 함수

            }



        }
        else
        {

            //ani.Play("1nuni_walk");
            //ani.Play("nuni_walk"); 기본이 
        }

    }

    public void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Building")
        {
            //반대쪽 X좌표를 찍게 해야 할 것 같음 
            //if ( this.transform.position.x < moveSpot.position.x)
            moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));

            if (moveSpot.position.x > ai.transform.position.x)
            {

                for (int i = 0; i < rend.Length; i++) // 뒤집히기
                {
                    rend[i].flipX = true;
                }
            }
            else
            {
                for (int i = 0; i < rend.Length; i++)
                {
                    rend[i].flipX = false;
                }
            }

            waitTime = startWaitTime;




            // wait-deltaTime=0이 되면 다음포지션을 정한다. 
        }

    }
}

