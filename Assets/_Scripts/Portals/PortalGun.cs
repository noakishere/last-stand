using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalGun : MonoBehaviour
{
    private InputMaster controls;
    [SerializeField] private LayerMask rayCastMask;
    [SerializeField] private Camera playerCam;

    [SerializeField] private GameObject portalAPrefab;

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
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            Instantiate(portalAPrefab, hit.point, rotation);
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
