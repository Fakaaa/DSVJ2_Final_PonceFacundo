using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBehaviour : MonoBehaviour
{
    [SerializeField] float speed;
    float originalSpeed;
    [SerializeField] Transform targetDestiny;
    [SerializeField] Transform initialPos;
    [SerializeField] LayerMask layerToCheck;
    [SerializeField] LayerMask myLayer;

    public bool travelDone;

    private void Start()
    {
        originalSpeed = speed;
    }

    public void SetTargetDestiny(Transform target)
    {
        targetDestiny = target;
    }
    
    public void SetInitialPosition(Transform initialTransform)
    {
        initialPos = initialTransform;

        if(originalSpeed != 0)
            speed = originalSpeed;
    }
    
    public void SetSpeedCar(float newSpeed)
    {
        speed = newSpeed;
    }

    public void MakeRide()
    {
        if (gameObject.activeSelf)
        {
            AvoidCrash();

            if (transform.position != targetDestiny.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetDestiny.position, speed * Time.deltaTime);
            }
            else
            {
                travelDone = true;
            }
        }
    }

    public void AvoidCrash()
    {
        RaycastHit hit;

        Ray directionRay = new Ray(transform.position, -transform.right * 8f);

        Debug.DrawRay(directionRay.origin, directionRay.direction * 8f, Color.blue);

        if (Physics.Raycast(directionRay.origin, directionRay.direction, out hit, 8f))
        {
            CarBehaviour theCarInFront = hit.collider.gameObject.GetComponent<CarBehaviour>();
            if(theCarInFront != null)
            {
                if(MyUtilities.Contains(layerToCheck, hit.collider.gameObject.layer) ||
                   MyUtilities.Contains(myLayer, hit.collider.gameObject.layer))
                    SetSpeedCar(theCarInFront.speed);
            }
        }
    }

    public void ResetCar()
    {
        if(travelDone)
        {
            transform.position = initialPos.position;
            travelDone = false;
        }
    }
}
