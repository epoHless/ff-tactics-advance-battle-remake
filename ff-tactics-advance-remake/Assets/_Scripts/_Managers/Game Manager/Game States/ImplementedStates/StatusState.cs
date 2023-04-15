using FinalFantasy;

public class StatusState : GameState
{
    public Character character;
    public int ID;
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        CameraManager.Instance.ToggleCamera(ECameraType.STATUS);
        CameraManager.Instance.SetFollowObject(character.transform);
        
        PlayerMenu.ActiveMenu.SwitchPanel(_manager.StatusPanel);
        _manager.StatusPanel.Init(character);
        
        EventManager.OnCharacterUnhovered?.Invoke(character);
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed && ID == 1)
        {
            _manager.movementState.IsMovementActive = false;
            _manager.ChangeState(_manager.movementState);
        }
        else if (InputSystem.WasBackPressed && ID == 0)
        {
            _manager.ChangeState(_manager.menuState);
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        CameraManager.Instance.ToggleCamera(ECameraType.TOPDOWN);

        if (ID == 0)
        {
            _manager.StatusPanel.SwitchPanel(_manager.PlayerMenu);
            _manager.PlayerMenu.Init();
        }
        else
        {
            _manager.StatusPanel.SwitchPanel(_manager.StatusMenu);
            _manager.StatusMenu.Init();
        }
        
    }
}
