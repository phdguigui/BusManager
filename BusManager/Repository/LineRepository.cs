using BusManager.Data;
using Microsoft.EntityFrameworkCore;

namespace BusManager.Repository
{
    public static class LineRepository
    {
        public static List<Line>? GetAllLines()
        {
            using var _db = new ApplicationContext();

            var LineList = _db.Line.AsNoTracking().ToList();
            return LineList;
        }

        public static List<Line>? GetActiveLines()
        {
            using var _db = new ApplicationContext();

            var LineList = _db.Line.Where(x => x.Active).AsNoTracking().ToList();
            return LineList;
        }

        public static Line? GetLineById(int id)
        {
            using var _db = new ApplicationContext();

            var Line = _db.Line.FirstOrDefault(x => x.Id == id);
            return Line;
        }

        public static bool AddLine(Line line)
        {
            using var _db = new ApplicationContext();

            _db.Line.Add(line);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool UpdateLine(Line line)
        {
            using var _db = new ApplicationContext();

            _db.Line.Update(line);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static bool DeleteLine(Line line)
        {
            using var _db = new ApplicationContext();

            _db.Line.Remove(line);
            var result = _db.SaveChanges();

            return result > 0;
        }

        public static Line? GetLastLine()
        {
            using var _db = new ApplicationContext();

            return _db.Line.OrderByDescending(x => x.Id).FirstOrDefault();
        }
    }
}
