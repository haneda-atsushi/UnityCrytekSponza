using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalLightController : MonoBehaviour
{
    public Vector3 m_FromEuler  = Vector3.zero;
    public Vector3 m_ToEuler    = Vector3.zero;

    public float   m_Speed      = 0.1f;
    public bool    m_EnableLoop = true;

    float          m_Time       = 0.0f;
    Quaternion     m_QuatFrom;
    Quaternion     m_QuatTo;

    void Start()
    {
        m_QuatFrom = Quaternion.Euler( m_FromEuler );
        m_QuatTo   = Quaternion.Euler( m_ToEuler );
    }

    void Update()
    {
        m_Time += Time.deltaTime;
        this.transform.rotation = 
            Quaternion.Lerp( m_QuatFrom, m_QuatTo,
                             m_Time * m_Speed );

        float angle = Quaternion.Angle( this.transform.rotation, m_QuatTo );
        if ( m_EnableLoop &&
            ( angle == 0.0f ) )
        {
            Quaternion tmp = m_QuatFrom;
            m_QuatFrom     = m_QuatTo;
            m_QuatTo       = tmp;

            m_Time         = 0.0f;
        }
    }
}
