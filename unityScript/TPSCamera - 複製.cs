using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera : MonoBehaviour
{
    public Transform m_LookPoint;
    public Transform m_FollowTarget;
    public float m_LookHeight = 1.0f;
    public float m_LookSmoothTime = 0.1f;
    public float m_FollowSmoothTime = 0.1f;
    public float m_FollowDistance = 5.0f;
    public float m_CameraSensitivity = 1.0f;
    public float m_FollowHeight = 0.0f;
    public LayerMask m_HitLayers;
    public float m_HitMoveDistance = 0.1f;
    private float m_HorizontalRotateDegree = 0.0f;
    private float m_VerticalRotateDegree = 0.0f;
    private Vector3 m_FollowPosition = Vector3.zero;
    private Vector3 m_currentVector = Vector3.zero;
    private Vector3 m_RefVel = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        m_currentVector = m_FollowTarget.forward;
    }

    // Update is called once per frame
    void Update()
    {
        float fMX = Input.GetAxis("Mouse X");
        float fMY = Input.GetAxis("Mouse Y");
        m_HorizontalRotateDegree = fMX* m_CameraSensitivity;

        m_VerticalRotateDegree += fMY;
        if(m_VerticalRotateDegree > 20.0f)
        {
            m_VerticalRotateDegree = 20.0f;
        } else if(m_VerticalRotateDegree < -45.0f)
        {
            m_VerticalRotateDegree = -45.0f;
        }
        Debug.Log(m_VerticalRotateDegree);
    }

    private void LateUpdate()
    {
        // calculate follow position
        Vector3 hvec = m_currentVector;
        hvec.y = 0.0f;
        Vector3 rotatedHVec = Quaternion.AngleAxis(m_HorizontalRotateDegree, Vector3.up) * hvec;
        rotatedHVec.Normalize();
        Vector3 axis = Vector3.Cross(Vector3.up, rotatedHVec);
        Vector3 finialVec = Quaternion.AngleAxis(-m_VerticalRotateDegree, axis) * rotatedHVec;

        // lerp look position
        Vector3 vHeadUpPos = m_FollowTarget.position + m_LookHeight * Vector3.up;
        // m_LookPoint.position = Vector3.Lerp(m_LookPoint.position, vHeadUpPos, m_LookSmoothTime);
        m_LookPoint.position = Vector3.SmoothDamp(m_LookPoint.position, vHeadUpPos, ref m_RefVel, m_LookSmoothTime);

        m_FollowPosition = m_LookPoint.position - finialVec * m_FollowDistance;

        // lerp to follow position
        transform.position = Vector3.Lerp(transform.position, m_FollowPosition, m_FollowSmoothTime);

        // reset horizontal rotate degree
        m_HorizontalRotateDegree = 0.0f;

        // first method.
        RaycastHit rh;
        Ray r = new Ray(m_LookPoint.position, -finialVec);
        //if(Physics.Raycast(r, out rh, m_FollowDistance, m_HitLayers))
        //{
        //    Vector3 t = rh.point + finialVec* m_HitMoveDistance;
        //    transform.position = t;
        //}

        // second method.
        if (Physics.SphereCast(r, 0.5f, out rh, m_FollowDistance, m_HitLayers))
        {
             Vector3 t = m_LookPoint.position - finialVec*(rh.distance - m_HitMoveDistance);
             transform.position = t;
        }

        // look at a point
        //this.transform.LookAt(m_LookPoint);
        Vector3 vLookVec = m_LookPoint.position - this.transform.position;
        this.transform.forward = vLookVec;


        m_currentVector = transform.forward;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_FollowPosition, 0.5f);
        Gizmos.DrawWireSphere(m_LookPoint.position, 0.5f);
    }
}
