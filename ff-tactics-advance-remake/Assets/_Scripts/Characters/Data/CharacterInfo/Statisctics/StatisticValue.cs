using UnityEngine;

[System.Serializable]
public class StatisticValue
{
    #region Fields
    
    [SerializeField] private float value;

    #endregion

    #region Properties

    public int ID { get; set; }
    
    public float Value
    {
        get => value;
        
        set
        {
            var previousID = ID;
            this.value = value;
            ID = previousID;
            
            EventManager.OnStatisticChanged?.Invoke(this);
        }
    }

    #endregion

    #region Constructor

    public StatisticValue() { }
    
    public StatisticValue(int _id)
    {
        ID = _id;
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
