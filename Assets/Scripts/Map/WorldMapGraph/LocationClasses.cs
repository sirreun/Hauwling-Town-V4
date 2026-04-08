using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Partial Copy of Scripts/Map/LocationClasses/Location.cs 
namespace Unity.GraphToolkit.WorldMapEditor
{
    public class LocationClasses
    {
    
    }

    [Serializable]
    public class LocationJson
    {
        public string ID;
        public string Name;
        public List<string> Connections;
    }

    [Serializable]
    public class LocationJsonList
    {
        public List<LocationJson> Data;
    }

    public enum Districts
    {
        Downtown,
        Eastside,
        LeneveHarbor,
        MerlinHeights
    }
}

