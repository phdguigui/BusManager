
using BusManager.Model.Services;

namespace BusManager.View
{
    public class ReportView
    {
        private static readonly TripService _tripService = new();
        private static readonly DriverService _driverService = new();
        private static readonly StationService _stationService = new();
        public static void ReportLandingPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                    (
                        $"1. Listar todas as viagens\n" +
                        $"2. Motoristas com mais viagens\n" +
                        $"3. Estações Obsoletas\n" +
                        $"4. Voltar"
                    );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ShowAllTrips();
                        break;
                    case '2':
                        ShowDriversWithMostTrips();
                        break;
                    case '3':
                        ShowOutdatedStations();
                        break;
                    case '4':
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla e tente novamente");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowAllTrips()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var tripList = _tripService.GetAllTrips();

            if (tripList is not null && tripList.Any())
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar todas as viagens\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(5)}" +
                    $"{"Linha".PadRight(30)}" +
                    $"{"Data Início".PadRight(18)}" +
                    $"{"Data Término".PadRight(18)}" +
                    $"{"Motorista".PadRight(30)}" +
                    $"{"Placa Ônibus"}");

                foreach (var trip in tripList)
                {
                    Console.WriteLine($"{trip.Id.ToString().PadRight(5)}" +
                        $"{trip.Line.Name.PadRight(30)}" +
                        $"{trip.StartTime.ToString("dd/MM/yyyy HH:mm").PadRight(18)}" +
                        $"{trip.EndTime.ToString("dd/MM/yyyy HH:mm").PadRight(18)}" +
                        $"{trip.Driver.Name} {trip.Driver.Surname}".PadRight(30) +
                        $"{trip.Bus.Plate}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma viagem encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }

        private static void ShowDriversWithMostTrips()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var driverList = _driverService.GetDriverRanking();

            if (driverList is not null && driverList.Any())
            {
                Console.Clear();
                Console.WriteLine("Menu - Ranking motoristas com mais viagens\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Posição".PadRight(8)}" +
                    $"{"Nome".PadRight(15)}" +
                    $"{"Sobrenome".PadRight(15)}" +
                    $"{"Número de Viagens".PadRight(20)}" +
                    $"{"Data de Contratação".PadRight(30)}");

                int count = 1;
                foreach (var driver in driverList)
                {
                    Console.WriteLine(
                        $"#{count}".PadRight(8) +
                        $"{driver.Name.PadRight(15)}" +
                        $"{driver.Surname.PadRight(15)}" +
                        $"{driver.TripCount}".PadRight(20) +
                        $"{driver.HireDate.ToString("dd/MM/yyyy HH:mm").PadRight(30)}");
                    count++;
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma viagem encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }

        private static void ShowOutdatedStations()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var stationList = _stationService.GetOutdatedStations();

            if (stationList is not null && stationList?.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar Estações Obsoletas\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(5)}" +
                    $"{"Endereço".PadRight(30)}" +
                    $"{"Número".PadRight(10)}");

                foreach (var station in stationList)
                {
                    Console.WriteLine(
                        $"{station.Id.ToString().PadRight(5)}" +
                        $"{station.Address.PadRight(30)}" +
                        $"{station.Number.PadRight(10)}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma estação encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }
    }
}
