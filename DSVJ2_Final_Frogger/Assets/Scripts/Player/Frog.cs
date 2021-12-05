using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Frog : MonoBehaviour
{
    private Vector3 initialFrogPosition;
    private int remainingLives;

    public UnityAction onFrogDeath;
    public UnityAction onFrogLose;
    public UnityAction onFrogSmashed;
    [SerializeField] LayerMask avoidCollision;
    [SerializeField] Animator myAnimator;
    BoxCollider colliderFrog;

    void Start()
    {
        initialFrogPosition = transform.position;
        colliderFrog = gameObject.GetComponent<BoxCollider>();
    }

    public void SetRemainingLives(int lives)
    {
        remainingLives = lives;
    }

    public void ResetFrogPosition()
    {
        if(remainingLives > 0)
        {
            transform.position = initialFrogPosition;
            colliderFrog.enabled = true;
            onFrogDeath?.Invoke();
        }
        else
        {
            onFrogLose?.Invoke();
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!MyUtilities.Contains(avoidCollision, collision.gameObject.layer))
        {
            myAnimator.SetTrigger("Die");
            colliderFrog.enabled = false;
            onFrogSmashed?.Invoke();
        }
    }
}
