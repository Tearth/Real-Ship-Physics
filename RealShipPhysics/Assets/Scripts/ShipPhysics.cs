using System.Collections.Generic;
using UnityEngine;

public class ShipPhysics : MonoBehaviour
{
    public Rigidbody ShipRigidbody;
    public List<AirArea> AirAreas;

    void Awake()
    {

    }

    void Start()
    {

    }

    void FixedUpdate()
    {
        foreach (var area in AirAreas)
        {
            foreach (var voxel in area.AirGrid)
            {
                ShipRigidbody.AddForceAtPosition(voxel.TotalForce, voxel.transform.position);
            }
        }
    }
}
