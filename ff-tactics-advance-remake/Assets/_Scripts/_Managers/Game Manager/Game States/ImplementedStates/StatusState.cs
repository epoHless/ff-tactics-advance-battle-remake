using FinalFantasy;
using UnityEngine;

public class StatusState : GameState
{
    public Character character;
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);
        
        CameraManager.Instance.ToggleCamera(ECameraType.STATUS);
        CameraManager.Instance.ActiveCamera.LookAt = character.transform;
        CameraManager.Instance.ActiveCamera.Follow = character.transform;
        
        PlayerMenu.ActiveMenu.SwitchPanel(_manager.StatusPanel);
        _manager.StatusPanel.Init(character);
        
        EventManager.OnCharacterUnhovered?.Invoke(character);
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed)
        {
            _manager.ChangeState(_manager.menuState);
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        PlayerMenu.ActiveMenu.SwitchPanel(_manager.StatusPanel.PreviousPanel);
        PlayerMenu.ActiveMenu.Init();
    }
}
