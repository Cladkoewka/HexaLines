﻿using UnityEngine;

namespace Assets.Scripts.Hex
{
    public static class HexMetrics
    {
        public const float OuterRadius = 1;
        public const float InnerRadius = OuterRadius * 0.866025404f;
        public const float Spacing = 0.2f;

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
        
        public static int[] CellsCount = new []{5,6,7,8,9,8,7,6,5};
    }
}