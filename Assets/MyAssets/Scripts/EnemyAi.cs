using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyAi : MonoBehaviour
{
    public Transform moveSpot;
    private float waitTime;
    
    public float minX, maxX, minY, maxY, speed, startWaitTime;
    private float moveSpotX, thisX, playerX;
    private Transform player;
    private Vector3 hitDistance;
    private bool takipediyormu;
    private float followDistance;

    void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        if (gameObject.tag == "Enemy")
        {
            followDistance = 6f;
            hitDistance = new Vector3(1.5f, 0, 0);
        }
        
        if (gameObject.tag == "Enemy1")
        {
            followDistance = 10f;
            hitDistance = new Vector3(5f, 0, 0);
        }
        player = GameObject.FindWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        moveSpotX = Mathf.Abs(moveSpot.position.x);
        thisX = transform.position.x;
        playerX = player.position.x;
        
        

        if (!takipediyormu)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }



        if (moveSpotX > thisX && !takipediyormu)
            transform.localScale = new Vector3(.2f, .25f, 1);
        if (moveSpotX < thisX && !takipediyormu)
            transform.localScale = new Vector3(-.2f, .25f, 1);

        if (Mathf.Abs(thisX - playerX) <= followDistance)
        {
            takipediyormu = true;
            if (player.position.x > transform.position.x && takipediyormu)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, -3.672009f) - hitDistance, speed * Time.deltaTime);
                transform.localScale = new Vector3(.2f, .25f, 1);
            }
            else if (player.position.x < transform.position.x && takipediyormu)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, -3.672009f) + hitDistance, speed * Time.deltaTime);
                transform.localScale = new Vector3(-.2f, .25f, 1);
            }
        }
        if (Vector2.Distance(transform.position, player.position) > followDistance)
        {
            takipediyormu = false;
        }
    }

}
