using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    CannonController mCannonController;
    LineRenderer mLineRenderer;

    [SerializeField] int m_NumPoints = 50;
    [SerializeField] float m_DistanceBwtweenPoints = 0.1f;

    [SerializeField] LayerMask m_CollidableLayers;

    [SerializeField] private GameObject m_TargetRing;

    private void Awake()
    {
        mCannonController = GetComponent<CannonController>();
        mLineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        mLineRenderer.positionCount = (int)m_NumPoints;
        List<Vector3> points = new List<Vector3>();
        Vector3 startingPosition = mCannonController.pShotPoint.position;
        Vector3 startingVelocity = mCannonController.pShotPoint.forward * mCannonController.pBlastPower;
        for(float t = 0; t < m_NumPoints; t += m_DistanceBwtweenPoints)
        {
            Vector3 newPoint = startingPosition + t * startingVelocity;
            newPoint.y = startingPosition.y + startingVelocity.y * t + Physics.gravity.y / 2f * t * t;
            points.Add(newPoint);
            if (Physics.OverlapSphere(newPoint, 2, m_CollidableLayers).Length > 0)
            {
                mLineRenderer.positionCount = points.Count;
                //SetTargetRing(mLineRenderer.GetPosition(points.Count - 1));
                break;
            }

        }

        mLineRenderer.SetPositions(points.ToArray());
    }

    private void SetTargetRing(Vector3 position)
    {
        m_TargetRing.transform.position = position;
    }

}
