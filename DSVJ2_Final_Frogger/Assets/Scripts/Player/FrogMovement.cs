using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] public int jumpUnits;
    [SerializeField] public float rotationSpeed;

    Vector3 actualDirection;
    bool rotateEnded = true;
    bool inteedMove = false;

    void Start()
    {
        actualDirection = transform.forward;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W) && rotateEnded)
        {
            inteedMove = true;
            actualDirection = Vector3.forward;
        }
        else if(Input.GetKeyDown(KeyCode.S) && rotateEnded)
        {
            inteedMove = true;
            actualDirection = Vector3.back;
        }
        else if (Input.GetKeyDown(KeyCode.A) && rotateEnded)
        {
            inteedMove = true;
            actualDirection = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.D) && rotateEnded)
        {
            inteedMove = true;
            actualDirection = Vector3.right;
        }

        MoveDirection(actualDirection);
    }

    public void MoveDirection(Vector3 direction)
    {
        if(inteedMove)
        {
            if(RotateFrogInDirection())
            {
                transform.position += direction * jumpUnits;
                actualDirection = direction;

                inteedMove = false;
            }
            else
            {
                Quaternion slerpedRotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(actualDirection, transform.up), rotationSpeed * Time.deltaTime);

                transform.rotation = Quaternion.Normalize(slerpedRotation);
            }
        }
    }

    public bool RotateFrogInDirection()
    {
        Quaternion targetRotation = Quaternion.LookRotation(actualDirection, transform.up);

        Debug.Log(targetRotation);

        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            rotateEnded = true;
            return true;
        }
        else
        {
            rotateEnded = false;
            return false;
        }
    }
}
