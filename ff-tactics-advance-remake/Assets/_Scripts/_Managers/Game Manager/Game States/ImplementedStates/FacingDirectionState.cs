using UnityEngine;
using UnityEngine.InputSystem;
using InputSystem = FinalFantasy.InputSystem;

public class FacingDirectionState : GameState
{
    private FacingDirectionHolder facingDirectionHolder;
    
    public FacingDirectionState(FacingDirectionHolder _facingDirectionHolder)
    {
        facingDirectionHolder = _facingDirectionHolder;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        InputSystem.EnableFacingDirection();
        
        facingDirectionHolder.gameObject.SetActive(true);
        facingDirectionHolder.Init(MovementManager.Instance.Character.transform.position + Vector3.up * 1.4f);
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);
        
        if (InputSystem.WasConfirmPressed)
        {
            _manager.ChangeState(_manager.menuState);
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        InputSystem.DisableFacingDirection();

        facingDirectionHolder.gameObject.SetActive(false);
    }
}
