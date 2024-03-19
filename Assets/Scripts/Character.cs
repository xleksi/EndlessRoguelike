using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public struct Attributes
    {
        public AttributeComponent Health;
        public AttributeComponent AttackSpeed;
        public AttributeComponent AttackDamage;
        public AttributeComponent MoveSpeed;
        public AttributeComponent AttackRate;
    }

    [SerializeField] private Attributes attributes;
    [SerializeField] private int level;
    private Animator animator;
    

    protected virtual void Start()
    {
        InitializeAttributes();
        animator = GetComponent<Animator>();
    }
    
    protected void InitializeAttributes()
    {
        float levelMultiplier = 1f + level * 0.25f;
        
        attributes.Health = new AttributeComponent(attributes.Health.InitialValue);
        attributes.AttackSpeed = new AttributeComponent(attributes.AttackSpeed.InitialValue);
        attributes.AttackDamage = new AttributeComponent(attributes.AttackDamage.InitialValue);
        attributes.MoveSpeed = new AttributeComponent(attributes.MoveSpeed.InitialValue);
        attributes.AttackRate = new AttributeComponent(attributes.AttackRate.InitialValue);
        
        attributes.Health.Value *= levelMultiplier;
        attributes.AttackSpeed.Value *= levelMultiplier;
        attributes.AttackDamage.Value *= levelMultiplier;
        attributes.MoveSpeed.Value *= levelMultiplier;
        attributes.AttackRate.Value /= levelMultiplier;
    }

    public virtual void TakeDamage(float amount)
    {
        attributes.Health.Subtract(amount);
        if (attributes.Health.Value <= 0)
        {
            animator.Play("Death");
            Die();
        }
        else
        {
            animator.Play("Hurt");
        }
    }
    

    protected virtual void Die()
    {
        animator.SetBool("isAlive",false);
        Destroy(gameObject);
    }

    public Attributes GetAttributes()
    {
        return attributes;
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}