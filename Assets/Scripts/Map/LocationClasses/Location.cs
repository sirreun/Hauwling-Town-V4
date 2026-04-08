using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Need to think closer about how to build these classes for the easiest usability

[Serializable]
public class Location
{
    public string ID {  get; set; }
    public string Name { get; set; }
    public string SceneName { get; set; }
    public Vector3 SpawnPosition { get; set; }

    public Location(string id, string name, string sceneName, Vector3 spawnPosition)
    {
        ID = id;
        Name = name;
        SceneName = sceneName;
        SpawnPosition = spawnPosition;
    }
}

public enum Districts
{
    Downtown,
    Eastside,
    LeneveHarbor,
    MerlinHeights
}

public enum DowntownStreets 
{
    MainStreet,
    UnionStreet,
    FoxwoodStreet
}
