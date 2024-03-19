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
        attributes.Health = new AttributeComponent(attributes.Health.InitialValue);
        attributes.AttackSpeed = new AttributeComponent(attributes.AttackSpeed.InitialValue);
        attributes.AttackDamage = new AttributeComponent(attributes.AttackDamage.InitialValue);
        attributes.MoveSpeed = new AttributeComponent(attributes.MoveSpeed.InitialValue);
        attributes.AttackRate = new AttributeComponent(attributes.AttackRate.InitialValue);
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

    private void LevelUpModifiers()
    {
        float modifiedAttackSpeed = attributes.AttackSpeed.Value * (1 + level * 0.05f);
        float modifiedWalkSpeed = attributes.MoveSpeed.Value * (1 + level * 0.05f);
    
        animator.SetFloat("AttackSpeedMultiplier", modifiedAttackSpeed);
        animator.SetFloat("WalkSpeedMultiplier", modifiedWalkSpeed);
    }
}