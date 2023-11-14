using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item(Kaan)")]

public class ItemSO : ScriptableObject
{
   public string itemName;
   public StatToChange statToChange = new StatToChange();
   public int amountToChangeStat;

   public AttributeToChange attributeToChange = new AttributeToChange();
   public int amountToChangeAttribute;

   public void UseItem()
   {
 //      if(StatToChange = StatToChange.health)
 //      {
 //         GameObject.Find("HealthManager").GetComponent<PlayerHealth>.ChangeHealth(amountToChangeStat);
 //      }
 //      if(StatToChange = StatToChange.health)
 //      {
 //         GameObject.Find("HealthManager").GetComponent<PlayerHealth>.ChangeHealth(amountToChangeStat);
 //      }
 //      if(StatToChange = StatToChange.health)
 //      {
 //         GameObject.Find("HealthManager").GetComponent<PlayerHealth>.ChangeHealth(amountToChangeStat);
 //      }
   }
   
   public enum StatToChange
   {
       none,
       health,
       movement
   };

   public enum AttributeToChange
   {
       none,
       buff,
       debuff,
       defense
   };
     
}
