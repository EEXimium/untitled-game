using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponEquip : MonoBehaviour
{

    private float HoldButton = 1f;

    public GameObject Hand1;  // Birincil Silah Slotu.
    public GameObject Hand2;  // Ýkincil Silah Slotu.

    private GameObject Hand1Weapon;  // Birincil Silah.
    private GameObject Hand2Weapon;  // Ýkincil Silah.

    private SpriteRenderer WeaponSprite;  // UI'da Göstermek için sprite çekme.

    private bool Hand1Online = true;  // Hangi Eli kullanýyoruz ?
    private bool Hand2Online = false;  // Hangi Eli kullanýyoruz ?

    private GameObject Weapon;  // Collide Olunan Yerdeki Silah.

    private bool WeaponCheck;  // Silahla collide olundu mu ?
     
    public Image Hand1Sprite, Hand2Sprite;  // UI'da Kullanýlan Silahýn Gösterileceði Yer.

    private void Update() 
    {
        HandSelection();  // LÝNE -- 84 

        if (WeaponCheck)  // Silahla collide olundu mu ?
        {
            if (Hand1Online && Input.GetButton("Interact")) // Birincil Silah Slotu boþ mu ? -- Birincil Slotu mu kullanýyoruz ? -- E tuþu basýldý mý ?
            {
                HoldButton -= Time.deltaTime;
                if (HoldButton <= 0)
                {
                    HoldButton = 1f;
                    if (Hand1.transform.childCount > 0)
                    {
                        DropItem(Hand1Weapon, Hand1, Hand1Sprite);
                    }
                    Hand1Weapon = Instantiate(Weapon, Hand1.transform.position, Quaternion.identity); // Yerdeki Silahý Elde Oluþtur.
                    Hand1Weapon.transform.parent = Hand1.transform;                                  // Birincil Silah Slotunun Child'ý yap.
                    Destroy(Weapon);                                                                // Yerdeki Silahý Sil.
                    Hand1Sprite.sprite = WeaponSprite.sprite;                                      // Silah Sprite'ýný UI'da göster.

                }                                   
            }
            else if (Hand2Online && Input.GetButton("Interact")) // Ýkincil Silah Slotu boþ mu ? -- Ýkincil Slotu mu kullanýyoruz ? -- E tuþu basýldý mý ?
            {
                HoldButton -= Time.deltaTime;
                if (HoldButton <= 0)
                {
                    HoldButton = 1f;
                    if (Hand2.transform.childCount > 0)
                    {
                        DropItem(Hand2Weapon, Hand2, Hand2Sprite);
                    }
                    Hand2Weapon = Instantiate(Weapon, Hand2.transform.position, Quaternion.identity); // Yerdeki Silahý Elde Oluþtur.
                    Hand2Weapon.transform.parent = Hand2.transform;                                  // Ýkincil Silah Slotunun Child'ý yap.
                    Destroy(Weapon);                                                                // Yerdeki Silahý Sil.
                    Hand2Sprite.sprite = WeaponSprite.sprite;                                      // Silah Sprite'ýný UI'da göster.
                }
            }
        }

        //if (Hand1Online && Input.GetKeyDown(KeyCode.G))  // Birincil Slotu mu kullanýyoruz ? -- G tuþu basýldý mý ?
        //{
        //    DropItem(Hand1Weapon, Hand1, Hand1Sprite);   // LÝNE -- 56 
        //}
        //if (Hand2Online && Input.GetKeyDown(KeyCode.G))  // Ýkincil Slotu mu kullanýyoruz ? -- G tuþu basýldý mý ?
        //{
        //    DropItem(Hand2Weapon, Hand2, Hand2Sprite);   // LÝNE -- 56 
        //}
    }
    private void DropItem(GameObject DropHandWeapon, GameObject DropHand, Image DropHandSprite)
    {
        GameObject DroppedWeapon = Instantiate(DropHandWeapon, DropHand.transform.position, this.transform.rotation);  // Elimizdeki Silahý Yerde Oluþtur.
        DroppedWeapon.transform.localScale = DropHandWeapon.transform.lossyScale;                                     // Boyutunu Ayarla.
        SpriteRenderer DroppedWeaponSprite = DroppedWeapon.GetComponent<SpriteRenderer>();                           // Yerde Oluþan Silahýn Sprite'ýný çek.
        DroppedWeaponSprite.flipY = false;                                                                          // Yerde Oluþan Silahýn Yönünü Ayarla.
        Destroy(DropHandWeapon);                                                                                   // Elimizdeki Silahý Sil.
        DropHandSprite.sprite = null;                                                                             // UI'da Silahý Göstermeyi Kaldýr.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            WeaponCheck = true;                                        // Silahla collide olundu mu ? ( EVET )
            Weapon = collision.gameObject;                            // Collidelanan silahý kaydet.
            WeaponSprite = collision.GetComponent<SpriteRenderer>(); // Collidelanan silahýn Sprite'ýný kaydet.        
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            Weapon = null;                       // Collidelanan silah kaydýný sil
            WeaponCheck = false;                 // Silahla collide olundu mu ? ( HAYIR )
        }
    }

    // ---------------- HAND SELECTÝON ----------------------- HAND SELECTÝON ------------------------ HAND SELECTÝON ------------------
    public Image Hand1Color, Hand2Color;         // Kullanýlan Eli belirtmek için çekilen UI Image'larý.
    private void HandSelection()
    {
        if (Input.GetButtonDown("HandSwitch") && !Hand1Online)    // "q" Tuþuna basýldý mý?
        {
            // Hand 1 Activate
            Hand1.SetActive(true);               // Birincil Silah Slotunu Aktifleþtir.
            Hand1Online = true;                 // Birincil Slotu mu kullanýyoruz ? ( EVET )
            ChangeAlpha(Hand1Color, 1);        // Görünürlük = OPAK

            // Hand 2 Deactivate
            Hand2.SetActive(false);             // Ýkincil Silah Slotunu DeAktifleþtir.
            Hand2Online = false;               // Ýkincil Slotu mu kullanýyoruz ? ( HAYIR )
            ChangeAlpha(Hand2Color, 0);       // Görünürlük = SAYDAM
        }
        else if (Input.GetButtonDown("HandSwitch") && !Hand2Online) // "q" Tuþuna basýldý mý?
        {
            // Hand 2 Activate
            Hand2.SetActive(true);             // Ýkincil Silah Slotunu Aktifleþtir.
            Hand2Online = true;               // Ýkincil Slotu mu kullanýyoruz ? ( EVET )
            ChangeAlpha(Hand2Color, 1);      // Görünürlük = OPAK

            // Hand 1 Deactivate
            Hand1.SetActive(false);          // Birincil Silah Slotunu DeAktifleþtir.
            Hand1Online = false;            // Birincil Slotu mu kullanýyoruz ? ( HAYIR )
            ChangeAlpha(Hand1Color, 0);    // Görünürlük = SAYDAM
        }
    }
    private void ChangeAlpha(Image Handname, float alphavalue)  // Görünürlük ( OPAK / SAYDAM)  Ayarlama yeri.
    {
        Color Hand1AlphaColor = Handname.color;
        Hand1AlphaColor.a = alphavalue;
        Handname.color = Hand1AlphaColor;
    }
    // --------------------------------------------------------------------------------------------------------------------------------

}
