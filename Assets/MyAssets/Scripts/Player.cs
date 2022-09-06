using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
  
    public GameObject tj, rc, jb;
    public bool Jumpable = false, dJump = true, tJump=false,tJumpActive=false, rocketingEnabler = false;
    public float Hýz;
    public float jumpDistance=1f;
    public static float PlayersY;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "jumpable")
        {
            Jumpable = true;
            dJump = true;
            
            
            if (tJumpActive)
            {
                tJump = true;
            }
        }
       // if (collision.tag == "apple")
       // {
       //     rocketingEnabler = true;
       //     Destroy(collision.gameObject);
       // }
       //
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "jumpable")
        {
            Jumpable = false;
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
       
        
        PlayersY = transform.position.y;
        
        
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector2.right * Time.deltaTime * 1f);
            transform.localScale = new Vector3(-1f, 2f, 1f);
        }
        if (Input.GetKey("a"))
        {
            transform.localScale = new Vector3(1f, 2f, 1f);
            transform.Translate(Vector2.left * Time.deltaTime *  1f);
        }
        if (Input.GetKeyDown("space"))
        {
            if (Jumpable)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Sqrt(9 * Hýz) * jumpDistance), ForceMode2D.Impulse);

            }
            else if (dJump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Sqrt(9 * Hýz) * jumpDistance), ForceMode2D.Impulse);

                dJump = false;
            }
            else if (tJump)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Sqrt(9 * Hýz) * jumpDistance), ForceMode2D.Impulse);
                tJump = false;
            }
       
                
        }
       /* if (Input.GetKeyDown("space") || Input.GetKey("w"))
        {
            if (rocketingEnabler)
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, Mathf.Sqrt(5 * Hýz) * jumpDistance), ForceMode2D.Impulse);
            }
        }*/
    }
          
        
    
   
  
        }

