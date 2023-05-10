namespace MeterMaintenance.Models.Abstract;

public class Mutation<T>
{
    public T Entity { get; set; }
    public List<string> Fields { get; set; }
}