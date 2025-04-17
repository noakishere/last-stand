using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class PortalGun : MonoBehaviour
{
    private InputMaster controls;
    [SerializeField] private LayerMask rayCastMask;
    [SerializeField] private Camera playerCam;

    [SerializeField] private GameObject portalAPrefab;

    [SerializeField] private List<GameObject> portals;

    [SerializeField] private float maxRaycastDist;

    [SerializeField] private Image uiImg;

    private int portalIndex = 0;

    [SerializeField] private AudioClip instanceSFX;

    void Awake()
    {
        controls = new InputMaster();
    }

    private void Update()
    {
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRaycastDist, rayCastMask))
        {
            uiImg.color = Color.green;
        }
        else
        {
            uiImg.color = Color.red;
        }
    }

    private void Shoot(InputAction.CallbackContext action)
    {
        //Debug.Log(action);
        Ray ray = playerCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDist, rayCastMask))
        {
            float threshold = 0.9f;
            Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
            portals[portalIndex].transform.position = hit.point;
            portals[portalIndex].transform.rotation = rotation;

            if (Mathf.Abs(Vector3.Dot(hit.normal, Vector3.up)) > threshold)
            {
                // Surface is horizontal.
                // Use the player's forward direction projected onto the horizontal plane
                Vector3 forward = Vector3.ProjectOnPlane(playerCam.transform.forward, Vector3.up).normalized;
                if (forward == Vector3.zero)
                    forward = Vector3.forward; // fallback if projection fails

                // Create a rotation that makes the portal lie flat (facing upward) but oriented based on player's forward
                rotation = Quaternion.LookRotation(forward, Vector3.up);
            }
            //portals[portalIndex].transform.GetChild(1).transform.localRotation = Quaternion.FromToRotation(Vector3.down, hit.normal);
            float randomSFXPitch = Random.Range(1f, 3f);
            SoundManager.Instance.PlayEffectAudio(instanceSFX, randomSFXPitch);
            portals[portalIndex].transform.GetChild(2).transform.localRotation = rotation;

            if (!portals[portalIndex].activeSelf)
            {
                portals[portalIndex].SetActive(true);
            }

            if(portalIndex == 0)
            {
                portalIndex = 1;
            }
            else
            {
                portalIndex = 0;
            }

            //Instantiate(portalAPrefab, hit.point, rotation);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
            //Debug.Log("shot!");
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
