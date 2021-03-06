﻿using STROOP.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STROOP.Structs.Configurations
{
    public static class SpecialConfig
    {
        // Point vars

        // - Custom

        public static double CustomX = 0;
        public static double CustomY = 0;
        public static double CustomZ = 0;
        public static double CustomAngle = 0;

        // - Self pos

        public static PositionAngle SelfPosPA = PositionAngle.Mario;
        public static PositionAngle SelfAnglePA = PositionAngle.Mario;
        public static PositionAngle SelfPA
        {
            get => PositionAngle.Hybrid(SelfPosPA, SelfAnglePA);
        }

        public static double SelfX
        {
            get => SelfPA.X;
        }

        public static double SelfY
        {
            get => SelfPA.Y;
        }

        public static double SelfZ
        {
            get => SelfPA.Z;
        }

        public static double SelfAngle
        {
            get => SelfPA.Angle;
        }

        // - Point pos

        public static PositionAngle PointPosPA = PositionAngle.Custom;
        public static PositionAngle PointAnglePA = PositionAngle.Custom;
        public static PositionAngle PointPA
        {
            get => PositionAngle.Hybrid(PointPosPA, PointAnglePA);
        }

        public static double PointX
        {
            get => PointPA.X;
        }

        public static double PointY
        {
            get => PointPA.Y;
        }

        public static double PointZ
        {
            get => PointPA.Z;
        }

        public static double PointAngle
        {
            get => PointPA.Angle;
        }

        // Rng vars

        public static int GoalRngIndex
        {
            get => RngIndexer.GetRngIndex(GoalRngValue);
            set => GoalRngValue = RngIndexer.GetRngValue(value);
        }
        public static ushort GoalRngValue = 0;
        
        // PU vars

        public static int PuParam1 = 0;
        public static int PuParam2 = 1;

        public static double PuHypotenuse
        {
            get => MoreMath.GetHypotenuse(PuParam1, PuParam2);
        }

        // Mupen vars

        public static int MupenLagOffset = 0;
    }
}
