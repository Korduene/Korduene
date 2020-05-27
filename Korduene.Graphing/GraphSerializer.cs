using Newtonsoft.Json;

namespace Korduene.Graphing
{
    public static class GraphSerializer
    {
        public static string Seraialize(IGraph graph)
        {
            return JsonConvert.SerializeObject(graph, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Auto
            });
        }

        public static T Deserialize<T>(string json) where T : IGraph
        {
            var graph = JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Auto,
            });

            //graph.Members.OfType<INode>().ToList().ForEach(x => x.UpdateVisuals());

            graph.IsLoading = false;

            return graph;
        }
    }
}
