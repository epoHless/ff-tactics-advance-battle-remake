using GridSystem;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Fields

    [SerializeField] private FacingDirectionHolder facingDirectionHolder;

    private GameState currentState;
    
    public MenuState menuState;
    public MovementState movementState;
    public TargetSelectionState targetSelectionState;
    public FacingDirectionState facingDirectionState;
    public StatusState statusState;
    
    #endregion
    
    #region Properties

    [field: SerializeField] public TileSelector TileSelector { get; private set; }
    [field: SerializeField] public TurnManager TurnManager { get; private set; }
    [field: SerializeField] public PlayerMenu PlayerMenu { get; private set; }
    [field: SerializeField] public StatisticsPanel StatusPanel { get; private set; }
    [field: SerializeField] public StatisticsMenu StatusMenu { get; private set; }
    
    #endregion

    #region Unity Methods

    protected override void Awake()
    {
        menuState = new MenuState(PlayerMenu);
        movementState = new MovementState(TileSelector);
        facingDirectionState = new FacingDirectionState(facingDirectionHolder);
        targetSelectionState = new TargetSelectionState(TileSelector);
        statusState = new StatusState();
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

    #endregion

    #region Methods

    public void ChangeState(GameState _newState)
    {
        currentState.OnExit(this);
        currentState = _newState;
        currentState.OnEnter(this);
    }

    #endregion
}
