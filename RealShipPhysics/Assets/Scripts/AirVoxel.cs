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

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, Vector3.up, out var hitInfo, Mathf.Infinity, WaterLayer.value))
        {
            Depth = hitInfo.distance;
            BuoyancyForce = WaterDensity * Mathf.Abs(Physics.gravity.y) * Volume;

            if (Depth < VoxelLength / 2)
            {
                var ratio = Depth / VoxelLength + 0.5f;
                BuoyancyForce = ratio * BuoyancyForce;
            }
        }
        else if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity, WaterLayer.value))
        {
            if (hitInfo.distance > VoxelLength / 2)
            {
                Depth = 0;
                BuoyancyForce = 0;
            }
            else
            {
                Depth = hitInfo.distance;

                var ratio = ((VoxelLength / 2 - Depth) / VoxelLength);
                BuoyancyForce = ratio * WaterDensity * Mathf.Abs(Physics.gravity.y) * Volume;
            }
        }

        Pressure = WaterDensity * Mathf.Abs(Physics.gravity.y) * Depth + AtmosphericPressure;
    }
}
