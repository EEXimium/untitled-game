using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class spawnorb : MonoBehaviour
{
    public RectTransform InfoPanel;
    public GameObject itself;

    //----------------- ORBS -------------------
    [SerializeField] private GameObject OrbHolder;
    [SerializeField] private GameObject CardChoice;

    public List<GameObject> Orbs;

    private void Start()
    {
        OrbHolder = GameObject.Find("orbHolder");
        CardChoice = GameObject.Find("CardChoice");
    }

    // --------------------- ORB Spawn Section -----------------------
    public void SpawnIceOrb()
    {
        GameObject IceOrb = Instantiate(Orbs[0], OrbHolder.transform.position, OrbHolder.transform.rotation);        
        IceOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnNoirOrb()
    {
        GameObject NoirOrb = Instantiate(Orbs[1], OrbHolder.transform.position, OrbHolder.transform.rotation);
        NoirOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnThunderOrb()
    {
        GameObject ThunderOrb = Instantiate(Orbs[2], OrbHolder.transform.position, OrbHolder.transform.rotation);
        ThunderOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnLavaOrb()
    {
        GameObject LavaOrb = Instantiate(Orbs[3], OrbHolder.transform.position, OrbHolder.transform.rotation);
        LavaOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnFoggyOrb()
    {
        GameObject FoggyOrb = Instantiate(Orbs[4], OrbHolder.transform.position, OrbHolder.transform.rotation);
        FoggyOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnToxicOrb()
    {
        GameObject ToxicOrb = Instantiate(Orbs[5], OrbHolder.transform.position, OrbHolder.transform.rotation);
        ToxicOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnWindOrb()
    {
        GameObject WindOrb = Instantiate(Orbs[6], OrbHolder.transform.position, OrbHolder.transform.rotation);
        WindOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnForrestOrb()
    {
        GameObject ForrestOrb = Instantiate(Orbs[7], OrbHolder.transform.position, OrbHolder.transform.rotation);
        ForrestOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnSteampunkOrb()
    {
        GameObject SteampunkOrb = Instantiate(Orbs[8], OrbHolder.transform.position, OrbHolder.transform.rotation);
        SteampunkOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnStockOrb()
    {
        GameObject StockOrb = Instantiate(Orbs[9], OrbHolder.transform.position, OrbHolder.transform.rotation);
        StockOrb.transform.parent = OrbHolder.transform;
    }
    public void SpawnCyberpunkOrb()
    {
        GameObject CyberpunkOrb = Instantiate(Orbs[10], OrbHolder.transform.position, OrbHolder.transform.rotation);
        CyberpunkOrb.transform.parent = OrbHolder.transform;
    }
    public void OnClick()
    {
        CardChoice.SetActive(false);
    }
    // ----------------------------------------------------------------------
    public void PositionCorrecter()
    {
        RectTransform CardInfo = itself.GetComponentInParent<RectTransform>();
        if (CardInfo.position.x < 491)
        {
            InfoPanel.localPosition = new Vector3(761, 0, 0);
            Debug.Log("übele");
        }
        else if (CardInfo.position.x > 1300)
        {
            InfoPanel.localPosition = new Vector3(-761, 0, 0);
            Debug.Log("übele");
        }
        else
        {
            InfoPanel.localPosition = new Vector3(0, 0, 0);
        }
    }
}
