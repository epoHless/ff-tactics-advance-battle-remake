using System;
using System.Collections;
using UnityEngine;

public class AbilitiesProcessor : MonoBehaviour
{
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

    private void UseAbility(IEnumerable _ability)
    {
        StartCoroutine(nameof(ExecuteAbility), _ability);
    }

    public IEnumerable ExecuteAbility(IEnumerable _ability)
    {
        yield return _ability;
        EventManager.OnAbilityFinished?.Invoke();
    }

    #endregion
}