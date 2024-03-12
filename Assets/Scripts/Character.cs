using UnityEngine;

public class Character : MonoBehaviour
{
    private struct Attributes
    {
        public AttributeComponent Health;
        public AttributeComponent Attack;
        public AttributeComponent Movement;
    }

    [SerializeField] private Attributes attributes;
    [SerializeField] private int level;

    protected virtual void Start()
    {
        attributes.Health = new AttributeComponent(attributes.Health.Value);
        attributes.Attack = new AttributeComponent(attributes.Attack.Value);
        attributes.Movement = new AttributeComponent(attributes.Movement.Value);
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

    private Attributes GetAttributes()
    {
        return attributes;
    }

    public int Level
    {
        get { return level; }
        set { level = value; }
    }
}