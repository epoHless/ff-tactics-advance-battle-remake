using UnityEngine;

[System.Serializable]
public class StatisticValue
{
    #region Fields

    private string name;
    
    [SerializeField] private float value;

    #endregion

    #region Properties

    public float Value
    {
        get => value;
        
        set
        {
            this.value = value;
            EventManager.OnStatisticChanged?.Invoke(this);
        }
    }

    #endregion

    #region Operator Overloads

    public static StatisticValue operator + (StatisticValue _left, StatisticValue _right)
    {
        StatisticValue temp = new StatisticValue
        {
            Value = _left.value + _right.value
        };
        
        return temp;
    } 
    
    public static StatisticValue operator - (StatisticValue _left, StatisticValue _right)
    {
        StatisticValue temp = new StatisticValue
        {
            Value = _left.value - _right.value
        };
        
        return temp;
    } 

    #endregion
    
    
}
