using UnityEditor;
using UnityEngine;

public class AirVoxel : MonoBehaviour
{
    [Header("Parameters")]
    public float WaterDensity;
    public float AtmosphericPressure;
    public float VoxelLength;

    [Header("Other")]
    public LayerMask WaterLayer;

    [Header("Read-only parameters")]
    [ReadOnlyField] public float Depth;
    [ReadOnlyField] public float Pressure;
    [ReadOnlyField] public float Volume;
    [ReadOnlyField] public float BuoyancyForce;

    void Start()
    {
        Volume = Mathf.Pow(VoxelLength, 3);
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out var hitInfo, Mathf.Infinity, WaterLayer.value))
        {
            Depth = hitInfo.distance;
            BuoyancyForce = WaterDensity * Mathf.Abs(Physics.gravity.y) * Volume;
        }
        else
        {
            Depth = 0;
            BuoyancyForce = 0;
        }

        Pressure = WaterDensity * Mathf.Abs(Physics.gravity.y) * Depth + AtmosphericPressure;
    }
}
