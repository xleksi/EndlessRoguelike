using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Character character;
    private Transform player;
    public float minDistanceToPlayer = 1f;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float attackTimer;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        character = GetComponent<Character>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!player || !character)
            return;

        Vector3 direction = player.position - transform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer > minDistanceToPlayer)
        {
            direction.Normalize();
            float moveSpeed = character.GetAttributes().MoveSpeed.Value * Time.deltaTime;
            transform.position += direction * moveSpeed;

            if (player.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            if (player.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
            animator.SetBool("isAttacking", false);
        }
        else
        {
            animator.SetBool("isAttacking", true);

            float attackRate = character.GetAttributes().AttackRate.Value;
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackRate)
            {
                attackTimer = 0f;
                if (player != null)
                {
                    Character playerCharacter = player.GetComponent<Character>();
                    playerCharacter.TakeDamage(character.GetAttributes().AttackDamage.Value);
                    float damage = character.GetAttributes().AttackDamage.Value;
                    Debug.Log("Enemy dealt " + damage + " damage to the player.");
                    playerCharacter.TakeDamage(damage);
                }
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            animator.SetBool("isAttacking", true);

            Character playerCharacter = collision.collider.GetComponent<Character>();
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(character.GetAttributes().AttackDamage.Value);
            }
        }
    }
}

