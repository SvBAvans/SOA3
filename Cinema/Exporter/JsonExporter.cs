using Cinema.Domain;
using Newtonsoft.Json;

namespace Cinema.Exporter;

public class JsonExporter : IExporter
{
    public void Export(Order order)
    {
        var json = JsonConvert.SerializeObject(order, Formatting.Indented);
        File.WriteAllText("output.json", json);
    }
}