using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash1_Script : MonoBehaviour
{
    [SerializeField] Animator splash2;
    
    public void ActivateSplash2()
    {
        splash2.SetBool("Splash2", true);
    }
}
