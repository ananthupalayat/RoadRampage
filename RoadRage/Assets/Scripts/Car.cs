using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Car : MonoBehaviour
{
    Rigidbody mRigidBody;
    [SerializeField] private float m_Carspeed = 20;

    public enum CarType {REDCAR,WHITECAR};
    public CarType pThisCarType;

    public delegate void CarHit(CarType carType,Vector3 position);
    public static CarHit OnCarHit;

    public delegate void CarEscaped();
    public static CarEscaped OnCarEscaped;

    // Start is called before the first frame update
    void Awake()
    {
        mRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mRigidBody.velocity = Vector3.forward * m_Carspeed; 
    }

    /// <summary>
    /// Detects Collision With a Missile
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Missile"))
        {
            CameraShaker.Instance.ShakeCamera(5, 0.5f);
            OnCarHit?.Invoke(pThisCarType, transform.position);
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Detects Exit Line
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            if (pThisCarType == CarType.WHITECAR)
            {
                OnCarEscaped?.Invoke();
            }
            this.gameObject.SetActive(false);
        }
    }
}
