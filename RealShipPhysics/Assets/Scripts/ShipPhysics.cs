using System.Collections.Generic;
using UnityEngine;

public class ShipPhysics : MonoBehaviour
{
    public Rigidbody ShipRigidbody;
    public GameObject MassCenter;
    public List<AirArea> AirAreas;

    void Awake()
    {

    }

    void Start()
    {
        ShipRigidbody.centerOfMass = MassCenter.transform.localPosition;
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
