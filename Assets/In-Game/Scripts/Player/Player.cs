using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private Slider HealthSlider;
    public float maxHealth;
    public float currentHealth;
    [SerializeField] private Color damageColor = Color.red;
    public bool isDead = false; //karakterin öldüğü fonksiyon çalışıyor mu inspector'dan görmek için public bırakıldı
    public Gradient gradient;
    public Image fill;
    public SpriteRenderer eyefill;
    public InsantiateText InsText;
    private SpriteRenderer charSpriteR;

    [Header("Movement Settings")]
    public Rigidbody2D rb;
    [SerializeField] public float movespeed = 3f;
    [SerializeField] public float sprintspeed = 1f;
    public Vector2 InputVector;
    public codex CodexScript;
    public GameObject UIDoc;
    public Animator animator;
    private Vector2 newPos;
    private Vector2 currentPos;

    [Header("Other Settings")]
    public int coinsCollected;
    public EffectMethods EM;
    [SerializeField] private TextMeshProUGUI CoinsCountText;

    void Start()
    {
        SetMaxHealth(maxHealth);
        //SetHealth(currentHealth);
        charSpriteR = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        EM = GetComponent<EffectMethods>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }

// ----------- Move ------------
        currentPos = rb.position;
        InputVector.x = Input.GetAxisRaw("Horizontal");
        InputVector.y = Input.GetAxisRaw("Vertical");

        InputVector.Normalize(); //Diagonal hareketin bozuk h�zl� olmamas� i�in
        InputVector = Vector2.ClampMagnitude(InputVector, 1);    // Diagonal movement 1,4 => 1

        if (!EM.isStuck) //&& !DialogueManager.instance.isDialogueActive)
        {
            if (Input.GetButton("Sprint"))
            {
                newPos = currentPos + (InputVector * (movespeed + sprintspeed) * Time.fixedDeltaTime);
            }
            else
            {
                newPos = currentPos + (InputVector * movespeed * Time.fixedDeltaTime);
            }

            rb.MovePosition(newPos);
        }

    }//end of update

    private IEnumerator ColorShift()
    {
        charSpriteR.color = damageColor;
        yield return new WaitForSeconds(0.1f);
        charSpriteR.color = Color.white;
    }

    public void TakeDamage(float damage) 
    {
        if (!isDead)
        {
            currentHealth -= damage;
            StartCoroutine(ColorShift());
            SetHealth(currentHealth);
            InsText.DisplayText(this.gameObject.transform, new Vector3(0, 1, 0), Quaternion.identity, .8f, "Ughh!");

            Debug.Log("Player takes damage: " + damage);
            Debug.Log("Player takes damage. Current Health: " + currentHealth);
        }
        if (currentHealth <= 0) { Die(); }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Öldün");
    }

    public void SetMaxHealth(float health)
    {
        HealthSlider.maxValue = health;
        fill.color = gradient.Evaluate(1f);
        eyefill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        HealthSlider.value = health;
        fill.color = gradient.Evaluate(HealthSlider.normalizedValue);
        eyefill.color = gradient.Evaluate(HealthSlider.normalizedValue);
    }

    public void SetCoins(int count)
    {
        CoinsCountText.text = count.ToString();
    }

    private void CollectCoin(GameObject coin)
    {
        coinsCollected++;
        SetCoins(coinsCollected);
        Destroy(coin);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
            CollectCoin(other.gameObject);
    }
}
