using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VcamScript : MonoBehaviour
{

    private Transform m_follow;
    public levelGenerator lvGen;
    private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFollow()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
    
        m_follow = GameObject.FindWithTag("Player").GetComponent<Transform>();
        vcam .LookAt = m_follow;
        vcam.Follow = m_follow;
        Debug.Log("Cam Setada");
        
    }
}
