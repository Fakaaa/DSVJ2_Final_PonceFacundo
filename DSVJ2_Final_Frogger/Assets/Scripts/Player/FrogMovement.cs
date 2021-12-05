using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] int jumpUnits;
    [SerializeField] float rotationSpeed;

    Vector3 actualDirection;
    bool rotateEnded = true;
    bool inteedMove = false;
    bool isGamePaused = false;
    bool isFrogSmahed = false;

    void Start()
    {
        actualDirection = transform.forward;
    }

    void Update()
    {
        if (isGamePaused || isFrogSmahed)
            return;

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

    public void AvoidPlayerMoveOnDie()
    {
        isFrogSmahed = !isFrogSmahed;
    }

    public void AvoidPlayerJumpOnPause()
    {
        isGamePaused = !isGamePaused;
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
