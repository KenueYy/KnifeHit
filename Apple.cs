using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private ApplePoint[] applePoints;
    private ApplePoint myPoint;
    [SerializeField] private ParticleSystem particle;
    private void Awake()
    {
        applePoints = FindObjectsOfType<ApplePoint>();
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        if (myPoint != null) myPoint.isExist = false;
        particle.Play();
    }
    public void Stand()
    {
        foreach (ApplePoint applePoint in applePoints)
        {
            if (!applePoint.isExist)
            {
                transform.position = applePoint.transform.position;
                transform.parent = applePoint.transform;
                transform.rotation = new Quaternion(0,0,0,0);
                applePoint.isExist = true;
                myPoint = applePoint;
                break;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        myPoint.isExist = false;
        gameObject.SetActive(false);
    }

}
