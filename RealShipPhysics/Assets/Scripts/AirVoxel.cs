using UnityEditor;
using UnityEngine;

public class AirVoxel : MonoBehaviour
{
    [Header("Parameters")]
    public float WaterDensity;
    public float AtmPressure;

    [Range(0, 100)]
    public float FloodLevel;

    [Header("Other")]
    public LayerMask WaterLayer;

    [Header("Read-only parameters")]
    [ReadOnlyField] public float Depth;
    [ReadOnlyField] public float Pressure;
    [ReadOnlyField] public float Volume;
    [ReadOnlyField] public float MassWithWater;
    [ReadOnlyField] public float BuoyancyForce;
    [ReadOnlyField] public float GravityForce;
    [ReadOnlyField] public float TotalForce;

    public float VoxelLength { get; set; }

    void Start()
    {
        Volume = Mathf.Pow(VoxelLength, 3);
        MassWithWater = PhysicsFormulas.CalculateMass(Volume, WaterDensity);
    }

    void FixedUpdate()
    {
        // Check if voxel is under or above the liquid
        if (Physics.Raycast(transform.position, Vector3.up, out var hitInfo, Mathf.Infinity, WaterLayer.value))
        {
            var ratio = 1f;

            // If voxel is half submerged (some of its part is above the liquid, but not more than 50%) then calculate
            // how many buoyancy force should be decreased (part in air doesn't generate this force)
            if (hitInfo.distance < VoxelLength / 2)
            {
                //  Depth |     Voxel length    | Buoyancy ratio
                // ---------------------------------------------
                //   0.0  |         1.0         |     0.5
                //   0.1  |         1.0         |     0.6
                //   0.2  |         1.0         |     0.7
                //   0.3  |         1.0         |     0.8
                //   0.4  |         1.0         |     0.9
                //   0.5  |         1.0         |     1.0
                // ---------------------------------------------
                // ratio = depth / voxel length + 0.5f
                //
                ratio = hitInfo.distance / VoxelLength + 0.5f;
            }

            Depth = hitInfo.distance;
            BuoyancyForce = ratio * PhysicsFormulas.CalculateBuoyancyForce(WaterDensity, Mathf.Abs(Physics.gravity.y), Volume);
        }
        else if(Physics.Raycast(transform.position, Vector3.down, out hitInfo, Mathf.Infinity, WaterLayer.value))
        {
            // If voxel is half sticking out (some of its part is under the liquid, but not more than 50%) then calculate
            // how many buoyancy force should be decreased (part in air doesn't generate this force)
            if (hitInfo.distance < VoxelLength / 2)
            {
                //  Depth |     Voxel length    | Buoyancy ratio
                // ---------------------------------------------
                //   0.0  |         1.0         |     0.5
                //   0.1  |         1.0         |     0.4
                //   0.2  |         1.0         |     0.3
                //   0.3  |         1.0         |     0.2
                //   0.4  |         1.0         |     0.1
                //   0.5  |         1.0         |     0.0
                // ---------------------------------------------
                // ratio = 1 - depth / voxel length - 0.5f
                //
                var ratio = 1 - hitInfo.distance / VoxelLength - 0.5f;

                BuoyancyForce = ratio * PhysicsFormulas.CalculateBuoyancyForce(WaterDensity, Mathf.Abs(Physics.gravity.y), Volume);
            }
            else
            {
                BuoyancyForce = 0;
            }

            Depth = 0;
        }

        // When air voxel is flood by water, it has smaller buoyancy force (because of less air) and generates
        // additional gravity force (because of water inside)
        if (FloodLevel > 0)
        {
            var ratio = FloodLevel / 100;

            BuoyancyForce *= 1 - ratio;
            GravityForce = ratio * PhysicsFormulas.CalculateGravityForce(MassWithWater, Mathf.Abs(Physics.gravity.y));
        }

        Pressure = PhysicsFormulas.CalculateAbsoluteHydrostaticPressure(WaterDensity, Mathf.Abs(Physics.gravity.y), Depth, AtmPressure);
        TotalForce = BuoyancyForce - GravityForce;
    }
}
