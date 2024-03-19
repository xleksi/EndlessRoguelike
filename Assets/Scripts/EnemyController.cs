using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Character character;
    private Transform player;
    public float minDistanceToPlayer = 1f;
    private SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        character = FindObjectOfType<Character>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        }
    }
}
