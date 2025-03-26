using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    private InputMaster controls;

    // Start is called before the first frame update
    void Awake()
    {
        controls = new InputMaster();
        Debug.Log(controls);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Shoot(InputAction.CallbackContext action)
    {
        Debug.Log(action);
        Debug.Log("shot!");
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
