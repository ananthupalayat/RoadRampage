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

    // Start is called before the first frame update
    void Awake()
    {
        
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
        Vector2 input = new Vector2(
            Input.GetAxis("Vertical"),
            Input.GetAxis("Horizontal")
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

    private void SpawnMissile()
    {
        GameObject spawnedMissile = ObjectPool.pInstance.GetObject(m_MissilePrefab);
        spawnedMissile.transform.SetPositionAndRotation(pShotPoint.position, pShotPoint.rotation);
        spawnedMissile.GetComponent<Rigidbody>().velocity = pShotPoint.transform.forward * pBlastPower;
    }
}
