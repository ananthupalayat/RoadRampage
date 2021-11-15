using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField, Range(1f, 360f)]
    float m_RotationSpeed = 90f;

    [SerializeField, Range(-89f, 89f)]
    float m_MinVerticalAngle = -30f, m_MaxVerticalAngle = 60f;

    Vector2 mOrbitAngles = new Vector2(45f, 0f);

    [SerializeField] private GameObject m_MissilePrefab;

    public Transform pShotPoint = default;
    public float pBlastPower = 20f;

    private Animator mAnimator;

    [SerializeField] private ParticleSystem m_MuzzleFlash;

    [SerializeField] private bl_Joystick m_Joystick;

    // Start is called before the first frame update
    void Awake()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnMissile();
        }
    }

    void OnValidate()
    {
        if (m_MaxVerticalAngle < m_MinVerticalAngle)
        {
            m_MaxVerticalAngle = m_MinVerticalAngle;
        }
    }

    void LateUpdate()
    {
        Vector2 input = new Vector2(m_Joystick.Vertical
            ,
            m_Joystick.Horizontal
        );
        const float e = 0.001f;
        ConstrainAngles();
        if (input.x < -e || input.x > e || input.y < -e || input.y > e)
        {
            mOrbitAngles += m_RotationSpeed * Time.unscaledDeltaTime * input;
        }
        Quaternion lookRotation = Quaternion.Euler(mOrbitAngles);
        Vector3 lookDirection = lookRotation * Vector3.forward;
        transform.rotation = lookRotation;
    }

    /// <summary>
    /// Constrains Cannons rotation
    /// </summary>
    void ConstrainAngles()
    {
        mOrbitAngles.x =
            Mathf.Clamp(mOrbitAngles.x, m_MinVerticalAngle, m_MaxVerticalAngle);

        if (mOrbitAngles.y < 0f)
        {
            mOrbitAngles.y += 360f;
        }
        else if (mOrbitAngles.y >= 360f)
        {
            mOrbitAngles.y -= 360f;
        }
    }

    /// <summary>
    /// Spawns Missile
    /// </summary>
    public void SpawnMissile()
    {
        m_MuzzleFlash.Play();
        mAnimator.SetTrigger("Fire");
        GameObject spawnedMissile = ObjectPool.pInstance.GetObject(m_MissilePrefab);
        spawnedMissile.transform.SetPositionAndRotation(pShotPoint.position, pShotPoint.rotation);
        spawnedMissile.GetComponent<Rigidbody>().velocity = pShotPoint.transform.forward * pBlastPower;
    }
}
