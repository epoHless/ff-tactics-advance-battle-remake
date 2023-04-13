public class ActionButton : MenuButton
{
    public override bool CanBeEnabled()
    {
        return !TurnManager.Instance.currentTurn.HasUsedAbilities;
    }
}
