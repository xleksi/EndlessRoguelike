using UnityEngine;

[System.Serializable]
public class AttributeComponent
{
    [SerializeField] private int value;

    public AttributeComponent(int initialValue)
    {
        value = initialValue;
    }

    public int Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void Add(int amount)
    {
        value += amount;
    }

    public void Subtract(int amount)
    {
        value -= amount;
    }
}
