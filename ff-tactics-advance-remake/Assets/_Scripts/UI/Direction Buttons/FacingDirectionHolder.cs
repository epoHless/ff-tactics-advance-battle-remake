using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FacingDirectionHolder : MonoBehaviour
{
    [SerializeField] private List<FacingDirectionButton> buttons;

    private Camera camera;
    
    public void Init(Vector3 _position)
    {
        transform.position = _position;
        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
    }

    private void Start()
    {
        camera = Camera.main;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        var direction = camera.transform.position - gameObject.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
