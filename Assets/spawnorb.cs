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
    public GameObject SuPrefab;
    public GameObject AtesPrefab;
    public GameObject CimPrefab;

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
    public void SpawnSuOrb()
    {
        GameObject SuOrb = Instantiate(SuPrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);        
        SuOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnAtesOrb()
    {
        GameObject AtesOrb = Instantiate(AtesPrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);
        AtesOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnCimOrb()
    {
        GameObject CimOrb = Instantiate(CimPrefab, OrbHolder.transform.position, OrbHolder.transform.rotation);
        CimOrb.transform.parent = OrbHolder.transform;
    }

}
