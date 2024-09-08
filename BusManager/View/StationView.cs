using BusManager.Model.Entities;
using BusManager.Model.Services;

namespace BusManager.View
{
    public class StationView
    {
        private static readonly StationService _service = new();
        private static readonly StopService _stopService = new();

        public static void StationLandingPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                    (
                        $"1. Listar todas as estações\n" +
                        $"2. Listar as estações ativas\n" +
                        $"3. Adicionar nova estação\n" +
                        $"4. Editar estação\n" +
                        $"5. Excluir estação\n" +
                        $"6. Voltar"
                    );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ShowAllStations();
                        break;
                    case '2':
                        ShowActiveStations();
                        break;
                    case '3':
                        ShowAddStation();
                        break;
                    case '4':
                        ShowUpdateStation();
                        break;
                    case '5':
                        ShowDeleteStation();
                        break;
                    case '6':
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla e tente novamente");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowAllStations()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var stationList = _service.GetAllStations();

            if (stationList is not null && stationList?.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar todas as estações\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Endereço".PadRight(30)}" +
                    $"{"Número".PadRight(10)}" +
                    $"{"Ativo?"}");

                foreach (var station in stationList)
                {
                    Console.WriteLine($"{station.Id.ToString().PadRight(3)}" +
                        $"{station.Address.PadRight(30)}" +
                        $"{station.Number.PadRight(10)}" +
                        $"{(station.Active ? "Sim" : "Não")}");
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

        public static void ShowActiveStations(bool externalCall = false)
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var stationList = _service.GetActiveStations();

            if (stationList is not null && stationList?.Count > 0)
            {
                Console.Clear();
                if (externalCall)
                    Console.WriteLine("Estações Ativas\n");
                else
                    Console.WriteLine("Menu - Listar estações ativas\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Endereço".PadRight(30)}" +
                    $"{"Número".PadRight(10)}" +
                    $"{"Ativo?"}");

                foreach (var station in stationList)
                {
                    Console.WriteLine($"{station.Id.ToString().PadRight(3)}" +
                        $"{station.Address.PadRight(30)}" +
                        $"{station.Number.PadRight(10)}" +
                        $"{(station.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma estação ativa encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowAddStation()
        {
            Console.Clear();
            Console.WriteLine("Menu - Adicionar estação\n");

            var station = new Station();

            Console.WriteLine("Digite o endereço da estação");
            station.Address = Console.ReadLine();
            if (string.IsNullOrEmpty(station.Address) || station.Address.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("Endereço não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Digite o número da estação");
            station.Number = Console.ReadLine();
            if (string.IsNullOrEmpty(station.Number) || station.Number.Length > 10)
            {
                Console.Clear();
                Console.WriteLine("Número não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("A estação está ativa? (S/N)");
            station.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S' ? true : false;

            Console.Clear();
            Console.WriteLine("Carregando...");
            var result = _service.AddStation(station);

            if (result)
            {
                Console.Clear();
                Console.WriteLine("Estação adicionada com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Falha ao adicionar estação");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }

        private static void ShowUpdateStation()
        {
            Console.Clear();
            Console.WriteLine("Menu - Editar estação\n");

            Console.WriteLine("Digite o id da estação a editar");
            var id = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var station = _service.GetStationById(id is null ? 0 : int.Parse(id));

            if (station is null)
            {
                Console.Clear();
                Console.WriteLine($"Estação com id {id} não encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu - Editar estação\n");

                Console.WriteLine
                        (
                            $"1. Atualizar Endereço\n" +
                            $"2. Atualizar Número\n" +
                            $"3. Atualizar Status Ativo\n" +
                            $"4. Voltar"
                        );
                var choice = Console.ReadKey().KeyChar;

                var exit = false;

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Digite o novo endereço da estação");
                        var address = Console.ReadLine();
                        if (string.IsNullOrEmpty(address) || address.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Endereço não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        station.Address = address;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateStation(station))
                        {
                            Console.Clear();
                            Console.WriteLine("Endereço atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Digite o novo número da estação");
                        var number2 = Console.ReadLine();
                        if (string.IsNullOrEmpty(number2) || number2.Length > 10)
                        {
                            Console.Clear();
                            Console.WriteLine("Número não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        station.Number = number2;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateStation(station))
                        {
                            Console.Clear();
                            Console.WriteLine("Número atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("A estação está ativa? (S/N)");
                        station.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S' ? true : false;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateStation(station))
                        {
                            Console.Clear();
                            Console.WriteLine("Status Ativo atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        exit = true;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla e tente novamente");
                        Console.ReadKey();
                        break;
                }
                if (exit)
                {
                    break;
                }
            }
        }

        private static void ShowDeleteStation()
        {
            Console.Clear();
            Console.WriteLine("Menu - Excluir estação\n");

            Console.WriteLine("Digite o id da estação");
            var id = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var station = _service.GetStationById(id is null ? 0 : int.Parse(id));

            if (station is not null)
            {
                if (_stopService.GetStopsByStation(station)?.Count > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Impossível deletar estação. Estação está atribuída à uma linha/parada.");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                if (_service.DeleteStation(station))
                {
                    Console.Clear();
                    Console.WriteLine("Estação removida com sucesso");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao remover estação");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Estação com id {id} não encontrada");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }
    }
}
