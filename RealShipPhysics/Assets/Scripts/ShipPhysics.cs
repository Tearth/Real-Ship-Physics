using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipPhysics : MonoBehaviour
{
    public Rigidbody Rigidbody;
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
            foreach (var voxel in area._airGrid)
            {
                Rigidbody.AddForceAtPosition(new Vector3(0, voxel.BuoyancyForce, 0), voxel.transform.position, ForceMode.Force);
            }
        }
    }
}
