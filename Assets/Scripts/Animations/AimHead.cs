using System.Collections.Generic;
using UnityEngine;

public class AimHead:MonoBehaviour
{
    //List of possible targets
    List<Transform> m_targets = new List<Transform>();

    //default aim position
    Vector3 m_defaultTargetPos = new Vector3(0, 1.45f, 3f);

    Camera m_cam;

    //head offset for looking directly where camera is pointing
    Vector3 m_headOffset = new Vector3(0, 5f, 0);

    [SerializeField] Transform m_aimTarget = null;

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

        //if list has targets
        if (m_targets.Count > 0)
        {
            m_aimTarget.position = m_targets[0].transform.position;
        }
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
            m_aimTarget.localPosition = m_defaultTargetPos;
        }
    }
}
