using BusManager.Model.Entities;
using BusManager.Model.Services;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using static System.Collections.Specialized.BitVector32;

namespace BusManager.View
{
    public class LineView
    {
        private static readonly LineService _service = new();
        private static readonly StationService _stationService = new();
        private static readonly StopService _stopService = new();
        private static readonly TripService _tripService = new();

        public static void LineLandingPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                    (
                        $"1. Listar todas as linhas\n" +
                        $"2. Listar as linhas ativas\n" +
                        $"3. Adicionar nova linha\n" +
                        $"4. Editar linha\n" +
                        $"5. Excluir linha\n" +
                        $"6. Visualizar rota por linha\n" +
                        $"7. Adicionar nova parada\n" +
                        $"8. Remover parada\n" +
                        $"9. Voltar"
                    );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ShowAllLines();
                        break;
                    case '2':
                        ShowActiveLines();
                        break;
                    case '3':
                        ShowAddLine();
                        break;
                    case '4':
                        ShowUpdateLine();
                        break;
                    case '5':
                        ShowDeleteLine();
                        break;
                    case '6':
                        ShowStopsByLine();
                        break;
                    case '7':
                        ShowAddStop();
                        break;
                    case '8':
                        ShowDeleteStop();
                        break;
                    case '9':
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla e tente novamente");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void ShowAllLines()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var lineList = _service.GetAllLines();

            if (lineList is not null && lineList?.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar todas as linhas\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Nome".PadRight(20)}" +
                    $"{"Código".PadRight(10)}" +
                    $"{"Origem".PadRight(20)}" +
                    $"{"Destino".PadRight(20)}" +
                    $"{"Ativo?"}");

