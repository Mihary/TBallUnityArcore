﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class DetectedPlaneGenerator : MonoBehaviour
{



    public static DetectedPlaneGenerator instance = null; 
    public List<GameObject> PLANES = new List<GameObject>();


    /// <summary>
    /// A prefab for tracking and visualizing detected planes.
    /// </summary>
    public GameObject DetectedPlanePrefab;

    /// <summary>
    /// A list to hold new planes ARCore began tracking in the current frame. This object is
    /// used across the application to avoid per-frame allocations.
    /// </summary>
    private List<DetectedPlane> m_NewPlanes = new List<DetectedPlane>();

    /// <summary>
    /// The Unity Update method.
    /// </summary>
    // Start is called before the first frame update


    /// <summary>
    /// The Unity Update method.
    /// </summary>
    /// 
    void Awake()//RK
    {

        if (instance == null)


            instance = this;


        else if (instance != this)

            Destroy(gameObject);


    }


    public void Update()
    {
        // Check that motion tracking is tracking.
        if (Session.Status != SessionStatus.Tracking)
        {
            return;
        }

        // Iterate over planes found in this frame and instantiate corresponding GameObjects to visualize them.
        Session.GetTrackables<DetectedPlane>(m_NewPlanes, TrackableQueryFilter.New);
        for (int i = 0; i < m_NewPlanes.Count; i++)
        {
            // Instantiate a plane visualization prefab and set it to track the new plane. The transform is set to
            // the origin with an identity rotation since the mesh for our prefab is updated in Unity World
            // coordinates.
            GameObject planeObject = Instantiate(DetectedPlanePrefab, Vector3.zero, Quaternion.identity, transform);
            PLANES.Add(planeObject); //RK
            planeObject.GetComponent<GoogleARCore.Examples.Common.DetectedPlaneVisualizer>().Initialize(m_NewPlanes[i]);

        }
    }
}

