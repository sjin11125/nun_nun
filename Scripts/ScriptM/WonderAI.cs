using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderAI : MonoBehaviour
    //콜라이더 충돌하면 방향바꾸기
{
    SpriteRenderer []rend;
    SpriteRenderer[] rend2;
    //추가
    public GameObject ai;

    public float speed;
    private float waitTime;
    
    public float startWaitTime;

    public Transform moveSpot;

   /*public float minX;
    public float maxX;
    public float minY;
    public float maxY;
   */


    float minX;
    float maxX;
    float minY;
    float maxY;




    // Start is called before the first frame update
    void Start()
    {
        //추가
        rend = GetComponentsInChildren<SpriteRenderer>();
        waitTime = Random.Range(3, 7);

        //추가 이거를 바꿔서 AI들이 움직이는 범위 지정하면 될거 같음 Good
        minX = Random.Range(-1, 10);
        maxX = Random.Range(-1, 10);
        minY = Random.Range(-8, 0);
        maxY = Random.Range(-8, 0);
        //Vector3 moveSpot = ai.GameObject.transform.position;

        
        waitTime = startWaitTime;
        //moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        moveSpot.position = new Vector2(ai.transform.position.x,ai.transform.position.y); //ai의 position에 spot위치
  
    }

    // Update is called once per frame
    void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);

        if(Vector2.Distance(transform.position,moveSpot.position) < 0.2f)
        {
            if(waitTime <=0)
            {
                moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                if(moveSpot.position.x > ai.transform.position.x)
                {
                    for (int i = 0; i < rend.Length; i++)
                    {
                        rend[i].flipX=true;
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
            }

        }
     
    }

}
