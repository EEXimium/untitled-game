using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockCardScript : MonoBehaviour
{
    public RectTransform InfoPanel;
    private GameObject OrbHolder;
    public GameObject orbwhospawn;
    [SerializeField] private GameObject CardChoice;
    public void OnClick()
    {
        CardChoice.SetActive(false);
    }
    private void Start()
    {
        OrbHolder = GameObject.FindWithTag("OrbHolder");
        CardChoice = GameObject.Find("CardChoice");
    }
    public void PositionCorrecter()
    {
        RectTransform CardInfo = this.GetComponentInParent<RectTransform>();
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
    public void SpawnOrbUniversal()
    {
        Instantiate(orbwhospawn, OrbHolder.transform.position, OrbHolder.transform.rotation, OrbHolder.transform);
    }
}
