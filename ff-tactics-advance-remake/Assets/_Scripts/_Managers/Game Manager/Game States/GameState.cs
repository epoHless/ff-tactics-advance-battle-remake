using System;
using UnityEngine;

public abstract class GameState
{
    #region Methods

    public virtual void OnEnter(GameManager _manager){ Debug.Log($"Entering: {GetType()}"); }
    public virtual void OnUpdate(GameManager _manager){}
    public virtual void OnExit(GameManager _manager) { Debug.Log($"Exiting: {GetType()}"); }

    #endregion
}
