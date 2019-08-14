using UnityEngine;

public static class PhysicsFormulas
{
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
    /// F_buo = p * g * V, where
    ///     p - liquid density,
    ///     g - gravity force,
    ///     V - volume
    /// </summary>
    public static float CalculateBuoyancyForce(float liquidDensity, float gravity, float volume)
    {
        return liquidDensity * gravity * volume;
    }
}
