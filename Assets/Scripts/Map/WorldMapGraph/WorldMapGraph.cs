using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Unity.GraphToolkit.Editor;
using System.ComponentModel;
using Codice.Client.Common;

namespace Unity.GraphToolkit.WorldMapEditor
{
    [Graph(AssetExtension)]
    [Serializable]
    public class WorldMapGraph : Graph
    {
        public const string AssetExtension = "wmgraph";
        //public List<LocationNode> LocationNodes = new List<LocationNode>();

        [MenuItem("Assets/Create/Graph Toolkit Samples/World Map Graph", false)]
        static void CreateAssetFile()
        {
            GraphDatabase.PromptInProjectBrowserToCreateNewAsset<WorldMapGraph>();
        }
    }
    //TODO: make location node child classes for different types to have extra information perhaps...
    // unless there should be another graph for this
    [Serializable]
    public class LocationNode : Node
    {
        const string connectionCountName = "ConnectionCount";
        public string id { get; private set; }

        public override void OnEnable()
        {
            if (id == null)
            {
                id = System.Guid.NewGuid().ToString(); // might reload everytime, not sure
            }
            base.OnEnable();
        }

        protected override void OnDefineOptions(IOptionDefinitionContext context)
        {
            context.AddOption(connectionCountName, typeof(int))
                 .WithDefaultValue(1)
                 .Delayed();
            context.AddOption("ID", typeof(string))
                 .WithDefaultValue(id)
                 .Delayed();
        }
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<string>("Name").Build();
            context.AddInputPort<Districts>("District").Build();

            var connectionCountOption = GetNodeOptionByName(connectionCountName);
            connectionCountOption.TryGetValue<int>(out var connectionCount);

            for (var i = 0; i < connectionCount; i++)
            {
                context.AddInputPort<string>($"{i}").Build();
            }

            context.AddOutputPort<LocationNode>("Connections").Build();
            context.AddOutputPort<string>("TrueID").Build();
            //IPort connectIDPort = GetOutputPortByName("ConnectID");
            //if (connectIDPort != null)
            //{
                //connectIDPort.TrySetValue(id); // TODO: when 6.5 comes out, it should have this
            //}
            
        }
    }
}
