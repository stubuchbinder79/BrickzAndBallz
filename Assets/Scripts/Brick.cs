using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Brick : MonoBehaviour
{
    public int health = 50;
    [SerializeField] private TMP_Text healthText;

    private void Awake()
    {
        healthText = (healthText != null) ? healthText : GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        healthText.text = health.ToString();
    }

    private void TakeDamage() {
        health--;

        if(health <= 0) {
            Debug.LogFormat("Destroyed brick: {0}", this);
            Destroy(gameObject);
            return;
        }
        healthText.text = health.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage();
    }


}
