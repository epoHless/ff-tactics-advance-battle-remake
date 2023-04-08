using FinalFantasy;

public class MenuState : GameState
{
    private PlayerMenu menu;
    
    public MenuState(PlayerMenu _menu)
    {
        menu = _menu;
    }
    
    public override void OnEnter(GameManager _manager)
    {
        base.OnEnter(_manager);

        InputSystem.DisableInput();
        
        menu.gameObject.SetActive(true);

        menu.Init();
        menu.Toggle(true);
    }

    public override void OnUpdate(GameManager _manager)
    {
        base.OnUpdate(_manager);

        if (InputSystem.WasBackPressed)
        {
            //todo go to map exploration
        }
    }

    public override void OnExit(GameManager _manager)
    {
        base.OnExit(_manager);
        
        menu.Toggle(false, -1).setOnComplete(() =>
        {
            menu.gameObject.SetActive(false);
            InputSystem.EnableInput();
        });
    }
}
