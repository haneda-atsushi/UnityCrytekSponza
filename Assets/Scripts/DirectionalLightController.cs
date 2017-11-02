using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class DirectionalLightController : MonoBehaviour
{
    public bool           m_EnableRotation = true;
    public Vector3        m_FromEuler     = Vector3.zero;
    public Vector3        m_ToEuler       = Vector3.zero;
    public float          m_RotationSpeed = 0.1f;
    float                 m_RotationTime  = 0.0f;
    Quaternion            m_QuatFrom;
    Quaternion            m_QuatTo;

    Light                 m_Light;
    float                 m_LightIntensity;
    public AnimationCurve m_LightIntensityCurve  = AnimationCurve.Linear( 0.0f, 1.0f, 1.0f, 1.0f );
    [Range(0.0f,120.0f)]
    public float          m_LightIntensityPeriod = 10.0f;

    float                 m_LightIntensityTime   = 0.0f;

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
        if ( m_EnableRotation )
        {
            this.transform.rotation = 
                Quaternion.Lerp( m_QuatFrom, m_QuatTo,
                                 m_RotationTime * m_RotationSpeed );
            float angle = Quaternion.Angle( this.transform.rotation, m_QuatTo );
            if ( angle == 0.0f )
            {
                Quaternion tmp = m_QuatFrom;
                m_QuatFrom     = m_QuatTo;
                m_QuatTo       = tmp;

                m_RotationTime = 0.0f;
            }
            else
            {
                m_RotationTime += Time.deltaTime;
            }
        }

        float light_intensity_time  = ( m_LightIntensityTime / m_LightIntensityPeriod );
        m_LightIntensityTime       += Time.deltaTime;
        if ( m_LightIntensityTime > m_LightIntensityPeriod )
        {
            m_LightIntensityTime = 0.0f;
        }

        float light_intensity = m_LightIntensity * m_LightIntensityCurve.Evaluate( light_intensity_time );
        m_Light.intensity     = light_intensity;
    }
}
