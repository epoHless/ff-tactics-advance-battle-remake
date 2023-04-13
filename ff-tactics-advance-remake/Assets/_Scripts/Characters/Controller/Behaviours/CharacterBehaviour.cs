using UnityEngine;

[System.Serializable]
public class CharacterBehaviour
{
    protected Character character;
    
    public virtual void Init(CharacterProcessor processor)
    {
        Debug.Log($"{GetType()} is init!");
        character = processor.Character;
    }

    public virtual void OnEnable()
    {
        Debug.Log($"{GetType()} is enabled!");
    }

    public virtual void OnDisable()
    {
        Debug.Log($"{GetType()} is disabled!");
    }
}
