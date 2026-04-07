using System;
using UnityEditor;
using UnityEngine;
//using Unity.GraphToolkit.Editor;

namespace Unity.GraphToolkit.WorldMapEditor
{
    //[Graph(AssetExtension)]
    [Serializable]
    public class WorldMapGraph// : Graph
    {
        public const string AssetExtension = "wmgraph";

        [MenuItem("Assets/Create/Graph Toolkit Samples/World Map Graph", false)]
        static void CreateAssetFile()
        {
            //GraphDatabase.PromptInProjectBrowserToCreateNewAsset<WorldMapGraph>();
        }
    }
}
