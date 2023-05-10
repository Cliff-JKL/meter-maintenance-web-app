namespace MeterMaintenance.Models.ViewModels;

public class MeterIndex
{
    public IEnumerable<string> Houses { get; set; } = null!;

    public string Address { get; set; }
}