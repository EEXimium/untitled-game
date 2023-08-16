using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class codex : MonoBehaviour
{
    private bool isCodexOpen = false;
    VisualElement root;
    float originalOpacity = 1.0f;
    VisualElement silahlarPage;
    VisualElement dusmanlarPage;
    VisualElement testPage;

    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        Button silahlar = root.Q<Button>("silahlarButton");
        Button dusmanlar = root.Q<Button>("dusmanlarButton");
        Button test = root.Q<Button>("testButton");

        silahlarPage = root.Q<VisualElement>("silahlarpage");
        dusmanlarPage = root.Q<VisualElement>("dusmanlarpage");
        testPage = root.Q<VisualElement>("testpage");

        // Add click handlers for the buttons
        silahlar.clicked += ShowSilahlarPage;
        dusmanlar.clicked += ShowDusmanlarPage;
        test.clicked += ShowTestPage;

        Button testSilah1 = root.Q<Button>("testSilah1");
        Button testSilah2 = root.Q<Button>("testSilah2");



    }
    // Start is called before the first frame update
    void Start()
    {
        originalOpacity = root.resolvedStyle.opacity;
        root.SetEnabled(false);
        root.style.opacity = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isCodexOpen)
            {
                Time.timeScale = 0; // Pause the game
                //codexUI.SetActive(true); // Show the codex
                root.SetEnabled(true);
                root.style.opacity = originalOpacity;
                isCodexOpen = true;
            }
            else
            {
                Time.timeScale = 1; // Resume the game
                //codexUI.SetActive(false); // Hide the codex UI
                root.SetEnabled(false);
                root.style.opacity = 0.0f;
                HideAllPages();
                isCodexOpen = false;
            }
        }
    }

    private void ShowSilahlarPage()
    {
        HideAllPages();
        silahlarPage.style.display = DisplayStyle.Flex;
    }

    private void ShowDusmanlarPage()
    {
        HideAllPages();
        dusmanlarPage.style.display = DisplayStyle.Flex;
    }

    private void ShowTestPage()
    {
        HideAllPages();
        testPage.style.display = DisplayStyle.Flex;
    }
    private void HideAllPages()
    {
        silahlarPage.style.display = DisplayStyle.None;
        dusmanlarPage.style.display = DisplayStyle.None;
        testPage.style.display = DisplayStyle.None;
    }
}
