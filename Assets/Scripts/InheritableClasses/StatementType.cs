using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatementType
{
    public string Name = "name";
    public bool PrintEnabled = true;

    public StatementType(string name)
    {
        this.Name = name;
    }
}