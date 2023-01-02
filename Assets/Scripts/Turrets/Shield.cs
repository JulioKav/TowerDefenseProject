using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [Header("Unity Stuff")]
    public Image healthBar;

    public float _health = 100;
    public float _maxHealth = 100;

    public Material material;

    private void Start()
    {
        material.SetColor("_Color", new Color(0, 1, 1, 0.25f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy Bullet")
        {
            Destroy(other.gameObject);
            Debug.Log("Shield Hit!");
            material.SetColor("_Color", new Color(1, 0, 0, 0.25f));
            StartCoroutine(colorChange());
        }
    }

    IEnumerator colorChange()
    {
        yield return new WaitForSeconds(0.3f);
        material.SetColor("_Color", new Color(0, 1, 1, 0.25f));
    }

    internal void TakeDamage(float amountOfDamage)
    {
        _health -= amountOfDamage;

        healthBar.fillAmount = _health / _maxHealth;

        if (_health <= 0)
        {//could place death animation here
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
