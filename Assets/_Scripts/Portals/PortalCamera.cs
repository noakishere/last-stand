using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;
    [SerializeField] private Transform portal;
    [SerializeField] private Transform otherPortal;

    //void LateUpdate()
    //{
    //    //Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
    //    //transform.position = portal.position + playerOffsetFromPortal;

    //    float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);

    //    Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
    //    Vector3 newCameraDirection = portalRotationalDifference * playerCamera.up;

    //    transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.forward);
    //}

    //void LateUpdate()
    //{
    //    // Get the normal vector of the portal surface
    //    Vector3 portalNormal = portal.forward;

    //    // Get the player's forward direction (incident ray)
    //    Vector3 playerDirection = playerCamera.forward;

    //    // Calculate the incident angle (angle between player direction and portal normal)
    //    float incidentAngle = Vector3.Angle(playerDirection, portalNormal);

    //    // Calculate reflection direction using the reflection formula:
    //    // R = I - 2(I·N)N where I is incident direction, N is normal
    //    float dotProduct = Vector3.Dot(playerDirection, portalNormal);
    //    Vector3 reflectionDirection = playerDirection - 2 * dotProduct * portalNormal;

    //    // Calculate the angle difference between portals for teleportation effect
    //    float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
    //    Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);

    //    // Apply the portal rotation difference to the reflection direction
    //    Vector3 newCameraDirection = portalRotationalDifference * reflectionDirection;

    //    // Set the camera rotation to look in the calculated direction
    //    transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);

    //    // You can also store or display the incident angle for debugging
    //    Debug.Log("Angle of Incidence: " + incidentAngle);
    //}

    void LateUpdate()
    {
        Vector3 portalNormal = portal.forward;
        Vector3 playerDirection = playerCamera.forward;

        // Calculate reflection using the portal normal.
        float dotProduct = Vector3.Dot(playerDirection, portalNormal);
        Vector3 reflectionDirection = playerDirection - 2 * dotProduct * portalNormal;

        // Use the portal's up for angular difference axis.
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, portal.up);

        Vector3 newCameraDirection = portalRotationalDifference * reflectionDirection;

        // Use portal's up vector for proper alignment.
        transform.rotation = Quaternion.LookRotation(newCameraDirection, portal.up);

        //Debug.Log("Angle of Incidence: " + Vector3.Angle(playerDirection, portalNormal));
    }

}
