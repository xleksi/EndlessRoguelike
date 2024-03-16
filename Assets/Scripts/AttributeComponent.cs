using UnityEngine;

[System.Serializable]
public class AttributeComponent
{
    [SerializeField] private int initialValue;
    [SerializeField] private int value;

    public AttributeComponent(int initialValue)
    {
        this.initialValue = initialValue;
        value = initialValue;
    }

    public int InitialValue
    {
        get { return initialValue; }
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
        value -= Mathf.Max(0,value-amount);
    }
}
