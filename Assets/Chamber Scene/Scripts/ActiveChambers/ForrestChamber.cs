using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForrestChamber : MonoBehaviour
{
    private PolygonCollider2D Pcoll;
    private string objectName;
    private int Colidedchambercount = 0;

    private void Start()
    {
        Pcoll = GetComponent<PolygonCollider2D>();
    }

    //private void Update()
    //{
    //    if (Colidedchambercount >= 2) 
    //    {
    //        Pcoll.enabled = false;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectName = collision.gameObject.name;

        if (objectName == "Chamber-Ice(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Cyberpunk(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Noir(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Lava(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Steampunk(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Toxic(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Forrest(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Foggy(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Stock(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Wind(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }

        else if (objectName == "Chamber-Thunder(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
            Colidedchambercount++;
        }
    }
}
