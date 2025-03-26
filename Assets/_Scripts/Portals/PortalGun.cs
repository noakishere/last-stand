using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    private InputMaster controls;
    [SerializeField] private LayerMask rayCastMask;
    [SerializeField] private Camera playerCam;

    void Awake()
    {
        controls = new InputMaster();
    }

    private void Shoot(InputAction.CallbackContext action)
    {
        Debug.Log(action);
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, rayCastMask))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            Debug.Log("shot!");
        }
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Shoot.performed += Shoot;
    }

    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Shoot.performed -= Shoot;
    }
}
