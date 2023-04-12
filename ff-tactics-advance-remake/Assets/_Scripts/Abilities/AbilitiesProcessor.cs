using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesProcessor : MonoBehaviour
{
    public static bool IsCasting = false;
    
    #region Unity Methods

    private void OnEnable()
    {
        EventManager.OnAbilityUsed += UseAbility;
    }

    private void OnDisable()
    {
        EventManager.OnAbilityUsed -= UseAbility;
    }

    #endregion

    #region Methods

    private void UseAbility(IEnumerator _ability)
    {
        StartCoroutine(ExecuteAbility(_ability));
    }

    IEnumerator ExecuteAbility(IEnumerator _ability)
    {
        IsCasting = true;
        
        yield return _ability;
        EventManager.OnAbilityFinished?.Invoke();

        IsCasting = false;
    }

    #endregion
}