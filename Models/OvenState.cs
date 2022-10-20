using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AISIN_WFA.Models
{
    public class OvenState
    {
        public string LighTower { get; set; } = string.Empty;
        public string RecipeName { get; set; } = string.Empty;
        public float[] RailWidthSP { get; set; } = new float[] { 0, 0, 0, 0 };
        public float[] RailWidthPV { get; set; } = new float[] { 0, 0, 0, 0 };
        public float[] BeltSpeedSP { get; set; } = new float[] { 0, 0 };
        public float[] BeltSpeedPV { get; set; } = new float[] { 0, 0 };
        public float[] ProcessedCount { get; set; } = new float[] { 0, 0, 0, 0 };
        public float[] InOvenCount { get; set; } = new float[] { 0, 0, 0, 0 };
        public float[] TopZoneTemperatureSP { get; set; } = new float[50];
        public float[] BottomZoneTemperatureSP { get; set; } = new float[50];
        public float[] TopZoneTemperaturePV { get; set; } = new float[50];
        public float[] BottomZoneTemperaturePV { get; set; } = new float[50];
        public float[] ZoneTemperatureSP { get; set; } = new float[100];
        public float[] OxgenPPM { get; set; } = new float[] { 0, 0, 0 };
    }
}
