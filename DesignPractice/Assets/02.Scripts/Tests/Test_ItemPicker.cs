using DP.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Test_ItemPicker : MonoBehaviour
{
    Camera _camera;
    [SerializeField] InputActionReference _pickAction;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _pickAction.action.started += OnPickStarted;
        _pickAction.action.performed += OnPickPerformed;
        _pickAction.action.canceled += OnPickCanceled;
        _pickAction.action.Enable();
    }
    private void OnDisable()
    {
        _pickAction.action.started -= OnPickStarted;
        _pickAction.action.performed -= OnPickPerformed;
        _pickAction.action.canceled -= OnPickCanceled;
        _pickAction.action.Disable();
    }

    private void OnPickStarted(InputAction.CallbackContext context)
    {
        Ray ray = _camera.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out ItemController itemController))
            {
                itemController.PickUp();
            }
        }
    }
    private void OnPickPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Pick performed");
    }
    private void OnPickCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Pick canceled");
    }
}