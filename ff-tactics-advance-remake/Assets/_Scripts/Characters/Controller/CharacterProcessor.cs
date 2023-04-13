using System.Collections.Generic;
using UnityEngine;

public class CharacterProcessor : MonoBehaviour
{
    #region Properties

    [field: SerializeField, SerializeReference] public List<CharacterBehaviour> Behaviours { get; private set; } = new List<CharacterBehaviour>();
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public SkinnedMeshRenderer MeshRenderer { get; private set; }

    public Character Character { get; private set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        Character = GetComponent<Character>();
        
        foreach (var behaviour in Behaviours)
        {
            behaviour.Init(this);
        }
    }

    private void OnEnable()
    {
        foreach (var behaviour in Behaviours)
        {
            behaviour.OnEnable();
        }
    }

    private void OnDisable()
    {
        foreach (var behaviour in Behaviours)
        {
            behaviour.OnDisable();
        }
    }

    #endregion

    [ContextMenu("Add Animation")]
    public void AddAnim()
    {
        Behaviours.Add(new AnimationBehaviour());
    }
    
    [ContextMenu("Add Material")]
    public void AddMat()
    {
        Behaviours.Add(new MaterialBehaviour());
    }
}
