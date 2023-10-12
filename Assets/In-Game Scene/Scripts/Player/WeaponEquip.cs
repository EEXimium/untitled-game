using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public GameObject Hand1;
    public GameObject Hand2;

    private GameObject Hand1Weapon;
    private GameObject Hand2Weapon;


    private bool Hand1Online;
    private bool Hand2Online;

    private GameObject Weapon;

    private bool WeaponCheck;
    private string WeaponName;

    public GameObject[] Weapons;
    

    private void Update()
    {
        if (Hand1Online && Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(Hand1Weapon, Hand1.transform.position, this.transform.rotation);
            Destroy(Hand1Weapon);
        }
        if (Hand2Online && Input.GetKeyDown(KeyCode.G))
        {
            Instantiate(Hand2Weapon, Hand2.transform.position, this.transform.rotation);
            Destroy(Hand2Weapon);
        }

        if (WeaponCheck)
        {
            if (Hand1.transform.childCount == 0 && Input.GetKeyDown(KeyCode.E))
            {                
                Hand1.SetActive(true);
                Hand1Online = true;
                Hand1Weapon = Instantiate(Weapon, Hand1.transform.position, Quaternion.identity);
                Hand1Weapon.transform.parent = Hand1.transform;
                Destroy(Weapon);
            }
            else if (Hand2.transform.childCount == 0 && Input.GetKeyDown(KeyCode.E))
            {
                Hand2.SetActive(true);
                Hand2Online = true;
                Hand2Weapon = Instantiate(Weapon, Hand2.transform.position, Quaternion.identity);
                Hand2Weapon.transform.parent = Hand2.transform;
                Destroy(Weapon);
            }

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            WeaponCheck = true;
            Weapon = collision.gameObject;
            WeaponName = collision.gameObject.name;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("weapon"))
        {
            Weapon = null;
            WeaponCheck = false;
            WeaponName = collision.gameObject.name;
        }
    }

}
