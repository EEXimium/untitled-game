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
    VisualElement itemPage;


    private List<string> collectedWeapons = new List<string>();
    public Texture2D lockedSprite; // Assign the locked image sprite in the inspector
    public Dictionary<string, bool> unlockedWeapons = new Dictionary<string, bool>(); // Assign unlocked weapons in the inspector
    private Dictionary<string, Texture2D> weaponSprites = new Dictionary<string, Texture2D>();
    private void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        //Main buttons
        Button silahlar = root.Q<Button>("silahlarButton");
        Button dusmanlar = root.Q<Button>("dusmanlarButton");
        Button test = root.Q<Button>("testButton");

        //Pages
        silahlarPage = root.Q<VisualElement>("silahlarpage");
        dusmanlarPage = root.Q<VisualElement>("dusmanlarpage");
        testPage = root.Q<VisualElement>("testpage");
        itemPage = root.Q<VisualElement>("itempage");

        //Main button click handlers
        silahlar.clicked += ShowSilahlarPage;
        dusmanlar.clicked += ShowDusmanlarPage;
        test.clicked += ShowTestPage;

        WeaponButtons();

    }
    // Start is called before the first frame update
    void Start()
    {
        originalOpacity = root.resolvedStyle.opacity;
        root.SetEnabled(false);
        root.style.opacity = 0.0f;
        HideAllPages();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!isCodexOpen)
            {
                Time.timeScale = 0; // Pause the game
                root.SetEnabled(true);
                root.style.opacity = originalOpacity;
                isCodexOpen = true;
            }
            else
            {
                Time.timeScale = 1; // Resume the game
                root.SetEnabled(false);
                root.style.opacity = 0.0f;
                HideAllPages();
                isCodexOpen = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1; // Resume the game
            root.SetEnabled(false);
            root.style.opacity = 0.0f;
            HideAllPages();
            isCodexOpen = false;
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
        itemPage.style.display = DisplayStyle.None;
    }

    private void ShowWeaponInfo(string weaponName, string description)
    {
        itemPage.style.display = DisplayStyle.Flex;
        silahlarPage.style.display = DisplayStyle.None;
        Button close = itemPage.Q<Button>("closeButton");
        close.clicked += ShowSilahlarPage;

        Label weaponNameLabel = itemPage.Q<Label>("weaponNameLabel");
        Label descriptionLabel = itemPage.Q<Label>("descriptionLabel");

        weaponNameLabel.text = weaponName;
        descriptionLabel.text = description;
    }

    private void WeaponButtonClicked(string weaponName, string description)
    {
        ShowWeaponInfo(weaponName, description);
    }

    private void WeaponButtons()
    {
        Button testSilah1 = silahlarPage.Q<Button>("testSilah1");
        //testSilah1.clicked += () => WeaponButtonClicked("keremiyoketinator", "keremi varoluþtan silmeye yarar");
        WeaponButtonUpdater(testSilah1, "keremiyoketinator", "keremi varoluþtan silmeye yarar");
        

        Button testSilah2 = silahlarPage.Q<Button>("testSilah2");
        WeaponButtonUpdater(testSilah2, "steampunkgun1", "dumanserserisi silahý piyuv piyuv");
        //testSilah2.clicked += () => WeaponButtonClicked("Weapon Name 2", "Description 2");

        Button testSilah3 = silahlarPage.Q<Button>("testSilah3");
        WeaponButtonUpdater(testSilah3, "bomba", "lan patlicak");

        // daha çok silah eklendikçe isimlerini ve açýklamalarýný buraya yazacaðýz. Butonlarýný burada atayacaðýz.
    }

    private void WeaponButtonUpdater(Button button, string weaponName, string description)
    {
        if (unlockedWeapons.ContainsKey(weaponName) && unlockedWeapons[weaponName])
        {
            // Weapon is unlocked
            button.SetEnabled(true);
            //button.style.backgroundImage = unlockedSprites[weaponName]; // Set unlocked weapon image
            button.style.backgroundImage = weaponSprites[weaponName];
            
            button.clicked += () => WeaponButtonClicked(weaponName, description);
        }
        else
        {
            // Weapon is locked
            button.SetEnabled(false);
            button.style.backgroundImage = lockedSprite; // Set locked image
        }
    }

    public void CollectedWeaponUpdater(string weaponName, Texture2D weaponSprite)
    {
        collectedWeapons.Add(weaponName);
        unlockedWeapons[weaponName] = true;
        weaponSprites[weaponName] = weaponSprite;

        // Update the UI buttons
        WeaponButtons();
    }
}
