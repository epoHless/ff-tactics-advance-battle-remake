using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FacingDirectionHolder : MonoBehaviour
{
    [SerializeField] private List<FacingDirectionButton> buttons;
    
    public void Init(Vector3 _position)
    {
        transform.position = _position;
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
}
