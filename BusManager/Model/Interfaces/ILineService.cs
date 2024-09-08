using BusManager.Model.Entities;

namespace BusManager.Model.Interfaces
{
    public interface ILineService
    {
        bool AddLine(Line line);
        List<Line>? GetActiveLines();
        List<Line>? GetAllLines();
        Line? GetLineById(int id);
        bool UpdateLine(Line line);
        bool DeleteLine(Line line);
    }
}
