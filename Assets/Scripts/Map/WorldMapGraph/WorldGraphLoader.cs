using UnityEngine;
using UnityEditor;
using Unity.GraphToolkit.Editor;
using System.Collections.Generic;

namespace Unity.GraphToolkit.WorldMapEditor
{
    // Turns the WorldMap graph into json files that can be read from during game runntime
    //https://docs.unity3d.com/6000.5/Documentation/ScriptReference/Unity.GraphToolkit.Editor.IPort.html
    public class WorldGraphLoader : MonoBehaviour
    {

        [MenuItem("Graphs/Save World Graph To Json")]
        public static void SaveGraphToJson()
        {
            List<LocationJson> nodesToSave = new List<LocationJson>();

            WorldMapGraph graph = GraphDatabase.LoadGraph<WorldMapGraph>("Assets/Scripts/Map/WorldMapGraph/WorldMap.wmgraph");
            var nodes = graph.GetNodes();

            foreach ( var node in nodes )
            {
                LocationJson newLocationJson = new LocationJson();
                List<string> connections = new List<string>();
                
                LocationNode locationNode = node as LocationNode;
                newLocationJson.ID = locationNode.id;
                
                foreach (IPort inputPort in locationNode.GetInputPorts())
                {
                    inputPort.TryGetValue(out string name);
                    inputPort.TryGetValue(out LocationNode connection);
                    
                    if ( connection != null)
                    {
                        connections.Add(connection.id);
                    }
                    if ( name != null)
                    {
                        newLocationJson.Name = name;
                    }
                }

                newLocationJson.Connections = connections;
                nodesToSave.Add(newLocationJson);
            }

            //https://docs.unity3d.com/2020.3/Documentation/Manual/JSONSerialization.html
            //TODO: serialize to json file, see saving to json file youtub eideo too
        }
    }
}
