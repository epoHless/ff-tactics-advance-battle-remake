using UnityEngine;
using UnityEngine.EventSystems;

public class FacingDirectionButton : MonoBehaviour, ISelectHandler
{
    [field: SerializeField] public Vector2 Direction { get; private set; }
    
    public void OnSelect(BaseEventData eventData)
    {
        EventManager.OnDirectionSelect?.Invoke(new Vector3(Direction.x, 0, Direction.y));
    }
}