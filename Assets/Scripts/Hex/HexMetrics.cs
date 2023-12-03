using UnityEngine;

namespace Assets.Scripts.Hex
{
    public static class HexMetrics
    {
        public const float OuterRadius = 1;
        public const float InnerRadius = OuterRadius * 0.866025404f;

        public static Vector3[] Corners =
        {
            new Vector3(0, OuterRadius, 0),
            new Vector3(InnerRadius, OuterRadius * 0.5f, 0),
            new Vector3(InnerRadius, OuterRadius * -0.5f, 0),
            new Vector3(0, -OuterRadius, 0),
            new Vector3(-InnerRadius, -0.5f * OuterRadius, 0),
            new Vector3(-InnerRadius, 0.5f * OuterRadius, 0),
            new Vector3(0, OuterRadius, 0)
        };

        
            /*
            new Vector3(0, OuterRadius, 0),
            new Vector3(InnerRadius, OuterRadius * 0.5f, 0),
            new Vector3(InnerRadius, OuterRadius * -0.5f, 0),
            new Vector3(0, -OuterRadius, 0),
            new Vector3(-InnerRadius, -0.5f * OuterRadius, 0),
            new Vector3(-InnerRadius, 0.5f * OuterRadius, 0),
            new Vector3(0, OuterRadius, 0)
             */
    }
}