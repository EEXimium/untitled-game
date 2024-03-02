using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freezearea : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Assuming "Player" is the tag of your player object
            EffectMethods EM = other.GetComponent<EffectMethods>();

            if (EM != null)
            {

                // Start a coroutine to unfreeze the player after 5 seconds
                StartCoroutine(EM.StuckPlayerCoroutine(5f));
            }
        }
    }

    private IEnumerator UnfreezePlayerAfterDelay(PlayerMovement playerMovement, float delay)
    {
        yield return new WaitForSeconds(delay);


    }
}
