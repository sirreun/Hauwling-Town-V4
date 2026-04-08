using UnityEngine;
using UnityEditor;
using Unity.GraphToolkit.Editor;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Unity.GraphToolkit.WorldMapEditor
{
    // Turns the WorldMap graph into json files that can be read from during game runntime
    //https://docs.unity3d.com/6000.5/Documentation/ScriptReference/Unity.GraphToolkit.Editor.IPort.html
    public class WorldGraphLoader : MonoBehaviour
    {
        private const string jsonSavePath = "Assets/Databases/WorldMap";
        private const string jsonFileName = "worldmapdata.json";

        [MenuItem("Graphs/Save World Graph To Json")]
        public static void SaveGraphToJson()
        {
            List<LocationJson> nodesToSave = new List<LocationJson>();

            WorldMapGraph graph = GraphDatabase.LoadGraph<WorldMapGraph>("Assets/Scripts/Map/WorldMapGraph/WorldMap.wmgraph");
            var nodes = graph.GetNodes();

            foreach ( var node in nodes )
            {
                // Remove nodes that aren't location nodes
                if (node.InputPortCount < 1)
                {
                    continue;
                }

                LocationJson newLocationJson = new LocationJson();
                List<string> connections = new List<string>();
                
                LocationNode locationNode = node as LocationNode;
                //newLocationJson.ID = locationNode.id;
                
                Debug.Log("Node " +  locationNode.id); //TODO: ids are diff from ones in graph
                //Debug.Log("Input: " + locationNode.GetInputPorts().Count<IPort>());
                
                foreach (IPort inputPort in locationNode.GetInputPorts())
                {
                    inputPort.TryGetValue(out string value);

                    if (inputPort.DisplayName == "Name")
                    {
                        newLocationJson.Name = value;
                    }
                    else if (inputPort.DisplayName == "District")
                    {

                    }
                    else
                    {
                        //if (inputPort.FirstConnectedPort != null)
                        //{
                            //inputPort.FirstConnectedPort.TryGetValue(out value);
                            //Debug.Log("Connected port: " + value);
                        connections.Add(value);
                        //}

                    }
                }

                //Debug.Log("Output: " + locationNode.GetOutputPorts().Count<IPort>());
                foreach (IPort outputPort in locationNode.GetOutputPorts())
                {
                    outputPort.TryGetValue(out string id);

                    if (id != null)
                    {
                        newLocationJson.ID = id;
                    }
                }

                foreach( string connection in connections )
                {
                    Debug.Log("connection: " + connection);
                }

                newLocationJson.Connections = new List<string>(connections);
                //Debug.Log("connections: " + connections.Count + " saved: " +  newLocationJson.Connections.Count);
                nodesToSave.Add(newLocationJson);
            }

            LocationJsonList locationJsonList = new LocationJsonList();
            locationJsonList.Data = nodesToSave;

            //https://docs.unity3d.com/2020.3/Documentation/Manual/JSONSerialization.html
            //TODO: serialize to json file, see saving to json file youtub eideo too
            string fullSavePath = Path.Combine(jsonSavePath, jsonFileName);
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullSavePath));

                string dataToStore = JsonUtility.ToJson(locationJsonList, true);

                using (FileStream stream = new FileStream(fullSavePath, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning("WorldGraphLoader Error: " + e);
            }

            Debug.Log("WorldGraphLoader: Complete");
        }
    }
}
