using UnityEngine;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public struct Attributes
    {
        public AttributeComponent Health;
        public AttributeComponent Attack;
        public AttributeComponent MoveSpeed;
    }

    [SerializeField] private Attributes attributes;
    [SerializeField] private int level;

    protected virtual void Start()
    {
        InitializeAttributes();
    }
    
    protected void InitializeAttributes()
    {
        attributes.Health = new AttributeComponent(attributes.Health.InitialValue);
        attributes.Attack = new AttributeComponent(attributes.Attack.InitialValue);
        attributes.MoveSpeed = new AttributeComponent(attributes.MoveSpeed.InitialValue);
    }

    public virtual void TakeDamage(int amount)
    {
        attributes.Health.Subtract(amount);
        if (attributes.Health.Value <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
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