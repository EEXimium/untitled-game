using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class spawnorb : MonoBehaviour
{

    private int clickCount;
    public GameObject Panel;
    public TextMeshProUGUI ButtonText;

    //----------------- ORBS -------------------
    public GameObject OrbHolder;
    public GameObject IcePrefab;
    public GameObject NoirPrefab;
    public GameObject ThunderPrefab;

    private void Start()
    {
        clickCount = 1;
    }

    // -------------- BackButton ---------------
    public void OnClick()
    {
        clickCount++;

        if (clickCount % 2 == 0)
        {
            Panel.SetActive(false);
            ButtonText.text = "CARDS";
        }
        else
        {
            Panel.SetActive(true);
            ButtonText.text = "MAP";
        }
    }

    // --------------------- SpawnSection -----------------------
    public void SpawnIceOrb()
    {
        GameObject IceOrb = Instantiate(IcePrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);        
        IceOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnNoirOrb()
    {
        GameObject NoirOrb = Instantiate(NoirPrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);
        NoirOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnThunderOrb()
    {
        GameObject ThunderOrb = Instantiate(ThunderPrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);
        ThunderOrb.transform.parent = OrbHolder.transform;
    }

}
