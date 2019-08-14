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
                ShipRigidbody.AddForceAtPosition(new Vector3(0, voxel.TotalForce, 0), voxel.transform.position);
            }
        }
    }
}
