using UnityEngine;

public static class PhysicsFormulas
{
    /// <summary>
    /// m = p * V, where
    ///     p - density,
    ///     h - volume
    /// </summary>
    public static float CalculateMass(float density, float volume)
    {
        return density * volume;
    }

    /// <summary>
    /// P_hyd_abs = p * g * h + P_atm, where
    ///     p - liquid density,
    ///     g - gravity force,
    ///     h - depth,
    ///     P_atm - atmospheric pressure
    /// </summary>
    public static float CalculateAbsoluteHydrostaticPressure(float liquidDensity, float gravity, float depth, float atmosphericPressure)
    {
        return CalculateAbsoluteHydrostaticPressure(liquidDensity, gravity, depth) + atmosphericPressure;
    }

    /// <summary>
    /// P_hyd_abs = p * g * h, where
    ///     p - liquid density,
    ///     g - gravity force,
    ///     h - depth
    /// </summary>
    public static float CalculateAbsoluteHydrostaticPressure(float liquidDensity, float gravity, float depth)
    {
        return liquidDensity * Mathf.Abs(gravity) * depth;
    }

    /// <summary>
    /// F_gra = m * g, where
    ///     m - mass of the object,
    ///     g - gravity force
    /// </summary>
    public static float CalculateGravityForce(float mass, float gravity)
    {
        return mass * gravity;
    }

    /// <summary>
    /// F_buo = p * g * V, where
    ///     p - liquid density,
    ///     g - gravity force,
    ///     V - volume
    /// </summary>
    public static float CalculateBuoyancyForce(float liquidDensity, float gravity, float volume)
    {
        return liquidDensity * gravity * volume;
    }

    /// <summary>
    /// F_drag = C * g * v^2 * A / 2, where
    ///     C - drag coefficient,
    ///     p - fluid density,
    ///     v - relative speed,
    ///     A - area projected to the flow
    /// </summary>
    public static float CalculateDragForce(float dragCoefficient, float fluidDensity, float relativeSpeed, float area)
    {
        return dragCoefficient * fluidDensity * Mathf.Pow(relativeSpeed, 2) * area / 2;
    }
}
