using BusManager.Model.Entities;
using BusManager.Model.Services;
using System.Globalization;
using System;

namespace BusManager.View
{
    public class TripView
    {
        private static readonly TripService _service = new();
        private static readonly BusService _busService = new();
        private static readonly DriverService _driverService = new();
        private static readonly LineService _lineService = new();

        public static void TripLandingPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                    (
                        $"1. Listar todas as viagens\n" +
                        $"2. Adicionar viagem\n" +
                        $"3. Editar viagem\n" +
                        $"4. Excluir viagem\n" +
                        $"5. Voltar"
                    );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ShowAllTrips();
                        break;
                    case '2':
                        ShowAddTrip();
                        break;
                    case '3':
                        ShowUpdateTrip();
                        break;
                    case '4':
                        ShowDeleteTrip();
                        break;
                    case '5':
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

            var tripList = _service.GetAllTrips();

            if (tripList is not null && tripList.Any())
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar todas as viagens\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Linha".PadRight(30)}" +
                    $"{"Data Início".PadRight(18)}" +
                    $"{"Data Término".PadRight(18)}" +
                    $"{"Motorista".PadRight(30)}" +
                    $"{"Placa Ônibus"}");

                foreach (var trip in tripList)
                {
                    Console.WriteLine($"{trip.Id.ToString().PadRight(3)}" +
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

        private static void ShowAddTrip()
        {
            Console.Clear();
            Console.WriteLine("Menu - Adicionar viagem\n");

            var trip = new Trip();

            Console.WriteLine("Digite a placa do ônibus da viagem");
            var plate = Console.ReadLine();
            if (string.IsNullOrEmpty(plate) || plate.Length > 10)
            {
                Console.Clear();
                Console.WriteLine("Placa inválida");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            var bus = _busService.GetBusByPlate(plate);

            if (bus is null)
            {
                Console.Clear();
                Console.WriteLine($"Ônibus não encontrado com a placa informada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Digite o CPF do motorista");
            var cpf = Console.ReadLine();
            if (string.IsNullOrEmpty(cpf) || cpf.Length > 11)
            {
                Console.Clear();
                Console.WriteLine("CPF inválido");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            var driver = _driverService.GetDriverByCPF(cpf);

            if (driver is null)
            {
                Console.Clear();
                Console.WriteLine($"Motorista não encontrado com o CPF informado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Digite o Id da linha");
            var lineId = int.Parse(Console.ReadLine());
            if (lineId < 1)
            {
                Console.Clear();
                Console.WriteLine("Linha inválida");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            var line = _lineService.GetLineById(lineId);

            if (line is null)
            {
                Console.Clear();
                Console.WriteLine($"Linha não encontrada com o id informado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            string format = "dd/MM/yyyy HH:mm";
            Console.WriteLine("Digite a data e hora do início da viagem (dd/MM/yyyy HH:mm):");
            string input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTime))
            {
                Console.Clear();
                Console.WriteLine("Data/Hora inválida");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            trip.StartTime = dateTime;

            Console.WriteLine("Digite a data e hora do final da viagem (dd/MM/yyyy HH:mm):");
            input = Console.ReadLine();
            if (!DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeEnd))
            {
                Console.Clear();
                Console.WriteLine("Data/Hora inválida");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            trip.EndTime = dateTimeEnd;

            trip.BusId = bus.Id;
            trip.DriverId = driver.Id;
            trip.LineId = line.Id;

            Console.Clear();
            Console.WriteLine("Carregando...");
            var result = _service.AddTrip(trip);

            if (result)
            {
                Console.Clear();
                Console.WriteLine("Viagem adicionada com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Falha ao adicionar viagem");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void ShowUpdateTrip()
        {
            Console.Clear();
            Console.WriteLine("Menu - Editar viagem\n");

            Console.WriteLine("Digite o ID da viagem a ser editada:");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.Clear();
                Console.WriteLine("ID inválido");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Carregando...");
            var trip = _service.GetTripById(id);

            if (trip is null)
            {
                Console.Clear();
                Console.WriteLine($"Viagem com ID {id} não encontrada");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu - Editar viagem\n");

                Console.WriteLine
                        (
                            $"1. Atualizar Ônibus\n" +
                            $"2. Atualizar Motorista\n" +
                            $"3. Atualizar Linha\n" +
                            $"4. Atualizar Data/Hora de Início\n" +
                            $"5. Atualizar Data/Hora de Fim\n" +
                            $"7. Voltar"
                        );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Digite a placa do novo ônibus:");
                        var plate = Console.ReadLine();
                        if (string.IsNullOrEmpty(plate) || plate.Length > 10)
                        {
                            Console.Clear();
                            Console.WriteLine("Placa inválida");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                            break;
                        }
                        var bus = _busService.GetBusByPlate(plate);
                        if (bus is null)
                        {
                            Console.Clear();
                            Console.WriteLine("Ônibus não encontrado com a placa informada");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            trip.BusId = bus.Id;
                            if (_service.UpdateTrip(trip))
                            {
                                Console.Clear();
                                Console.WriteLine("Sucesso ao atualizar ônibus da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Falha ao atualizar ônibus da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case '2':
                        Console.Clear();
                        Console.WriteLine("Digite o CPF do novo motorista:");
                        var cpf = Console.ReadLine();
                        if (string.IsNullOrEmpty(cpf) || cpf.Length > 11)
                        {
                            Console.Clear();
                            Console.WriteLine("CPF inválido");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                            break;
                        }
                        var driver = _driverService.GetDriverByCPF(cpf);
                        if (driver is null)
                        {
                            Console.Clear();
                            Console.WriteLine("Motorista não encontrado com o CPF informado");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            trip.DriverId = driver.Id;
                            if (_service.UpdateTrip(trip))
                            {
                                Console.Clear();
                                Console.WriteLine("Sucesso ao atualizar motorista da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Falha ao atualizar motorista da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case '3':
                        Console.Clear();
                        Console.WriteLine("Digite o ID da nova linha:");
                        if (!int.TryParse(Console.ReadLine(), out var lineId) || lineId < 1)
                        {
                            Console.Clear();
                            Console.WriteLine("Linha inválida");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                            break;
                        }
                        var line = _lineService.GetLineById(lineId);
                        if (line is null)
                        {
                            Console.Clear();
                            Console.WriteLine("Linha não encontrada com o id informado");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            trip.LineId = line.Id;
                            if (_service.UpdateTrip(trip))
                            {
                                Console.Clear();
                                Console.WriteLine("Sucesso ao atualizar linha da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Falha ao atualizar linha da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case '4':
                        Console.Clear();
                        Console.WriteLine("Digite a nova data e hora de início da viagem (dd/MM/yyyy HH:mm):");
                        string format = "dd/MM/yyyy HH:mm";
                        string input = Console.ReadLine();
                        if (!DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var newStartTime))
                        {
                            Console.Clear();
                            Console.WriteLine("Data/Hora inválida");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            trip.StartTime = newStartTime;
                            if (_service.UpdateTrip(trip))
                            {
                                Console.Clear();
                                Console.WriteLine("Sucesso ao atualizar data de início da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Falha ao atualizar data de início da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case '5':
                        Console.Clear();
                        Console.WriteLine("Digite a nova data e hora de fim da viagem (dd/MM/yyyy HH:mm):");
                        format = "dd/MM/yyyy HH:mm";
                        input = Console.ReadLine();
                        if (!DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var newEndTime))
                        {
                            Console.Clear();
                            Console.WriteLine("Data/Hora inválida");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            trip.EndTime = newEndTime;
                            if (_service.UpdateTrip(trip))
                            {
                                Console.Clear();
                                Console.WriteLine("Sucesso ao atualizar data final da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Falha ao atualizar data final da viagem");
                                Console.WriteLine("\nToque qualquer tecla para voltar...");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case '7':
                        return;

                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla e tente novamente");
                        Console.ReadKey();
                        break;
                }
            }

        }

        private static void ShowDeleteTrip()
        {
            Console.Clear();
            Console.WriteLine("Menu - Excluir viagem\n");

            Console.WriteLine("Digite o ID da viagem a ser excluída:");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.Clear();
                Console.WriteLine("ID inválido");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Carregando...");
            var trip = _service.GetTripById(id);

            if (trip is not null)
            {
                if (_service.DeleteTrip(trip))
                {
                    Console.Clear();
                    Console.WriteLine("Viagem excluída com sucesso");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao excluir viagem");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Viagem com ID {id} não encontrada");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
