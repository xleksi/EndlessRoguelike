using UnityEngine;

[System.Serializable]
public class AttributeComponent
{
    [SerializeField] private float initialValue;
    [SerializeField] private float value;

    public AttributeComponent(float initialValue)
    {
        this.initialValue = initialValue;
        value = initialValue;
    }

    public float InitialValue
    {
        get { return initialValue; }
        set { this.value = value; }
    }
    
    public float Value
    {
        get { return value; }
        set { this.value = value; }
    }

    public void Add(float amount)
    {
        value += amount;
    }

    public void Subtract(float amount)
    {
        value -= amount;
    }
}
