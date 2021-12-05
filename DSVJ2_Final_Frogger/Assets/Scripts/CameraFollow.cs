using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region EXPOSED_FIELDS

    [Header("Needs")]
    [SerializeField] Transform target;
    [Header("MainSettings")]
    [Space(10)]
    [SerializeField] float speedFollow;
    [SerializeField] float zoomUp;
    [SerializeField] float zoomDistance;
    [SerializeField] bool cameraFocusTarget;

    #endregion

    #region PRIVATE_FIELDS

    private Vector3 zoom;
    private Vector3 posToMoveTowards;
    
    #endregion

    void LateUpdate()
    {
        if(cameraFocusTarget)
        {
            transform.LookAt(target, target.up);
        }

        FollowTarget();
    }

    public void FollowTarget()
    {
        if (target == null)
            return;

        Vector3 cameraPosition = transform.position;

        zoom = new Vector3(0, zoomUp, -zoomDistance);

        posToMoveTowards = target.position + zoom;

        transform.position = Vector3.Lerp(cameraPosition, posToMoveTowards, 
            Vector3.Distance(cameraPosition, posToMoveTowards) * Time.deltaTime * speedFollow);
    }
}
