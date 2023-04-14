using System;
using UnityEngine;

[System.Serializable]
public class AnimationBehaviour : CharacterBehaviour
{
    private Animator animator;

    public override void Init(CharacterProcessor processor)
    {
        base.Init(processor);
        animator = processor.Animator;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnCharacterDeath += PlayDeathAnimation;
        EventManager.OnCharacterHalfHP += PlayHalfHPAnimation;
    }

    

    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnCharacterDeath -= PlayDeathAnimation;
        EventManager.OnCharacterHalfHP -= PlayHalfHPAnimation;
    }

    private void PlayDeathAnimation(Character _character)
    {
        if (_character == character)
        {
            animator.SetBool("Dead", true);
        }
    }
    
    private void PlayHalfHPAnimation(Character _character)
    {
        if (_character == character)
        {
            animator.SetBool("HalfHP", true);
        }
    }
}
