using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlots : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";
    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TextMeshProUGUI HasDataText;
    private Button saveSloButton;

    private void Awake()
    {
        saveSloButton = this.GetComponent<Button>();
    }
    public void SetData(GameData data)
    {
        //there's no data for this profileId
        if (data == null)
        {
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
        }
        //there's data for this profileId
        else
        {
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);

            HasDataText.text = "Curent Health: " + data.curentHealth;
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }
    public void SetInteractable(bool interactable)
    {
        saveSloButton.interactable = interactable;
    }
}
