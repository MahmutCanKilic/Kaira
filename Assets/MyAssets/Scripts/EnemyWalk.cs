using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalk : MonoBehaviour
{
    public GameObject obj,zmn;
    public bool isTriggered,isEdge,isBored,left;
    public float kenar1,kenar2,pos,side;

    private void Start()
    {
        side = obj.transform.localScale.x;
        pos = obj.transform.position.x;
        kenar1 = zmn.transform.position.x-4;
        kenar2 = zmn.transform.position.x+4;
    }
    private void Update()
    {
        if (isTriggered)
        {
            //chase durumuna geç
        }
        Edge();
      
    }
    public void  Edge()
    {
        if (kenar1 > pos)
        {
            isEdge = true;
          
        }
        else if (kenar2 < pos)
        {
            isEdge = true;
            
        }
        else
        {
            isEdge = false;
        }
    }
    public void Turn(bool left)
    {

        
           // side = side * -1; alttaki ile ayný
            side *= -1;
            
        
    }
}

