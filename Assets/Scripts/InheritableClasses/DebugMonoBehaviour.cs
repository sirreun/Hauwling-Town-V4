using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using UnityEngine;

public class DebugMonoBehaviour : MonoBehaviour
{
    [Header("Debug Statements")]
    [SerializeField] private bool printDebugStatements = true;
    [Space(10)]
    [Header("Debug Statement Types")]
    [Header("update : ")]
    [SerializeField] private bool enableUpdateDebugStatements = true;
    [Space(10)]
    [SerializeField] private bool onlyCustomStatementsEnabled = false;
    public List<StatementType> customDebugStatementTypes = new List<StatementType>();

    /// <summary>
    /// Creates an easily toggleable debug statement. Turn off Print Debug Statements in the
    /// inspector to stop all debugs statement through this function from printing. Will not 
    /// print if Only Custom Statements Enabled is true.
    /// </summary>
    /// <param name="statement"></param>
    protected void Print(string statement)
    {
        if (printDebugStatements && !onlyCustomStatementsEnabled)
        {
            Debug.Log(statement);
        }
    }

    /// <summary>
    /// Creates an easily toggleable debug statement. Turn off Print Debug Statements in the
    /// inspector to stop all debugs statement through this function from printing. Adding a
    /// type thorugh a string allows the statement to be grouped with similar 
    /// debug statements.
    /// </summary>
    /// <param name="statement"></param>
    /// <param name="type"></param>
    protected void Print(string statement, string type)
    {
        if (!printDebugStatements && !onlyCustomStatementsEnabled)
        {
            return;
        }
        else if (customDebugStatementTypes.Count <= 0)
        {
            Debug.LogWarning(this + " custom debug statement warning: used a custom print, but this script has no custom types. Please add them in the inspector under Custom Debug Statement Types.");
        }

        foreach (StatementType statementType in customDebugStatementTypes)
        {
            if (statementType.Name == type && statementType.PrintEnabled)
            {
                if (statementType.PrintEnabled)
                {
                    Debug.Log(statement);
                }
                else
                {
                    return;
                }
            }
        }
    }
}