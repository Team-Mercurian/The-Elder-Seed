﻿using System.Collections.Generic;
using UnityEngine;

public class AimHead:MonoBehaviour
{
    
    [SerializeField] float m_smoothness = 0.1f;

    //List of possible targets
    List<Transform> m_targets = new List<Transform>();

    //default aim position
    Vector3 m_defaultTargetPos = new Vector3(0, 1.45f, 3f);

    Camera m_cam;

    //head offset for looking directly where camera is pointing
    Vector3 m_headOffset = new Vector3(0, 5f, 0);

    Vector3 m_velocity;

    [SerializeField] Transform m_aimTarget = null;
    [SerializeField] Transform m_player = null;

    // Start is called before the first frame update
    void Start()
    {
        m_cam = Camera.main;
        m_aimTarget.position = m_defaultTargetPos;
    }

    void LateUpdate()
    {
        //Player looks at whatever camera is pointing at (doesn't work)
        //m_aimTarget.position = (m_cam.transform.position - (transform.position + m_headOffset)) * -1;

        Vector3 m_lookPosition;
        Vector3 m_defaultPos = m_player.position + Vector3.Scale(m_player.forward, new Vector3(m_defaultTargetPos.z, 1, m_defaultTargetPos.z)) + (Vector3.up * m_defaultTargetPos.y);

        //if list has targets
        if (m_targets.Count > 0 && m_targets[0] != null) m_lookPosition = m_targets[0].transform.position;
        else m_lookPosition = m_defaultPos;
        
        float m_angle = (Mathf.Atan2(m_lookPosition.z - m_player.position.z, m_lookPosition.x - m_player.position.x) * Mathf.Rad2Deg) + m_player.eulerAngles.y;
        if (m_angle > 180) m_angle -= 360;
        if (m_angle < -180) m_angle += 360;

        bool m_canLookTarget = m_angle > 0 && m_angle < 180;

        m_lookPosition = m_canLookTarget ? m_lookPosition : m_defaultPos;

        m_aimTarget.position = Vector3.SmoothDamp(m_aimTarget.position, m_lookPosition, ref m_velocity, m_smoothness);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            m_targets.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            m_targets.Remove(other.transform);
            //m_aimTarget.localPosition = m_defaultTargetPos;
        }
    }
}