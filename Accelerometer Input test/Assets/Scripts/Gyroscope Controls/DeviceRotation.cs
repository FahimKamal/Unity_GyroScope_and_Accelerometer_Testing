using Unity.Mathematics;
using UnityEngine;

public static class DeviceRotation
{
    private static bool _gyroInitialized;

    private static bool HasGyroscope => SystemInfo.supportsGyroscope;

    public static Quaternion Get()
    {
        if (!_gyroInitialized)
        {
            InitGyro();
        }

        return HasGyroscope ? ReadyGyroscopeRotation() : quaternion.identity;
    }

    private static void InitGyro()
    {
        if (HasGyroscope)
        {
            Input.gyro.enabled = true;              // Enable the gyroscope
            Input.gyro.updateInterval = 0.0167f;    // Set the update interval to it's highest value (60 Hz)
        }

        _gyroInitialized = true;
    }

    private static Quaternion ReadyGyroscopeRotation()
    {
        return new Quaternion(0.5f, 0.5f, -0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
