using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DirectionalLightController : MonoBehaviour
{
    public Vector3 m_FromEuler     = Vector3.zero;
    public Vector3 m_ToEuler       = Vector3.zero;
    public float   m_RotationSpeed = 0.1f;

    float          m_Time          = 0.0f;
    Quaternion     m_QuatFrom;
    Quaternion     m_QuatTo;

    public float   m_LightIntensitySpeed = 0.5f;
    Light          m_Light;
    float          m_LightIntensity;

    void Start()
    {
        m_QuatFrom = Quaternion.Euler( m_FromEuler );
        m_QuatTo   = Quaternion.Euler( m_ToEuler );

        m_Light    = GetComponent<Light>();
        Assert.IsNotNull( m_Light );

        m_LightIntensity = m_Light.intensity;
    }

    void Update()
    {
        m_Time += Time.deltaTime;
        this.transform.rotation = 
            Quaternion.Lerp( m_QuatFrom, m_QuatTo,
                             m_Time * m_RotationSpeed );

        float angle = Quaternion.Angle( this.transform.rotation, m_QuatTo );
        if ( angle == 0.0f )
        {
            Quaternion tmp = m_QuatFrom;
            m_QuatFrom     = m_QuatTo;
            m_QuatTo       = tmp;

            m_Time         = 0.0f;
        }

        float deg = 180.0f * Time.realtimeSinceStartup * m_LightIntensitySpeed;
        float rad = Mathf.Deg2Rad * deg;
        float cos = Mathf.Cos( rad );

        float light_offset    = 0.5f;
        float light_intensity = ( m_LightIntensity - light_offset ) * ( 0.5f * cos + 0.5f ) + light_offset;
        m_Light.intensity     = light_intensity;
    }
}
