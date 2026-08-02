using System;
using System.Globalization;
using UnityEngine;

public static class Extensions
{
    public static Vector3Data AsVectorData(this Vector3 vector) =>
        new Vector3Data(vector.x, vector.y, vector.z);

    public static Vector3 AsUnityVector(this Vector3Data vector3Data) =>
        new Vector3(vector3Data.X, vector3Data.Y, vector3Data.Z);
}

public static class NumberFormatter
{
    private static readonly string[] Suffixes =
    {
        "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc"
    };

    public static string NumberAsString(float value)
    {
        if (value < 1000)
            return value.ToString("0", CultureInfo.InvariantCulture);

        int suffix = 0;

        while (value >= 1000f && suffix < Suffixes.Length - 1)
        {
            value /= 1000f;
            suffix++;
        }

        if (MathF.Round(value, 1) >= 1000f && suffix < Suffixes.Length - 1)
        {
            value /= 1000f;
            suffix++;
        }

        return value.ToString(value >= 100 ? "0" : "0.#", CultureInfo.InvariantCulture)
               + Suffixes[suffix];
    }
}