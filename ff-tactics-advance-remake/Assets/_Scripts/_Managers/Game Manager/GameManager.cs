using GridSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private TileSelector tileSelector;
    [SerializeField] private FacingDirectionHolder facingDirectionHolder;
    [SerializeField] private PlayerMenu playerMenu;
    
    private GameState currentState;

    public MenuState menuState;
    public MovementState movementState;
    public FacingDirectionState facingDirectionState;

    protected override void Awake()
    {
        menuState = new MenuState(playerMenu);
        movementState = new MovementState(tileSelector);
        facingDirectionState = new FacingDirectionState(facingDirectionHolder);
    }

    private void Start()
    {
        currentState = menuState;
        currentState.OnEnter(this);
    }

    private void Update()
    {
        currentState.OnUpdate(this);
    }

    public void ChangeState(GameState _newState)
    {
        currentState.OnExit(this);
        currentState = _newState;
        currentState.OnEnter(this);
    }
}
