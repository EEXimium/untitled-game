using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ChamberSlotBehavior : MonoBehaviour
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    private CircleCollider2D Ccoll;

    void Start()
    {
        Ccoll = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ActiveChamber"))
        {
            Ccoll.radius = .1f;
            Ccoll.isTrigger = true;
            ChangeAlpha(1f);
        }

    }
    void ChangeAlpha(float alphaValue)
    {
        Color spriteColor = SpriteRenderer.color;
        spriteColor.a = alphaValue;
        SpriteRenderer.color = spriteColor;
    }
}
