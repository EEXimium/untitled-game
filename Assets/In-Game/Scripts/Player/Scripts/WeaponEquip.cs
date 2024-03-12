using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquip : MonoBehaviour
{

    private float HoldButton = 1f;

    public GameObject Hand1;  // Birincil Silah Slotu.
    public GameObject Hand2;  // �kincil Silah Slotu.

    private GameObject Hand1Weapon;  // Birincil Silah.
    private GameObject Hand2Weapon;  // �kincil Silah.

    private SpriteRenderer WeaponSprite;  // UI'da G�stermek i�in sprite �ekme.

    private bool Hand1Online = true;  // Hangi Eli kullan�yoruz ?
    private bool Hand2Online = false;  // Hangi Eli kullan�yoruz ?

    private GameObject Weapon;  // Collide Olunan Yerdeki Silah.

    private bool WeaponCheck;  // Silahla collide olundu mu ?
     
    public Image Hand1Sprite, Hand2Sprite;  // UI'da Kullan�lan Silah�n G�sterilece�i Yer.

    private void Update() 
    {
        HandSelection();  // L�NE -- 84 

        if (WeaponCheck)  // Silahla collide olundu mu ?
        {
            if (Hand1Online && Input.GetButton("Interact")) // Birincil Silah Slotu bo� mu ? -- Birincil Slotu mu kullan�yoruz ? -- E tu�u bas�ld� m� ?
            {
                HoldButton -= Time.deltaTime;
                if (HoldButton <= 0)
                {
                    HoldButton = 1f;
                    if (Hand1.transform.childCount > 0)
                    {
                        DropItem(Hand1Weapon, Hand1, Hand1Sprite);
                    }
                    Hand1Weapon = Instantiate(Weapon, Hand1.transform.position, Quaternion.identity); // Yerdeki Silah� Elde Olu�tur.
                    Hand1Weapon.transform.parent = Hand1.transform;                                  // Birincil Silah Slotunun Child'� yap.
                    Destroy(Weapon);                                                                // Yerdeki Silah� Sil.
                    Hand1Sprite.sprite = WeaponSprite.sprite;                                      // Silah Sprite'�n� UI'da g�ster.

                }                                   
            }
            else if (Hand2Online && Input.GetButton("Interact")) // �kincil Silah Slotu bo� mu ? -- �kincil Slotu mu kullan�yoruz ? -- E tu�u bas�ld� m� ?
            {
                HoldButton -= Time.deltaTime;
                if (HoldButton <= 0)
                {
                    HoldButton = 1f;
                    if (Hand2.transform.childCount > 0)
                    {
                        DropItem(Hand2Weapon, Hand2, Hand2Sprite);
                    }
                    Hand2Weapon = Instantiate(Weapon, Hand2.transform.position, Quaternion.identity); // Yerdeki Silah� Elde Olu�tur.
                    Hand2Weapon.transform.parent = Hand2.transform;                                  // �kincil Silah Slotunun Child'� yap.
                    Destroy(Weapon);                                                                // Yerdeki Silah� Sil.
                    Hand2Sprite.sprite = WeaponSprite.sprite;                                      // Silah Sprite'�n� UI'da g�ster.
                }
            }
        }

        //if (Hand1Online && Input.GetKeyDown(KeyCode.G))  // Birincil Slotu mu kullan�yoruz ? -- G tu�u bas�ld� m� ?
        //{
        //    DropItem(Hand1Weapon, Hand1, Hand1Sprite);   // L�NE -- 56 
        //}
        //if (Hand2Online && Input.GetKeyDown(KeyCode.G))  // �kincil Slotu mu kullan�yoruz ? -- G tu�u bas�ld� m� ?
        //{
        //    DropItem(Hand2Weapon, Hand2, Hand2Sprite);   // L�NE -- 56 
        //}
    }
    private void DropItem(GameObject DropHandWeapon, GameObject DropHand, Image DropHandSprite)
    {
        GameObject DroppedWeapon = Instantiate(DropHandWeapon, DropHand.transform.position, this.transform.rotation);  // Elimizdeki Silah� Yerde Olu�tur.
        DroppedWeapon.transform.localScale = DropHandWeapon.transform.lossyScale;                                     // Boyutunu Ayarla.
        SpriteRenderer DroppedWeaponSprite = DroppedWeapon.GetComponent<SpriteRenderer>();                           // Yerde Olu�an Silah�n Sprite'�n� �ek.
        DroppedWeaponSprite.flipY = false;                                                                          // Yerde Olu�an Silah�n Y�n�n� Ayarla.
        Destroy(DropHandWeapon);                                                                                   // Elimizdeki Silah� Sil.
        DropHandSprite.sprite = null;                                                                             // UI'da Silah� G�stermeyi Kald�r.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            WeaponCheck = true;                                        // Silahla collide olundu mu ? ( EVET )
            Weapon = collision.gameObject;                            // Collidelanan silah� kaydet.
            WeaponSprite = collision.GetComponent<SpriteRenderer>(); // Collidelanan silah�n Sprite'�n� kaydet.        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            Weapon = null;                       // Collidelanan silah kayd�n� sil
            WeaponCheck = false;                 // Silahla collide olundu mu ? ( HAYIR )
        }
    }

    // ---------------- HAND SELECT�ON ----------------------- HAND SELECT�ON ------------------------ HAND SELECT�ON ------------------
    public Image Hand1Color, Hand2Color;         // Kullan�lan Eli belirtmek i�in �ekilen UI Image'lar�.
    private void HandSelection()
    {
        if (Input.GetButtonDown("HandSwitch") && !Hand1Online)    // "q" Tu�una bas�ld� m�?
        {
            // Hand 1 Activate
            Hand1.SetActive(true);               // Birincil Silah Slotunu Aktifle�tir.
            Hand1Online = true;                 // Birincil Slotu mu kullan�yoruz ? ( EVET )
            ChangeAlpha(Hand1Color, 1);        // G�r�n�rl�k = OPAK

            // Hand 2 Deactivate
            Hand2.SetActive(false);             // �kincil Silah Slotunu DeAktifle�tir.
            Hand2Online = false;               // �kincil Slotu mu kullan�yoruz ? ( HAYIR )
            ChangeAlpha(Hand2Color, 0);       // G�r�n�rl�k = SAYDAM
        }
        else if (Input.GetButtonDown("HandSwitch") && !Hand2Online) // "q" Tu�una bas�ld� m�?
        {
            // Hand 2 Activate
            Hand2.SetActive(true);             // �kincil Silah Slotunu Aktifle�tir.
            Hand2Online = true;               // �kincil Slotu mu kullan�yoruz ? ( EVET )
            ChangeAlpha(Hand2Color, 1);      // G�r�n�rl�k = OPAK

            // Hand 1 Deactivate
            Hand1.SetActive(false);          // Birincil Silah Slotunu DeAktifle�tir.
            Hand1Online = false;            // Birincil Slotu mu kullan�yoruz ? ( HAYIR )
            ChangeAlpha(Hand1Color, 0);    // G�r�n�rl�k = SAYDAM
        }
    }
    private void ChangeAlpha(Image Handname, float alphavalue)  // G�r�n�rl�k ( OPAK / SAYDAM)  Ayarlama yeri.
    {
        Color Hand1AlphaColor = Handname.color;
        Hand1AlphaColor.a = alphavalue;
        Handname.color = Hand1AlphaColor;
    }
    // --------------------------------------------------------------------------------------------------------------------------------

}
