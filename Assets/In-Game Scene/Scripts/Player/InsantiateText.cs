using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InsantiateText : MonoBehaviour
{
    public GameObject PrefabText;    

    public void DisplayText(Transform location, Vector3 DisLocation, Quaternion rotation, float TextDurationTime, string TextToDisplay)
    {
        GameObject Text = Instantiate(PrefabText, location.position, rotation);
        TextMeshPro Textmesh = Text.GetComponent<TextMeshPro>();
        Textmesh.text = TextToDisplay;
        Text.transform.Translate(DisLocation);
        Object.Destroy(Text, TextDurationTime);
    }

}
