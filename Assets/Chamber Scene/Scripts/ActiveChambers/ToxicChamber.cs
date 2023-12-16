using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToxicChamber : MonoBehaviour
{
    private PolygonCollider2D Pcoll;
    private string objectName;

    public LayerMask excludedLayers;

    //[SerializeField] private GameObject GetBackButton;
    private void Start()
    {
        Pcoll = GetComponent<PolygonCollider2D>();
        //Instantiate(GetBackButton, new Vector3 (960, 800,0), Quaternion.identity, GameObject.FindWithTag("Canvas").transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        objectName = collision.gameObject.name;

        if (objectName == "Chamber-Ice(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Cyberpunk(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Noir(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Lava(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Steampunk(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Toxic(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Forrest(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Foggy(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Stock(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Wind(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }

        else if (objectName == "Chamber-Thunder(Clone)")
        {
            Debug.Log(objectName + " detected by " + this.gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Slot"))
        {
            Pcoll.excludeLayers = excludedLayers;
        }
    }

    //public void OpenChamber()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}
}
