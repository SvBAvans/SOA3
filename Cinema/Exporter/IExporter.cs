using Cinema.Domain;

namespace Cinema.Exporter;

public interface IExporter
{
    void Export(Order order);
}