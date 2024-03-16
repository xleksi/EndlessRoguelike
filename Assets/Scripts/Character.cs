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
        attributes.Health = new AttributeComponent(attributes.Health.InitialValue);
        attributes.AttackSpeed = new AttributeComponent(attributes.AttackSpeed.InitialValue);
        attributes.AttackDamage = new AttributeComponent(attributes.AttackDamage.InitialValue);
        attributes.MoveSpeed = new AttributeComponent(attributes.MoveSpeed.InitialValue);
    }

    public virtual void TakeDamage(int amount)
    {
        attributes.Health.Subtract(amount);
        if (attributes.Health.Value <= 0)
        {
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