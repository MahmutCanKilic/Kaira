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
    private float attack;
    void Start()
    {
        //Attack = 2 gezinme, Attack = 1 attack, Attack = 0 takip
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        switch (gameObject.tag)
        {
            case "Enemy":
                followDistance = 6f;
                hitDistance = new Vector3(1.5f, 0, 0);
                break;
            case "Enemy1":
                followDistance = 10f;
                hitDistance = new Vector3(5f, 0, 0);
                break;
            default:
                break;
        }
        player = GameObject.FindWithTag("Player").transform;
    }


    void FixedUpdate()
    {
        moveSpotX = Mathf.Abs(moveSpot.position.x);
        thisX = transform.position.x;
        playerX = player.position.x;

        PatrolAI();

        if (moveSpotX > thisX && attack == 2)
            transform.localScale = new Vector3(.2f, .25f, 1);
        if (moveSpotX < thisX && attack == 2)
            transform.localScale = new Vector3(-.2f, .25f, 1);

        if (Vector2.Distance(transform.position, player.position) <= hitDistance.x)
        {
            StartCoroutine(Coroutine());
        }

        FollowAI();

    }
    private void PatrolAI()
    {
        if (attack == 2)
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
    }

    private void FollowAI()
    {
        if (Mathf.Abs(thisX - playerX) <= followDistance)
        {
            attack = 0;
            if (player.position.x > transform.position.x && attack == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, -3.672009f) - hitDistance, speed * Time.deltaTime);
                transform.localScale = new Vector3(.2f, .25f, 1);
            }
            else if (player.position.x < transform.position.x && attack == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector3(player.position.x, -3.672009f) + hitDistance, speed * Time.deltaTime);
                transform.localScale = new Vector3(-.2f, .25f, 1);
            }
        }
        if (Vector2.Distance(transform.position, player.position) > followDistance)
        {
            attack = 2;
        }
    }

    private IEnumerator Coroutine()
    {
        Debug.Log("Attack yaptý");
        attack = 1;
        speed = 0;
        yield return new WaitForSeconds(/*attack animasyonunun süresi*/1);
        Debug.Log("Attack bitti");
        attack = 0;
        speed = 4;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitDistance.x);   
    }
}