                foreach (var line in lineList)
                {
                    Console.WriteLine($"{line.Id.ToString().PadRight(3)}" +
                        $"{line.Name.PadRight(20)}" +
                        $"{line.Code.PadRight(10)}" +
                        $"{line.Origin.PadRight(20)}" +
                        $"{line.Destiny.PadRight(20)}" +
                        $"{(line.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma linha encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowActiveLines()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var lineList = _service.GetActiveLines();

            if (lineList is not null && lineList?.Count > 0)
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar linhas ativas\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Nome".PadRight(20)}" +
                    $"{"Código".PadRight(10)}" +
                    $"{"Origem".PadRight(20)}" +
                    $"{"Destino".PadRight(20)}" +
                    $"{"Ativo?"}");

                foreach (var line in lineList)
                {
                    Console.WriteLine($"{line.Id.ToString().PadRight(3)}" +
                        $"{line.Name.PadRight(20)}" +
                        $"{line.Code.PadRight(10)}" +
                        $"{line.Origin.PadRight(20)}" +
                        $"{line.Destiny.PadRight(20)}" +
                        $"{(line.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma linha ativa encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowAddLine()
        {
            Console.Clear();
            Console.WriteLine("Menu - Adicionar linha\n");

            var line = new Line();

            Console.WriteLine("Digite o nome da linha");
            line.Name = Console.ReadLine();
            if (string.IsNullOrEmpty(line.Name) || line.Name.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("Nome não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Digite o código da linha");
            line.Code = Console.ReadLine();
            if (string.IsNullOrEmpty(line.Code) || line.Code.Length > 10)
            {
                Console.Clear();
                Console.WriteLine("Código não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Digite a origem da linha");
            line.Origin = Console.ReadLine();
            if (string.IsNullOrEmpty(line.Origin) || line.Origin.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("Origem não informada ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Digite o destino da linha");
            line.Destiny = Console.ReadLine();
            if (string.IsNullOrEmpty(line.Destiny) || line.Destiny.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("Destino não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Digite a descrição da linha");
            line.Description = Console.ReadLine();
            if (string.IsNullOrEmpty(line.Description) || line.Description.Length > 100)
            {
                Console.Clear();
                Console.WriteLine("Descrição não informada ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("A linha está ativa? (S/N)");
            line.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S' ? true : false;

            Console.Clear();
            Console.WriteLine("Carregando...");

            _service.AddLine(line);

            // Stop part
            while (true)
            {
                Console.Clear();
                var activeStations = _stationService.GetActiveStations();
                if (activeStations is null) {
                    Console.Clear();
                    Console.WriteLine("Não existem estações ativas suficientes.");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();
                    return;
                }
                var numMaxStations = activeStations!.Count;
                Console.WriteLine($"Quantas paradas terá a linha (Máximo de {numMaxStations} estações)");
                var stopCountS = Console.ReadLine();
                var stopCount = stopCountS is null ? 0 : int.Parse(stopCountS);
                if (stopCount < 2)
                {
                    Console.Clear();
                    Console.WriteLine("Você deve informar pelo menos 2 paradas");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    continue;
                }
                else if (stopCount > numMaxStations)
                {
                    Console.Clear();
                    Console.WriteLine($"Você deve informar no máximo {numMaxStations} estações");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    continue;
                }
                else
                {
                    var currentLine = _service.GetLastLine();
                    List<Stop> stopList = new List<Stop>();
                    for (int i = 0; i < stopCount; i++)
                    {
                        string? tempId;
                        while (true)
                        {
                            Console.Clear();
                            StationView.ShowActiveStations(true);
                            Console.WriteLine($"Digite o ID da estação para a parada {i + 1}");
                            tempId = Console.ReadLine();
                            var station = _stationService.GetStationById(int.Parse(tempId ?? "0"));
                            if (station is null)
                            {
                                Console.Clear();
                                Console.WriteLine($"Estação com id {tempId} não encontrada");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();

                                continue;
                            }
                            if (!station.Active)
                            {
                                Console.Clear();
                                Console.WriteLine($"Estação com id {tempId} não está ativa");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();

                                return;
                            }
                            if (_stopService.GetStopByStationAndLine(int.Parse(tempId), currentLine!.Id) is not null)
                            {
                                Console.Clear();
                                Console.WriteLine($"Estação com id {tempId} já adicionada a essa linha");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();

                                continue;
                            }
                            break;
                        }

                        string entrada;
                        DateTime horaChegada;

                        while (true)
                        {
                            Console.Write("Digite a hora de chegada do ônibus na parada (HH:mm): ");
                            entrada = Console.ReadLine();

                            if (DateTime.TryParseExact(entrada, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime tempHora))
                            {
                                horaChegada = new DateTime(1, 1, 1, tempHora.Hour, tempHora.Minute, 0, DateTimeKind.Utc);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Formato inválido. Por favor, insira a hora no formato HH:mm.");
                            }
                        }

                        Stop stop = new Stop()
                        {
                            LineId = currentLine.Id,
                            StationId = int.Parse(tempId),
                            Hour = horaChegada
                        };

                        stopList.Add(stop);
                    }

                    _stopService.AddManyStop(stopList);
                }
                break;
            }

            Console.Clear();
            Console.WriteLine("Sucesso ao criar nova linha");
            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }

        private static void ShowUpdateLine()
        {
            Console.Clear();
            Console.WriteLine("Menu - Editar linha\n");

            Console.WriteLine("Digite o id da linha a editar");
            var id = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var line = _service.GetLineById(id is null ? 0 : int.Parse(id));

            if (line is null)
            {
                Console.Clear();
                Console.WriteLine($"Linha com id {id} não encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu - Editar linha\n");

                Console.WriteLine
                        (
                            $"1. Atualizar Nome\n" +
                            $"2. Atualizar Código\n" +
                            $"3. Atualizar Origem\n" +
                            $"4. Atualizar Destino\n" +
                            $"5. Atualizar Descrição\n" +
                            $"6. Atualizar Status Ativo\n" +
                            $"7. Voltar"
                        );
                var choice = Console.ReadKey().KeyChar;

                var exit = false;

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Digite o novo nome da linha");
                        var name = Console.ReadLine();
                        if (string.IsNullOrEmpty(name) || name.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Nome não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        line.Name = name;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateLine(line))
                        {
                            Console.Clear();
                            Console.WriteLine("Nome atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Digite o novo código da linha");
                        var code = Console.ReadLine();
                        if (string.IsNullOrEmpty(code) || code.Length > 10)
                        {
                            Console.Clear();
                            Console.WriteLine("Código não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        line.Code = code;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateLine(line))
                        {
                            Console.Clear();
                            Console.WriteLine("Código atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("Digite a nova origem da linha");
                        var origin = Console.ReadLine();
                        if (string.IsNullOrEmpty(origin) || origin.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Origem não informada ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        line.Origin = origin;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateLine(line))
                        {
                            Console.Clear();
                            Console.WriteLine("Origem atualizada com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '4':
                        Console.Clear();
                        Console.WriteLine("Digite o novo destino da linha");
                        var destiny = Console.ReadLine();
                        if (string.IsNullOrEmpty(destiny) || destiny.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Destino não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        line.Destiny = destiny;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateLine(line))
                        {
                            Console.Clear();
                            Console.WriteLine("Destino atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '5':
                        Console.Clear();
                        Console.WriteLine("Digite a nova descrição da linha");
                        var description = Console.ReadLine();
                        if (string.IsNullOrEmpty(description) || description.Length > 100)
                        {
                            Console.Clear();
                            Console.WriteLine("Descrição não informada ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        line.Description = description;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateLine(line))
                        {
                            Console.Clear();
                            Console.WriteLine("Descrição atualizada com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '6':
                        Console.Clear();
                        Console.WriteLine("A linha está ativa? (S/N)");
                        var active = Console.ReadKey().KeyChar;
                        line.Active = active == 's' || active == 'S' ? true : false;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateLine(line))
                        {
                            Console.Clear();
                            Console.WriteLine("Status atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '7':
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
                    return;
                }
            }
        }

        private static void ShowDeleteLine()
        {
            Console.Clear();
            Console.WriteLine("Menu - Excluir linha\n");

            Console.WriteLine("Digite o id da linha");
            var id = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var line = _service.GetLineById(id is null ? 0 : int.Parse(id));

            if (line is not null)
            {
                if (_tripService.GetTripsByLineId(line.Id) is not null)
                {
                    Console.Clear();
                    Console.WriteLine($"Linha possui viagens registradas e não pode ser excluída");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();
                    return;
                }
                _stopService.DeleteStopsByLineId(line.Id);
                if (_service.DeleteLine(line))
                {
                    Console.Clear();
                    Console.WriteLine("Linha removida com sucesso");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao remover linha");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Linha com id {id} não encontrada");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }

        private static void ShowAddStop()
        {
            Console.Clear();
            Console.WriteLine("Menu - Adicionar Parada\n");

            Console.WriteLine("Digite o id da linha");
            var id = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var line = _service.GetLineById(id is null ? 0 : int.Parse(id));

            if (line is not null)
            {
                Console.Clear();
                StationView.ShowActiveStations(true);
                Console.WriteLine($"Digite o ID da estação");
                var tempId = Console.ReadLine();
                var station = _stationService.GetStationById(int.Parse(tempId ?? "0"));
                if (station is null)
                {
                    Console.Clear();
                    Console.WriteLine($"Estação com id {tempId} não encontrada");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                if (!station.Active)
                {
                    Console.Clear();
                    Console.WriteLine($"Estação com id {tempId} não está ativa");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                if (_stopService.GetStopByStationAndLine(int.Parse(tempId ?? "0"), line!.Id) is not null)
                {
                    Console.Clear();
                    Console.WriteLine($"Estação com id {tempId} já adicionada a essa linha");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }

                string? entrada;
                DateTime horaChegada;

                while (true)
                {
                    Console.Write("Digite a hora de chegada do ônibus na parada (HH:mm): ");
                    entrada = Console.ReadLine();

                    if (DateTime.TryParseExact(entrada, "HH:mm", null, System.Globalization.DateTimeStyles.None, out DateTime tempHora))
                    {
                        horaChegada = new DateTime(1, 1, 1, tempHora.Hour, tempHora.Minute, 0, DateTimeKind.Utc);

                        Console.WriteLine($"Hora de chegada do ônibus: {horaChegada.ToString("HH:mm")}");
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Formato inválido. Por favor, insira a hora no formato HH:mm.");
                    }
                }

                Stop stop = new Stop()
                {
                    LineId = int.Parse(id),
                    StationId = int.Parse(tempId),
                    Hour = horaChegada
                };

                if (_stopService.AddStop(stop))
                {
                    Console.Clear();
                    Console.WriteLine("Sucesso ao inserir nova parada");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao inserir nova parada");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Linha com id {id} não encontrada");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }

        private static void ShowStopsByLine ()
        {
            Console.Clear();
            Console.WriteLine($"Menu - Visualizar rota por linha\n");
            Console.WriteLine("Digite o id da linha");
            var lineId = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var line = _service.GetLineById(int.Parse(lineId ?? "0"));

            if (line is not null)
            {
                Console.Clear();
                Console.WriteLine($"Menu - Rota Linha {line.Name} - Código {line.Code}\n");

                var rota = _stopService.GetRouteLine(int.Parse(lineId ?? "0"));

                if (rota is not null)
                {
                    // Cabeçalho
                    Console.WriteLine(
                        $"{"Estação".PadRight(40)}" +
                        $"{"Horário".PadRight(20)}");

                    foreach (var stop in rota)
                    {
                        Console.WriteLine(
                            $"{stop.Station.Address}, {stop.Station.Number}".PadRight(40) +
                            $"{stop.Hour.ToString("HH:mm")}");
                    }

                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Essa linha não possui nenhuma rota ainda");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nenhuma linha encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowDeleteStop()
        {
            Console.Clear();
            Console.WriteLine("Menu - Remover Parada\n");

            Console.WriteLine("Digite o id da linha");
            var id = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var line = _service.GetLineById(id is null ? 0 : int.Parse(id));

            if (line is not null)
            {
                if (line.Stops.Count <= 2)
                {
                    Console.Clear();
                    Console.WriteLine($"Linha possui {line.Stops.Count} paradas, impossível reduzir o número de paradas (min 2)");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }

                Console.Clear();
                StationView.ShowActiveStations(true);
                Console.WriteLine($"Digite o ID da estação");
                var tempId = Console.ReadLine();
                var station = _stationService.GetStationById(int.Parse(tempId));
                if (station is null)
                {
                    Console.Clear();
                    Console.WriteLine($"Estação com id {tempId} não encontrada");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }

                Stop stop = new Stop()
                {
                    LineId = line.Id,
                    StationId = station.Id,
                };

                _stopService.GetStop(stop);

                if (_stopService.DeleteStop(stop))
                {
                    Console.Clear();
                    Console.WriteLine("Sucesso ao remover parada");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao remover parada");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Linha com id {id} não encontrada");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }
    }
}
