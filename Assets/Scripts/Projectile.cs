using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float DestroyDelay;
    [SerializeField] private GameObject character;
    

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        character = GameObject.FindGameObjectWithTag("Player");
        
        if (character != null)
        {
            Character characterComponent = character.GetComponent<Character>();
            rb = GetComponent<Rigidbody2D>();
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePos - transform.position;
            Vector3 rotation = transform.position - mousePos;
            rb.velocity = new Vector2(direction.x, direction.y).normalized *
                          characterComponent.GetAttributes().AttackSpeed.Value;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot);
            Destroy(gameObject, DestroyDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Character enemyCharacter = collision.GetComponent<Character>();
            if (enemyCharacter != null)
            {
                Character playerCharacter = character.GetComponent<Character>();
                if (playerCharacter != null)
                {
                    enemyCharacter.TakeDamage(playerCharacter.GetAttributes().AttackDamage.Value);
                }
            }
            Destroy(gameObject);
        }
    }
}
