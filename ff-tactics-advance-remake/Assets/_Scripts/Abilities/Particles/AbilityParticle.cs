using System;
using System.Collections;
using UnityEngine;

public class AbilityParticle : MonoBehaviour
{
    #region Fields

    private ParticleSystem ParticleSystem;
    private ParticleSystem.MainModule emittersModule;

    public bool IsPlaying = false;
    private WaitUntil WaitUntil;

    #endregion

    #region Properties

    #endregion

    #region Unity Methods

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        emittersModule = ParticleSystem.main;
        
        WaitUntil = new WaitUntil(() => !IsPlaying);
    }
    
    protected virtual void OnEnable()
    {
        emittersModule.stopAction = ParticleSystemStopAction.Callback;

        IsPlaying = true;
    }

    protected virtual void OnDisable()
    {
        emittersModule.stopAction = ParticleSystemStopAction.None;
    }

    private void OnParticleSystemStopped()
    {
        IsPlaying = false;
        Destroy(gameObject);
    }

    #endregion

    #region Particle Methods

    public IEnumerable PlayParticles(Vector3 _position, Vector3 _direction)
    {
        transform.position = _position;

        var direction = _direction - transform.position;
        transform.forward = direction;
        
        yield return PlayParticles();
    }
    
    public IEnumerable PlayParticles(Vector3 _position)
    {
        transform.position = _position;
        yield return PlayParticles();
    }

    private IEnumerable PlayParticles()
    {
        IsPlaying = true;
        ParticleSystem.Play(true);
        yield return new WaitUntil(() => !IsPlaying);
    }

    #endregion
}
