using BusManager.Model.Entities;
using BusManager.Model.Services;

namespace BusManager.View
{
    public class BusView
    {
        private static readonly BusService _service = new();
        private static readonly TripService _tripService = new();
        public static void BusLandingPage ()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                    (
                        $"1. Listar todos os ônibus\n" +
                        $"2. Listar os ônibus ativos\n" +
                        $"3. Pesquisar ônibus por placa\n" +
                        $"4. Adicionar novo ônibus\n" +
                        $"5. Editar ônibus\n" +
                        $"6. Excluir ônibus\n" +
                        $"7. Voltar"
                    );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ShowAllBuses();
                        break;
                    case '2':
                        ShowActiveBuses();
                        break;
                    case '3':
                        ShowBusByPlate();
                        break;
                    case '4':
                        ShowAddBus();
                        break;
                    case '5':
                        ShowUpdateBus();
                        break;
                    case '6':
                        ShowDeleteBus();
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

        private static void ShowAllBuses()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var busList = _service.GetAllBuses();

            if (busList is not null)
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar todos os ônibus\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Modelo".PadRight(15)}" +
                    $"{"Placa".PadRight(10)}" +
                    $"{"Ativo?"}");

                foreach (var bus in busList)
                {
                    Console.WriteLine($"{bus.Id.ToString().PadRight(3)}" +
                        $"{bus.Model.PadRight(15)}" +
                        $"{bus.Plate.PadRight(10)}" +
                        $"{(bus.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.WriteLine("\nNenhum ônibus encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowActiveBuses()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var busList = _service.GetActiveBuses();

            if (busList is not null)
            {
                Console.Clear();
                // Cabeçalho
                Console.WriteLine("Menu - Pesquisar Ônibus Ativos\n");
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Modelo".PadRight(15)}" +
                    $"{"Placa".PadRight(10)}" +
                    $"{"Ativo?"}");

                foreach (var bus in busList)
                {
                    Console.WriteLine($"{bus.Id.ToString().PadRight(3)}" +
                        $"{bus.Model.PadRight(15)}" +
                        $"{bus.Plate.PadRight(10)}" +
                        $"{(bus.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.WriteLine("\nNenhum ônibus ativo encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowBusByPlate()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            Console.WriteLine("Digite a placa do ônibus");
            var plate = Console.ReadLine();

            var bus = _service.GetBusByPlate(plate is null ? "" : plate);

            if (bus is not null)
            {
                Console.Clear();
                Console.WriteLine("Menu - Pesquisar Ônibus por Placa\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Modelo".PadRight(15)}" +
                    $"{"Placa".PadRight(10)}" +
                    $"{"Ativo?"}");

                Console.WriteLine($"{bus.Id.ToString().PadRight(3)}" +
                    $"{bus.Model.PadRight(15)}" +
                    $"{bus.Plate.PadRight(10)}" +
                    $"{(bus.Active ? "Sim" : "Não")}");

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Nenhum ônibus com a placa {plate} encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
        }

        private static void ShowAddBus()
        {
            Console.Clear();
            Console.WriteLine("Menu - Adicionar ônibus\n");

            var bus = new Bus();

            Console.WriteLine("Digite o modelo do ônibus");
            bus.Model = Console.ReadLine();
            if (string.IsNullOrEmpty(bus.Model) || bus.Model.Length > 50)
            {
                Console.Clear();
                Console.WriteLine("Modelo não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("Digite a placa do ônibus");
            bus.Plate = Console.ReadLine();
            if (string.IsNullOrEmpty(bus.Plate) || bus.Plate.Length > 10)
            {
                Console.Clear();
                Console.WriteLine("Placa não informada ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            if (_service.GetBusByPlate(bus.Plate) is not null)
            {
                Console.Clear();
                Console.WriteLine("Placa já utilizada em outro ônibus");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }
            Console.WriteLine("O ônibus está ativo? (S/N)");
            bus.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S' ? true : false;

            Console.Clear();
            Console.WriteLine("Carregando...");
            var result = _service.AddBus(bus);

            if (result)
            {
                Console.Clear();
                Console.WriteLine("Ônibus adicionado com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Falha ao adicionar ônibus");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }

        private static void ShowUpdateBus()
        {
            Console.Clear();
            Console.WriteLine("Menu - Editar ônibus\n");

            Console.WriteLine("Digite a placa do ônibus a editar");

            var plate = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Carregando...");
            var bus = _service.GetBusByPlate(plate);

            if (bus is null)
            {
                Console.Clear();
                Console.WriteLine($"Ônibus com a placa {plate} não encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();

                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu - Editar ônibus\n");

                Console.WriteLine
                        (
                            $"1. Atualizar Placa\n" +
                            $"2. Atualizar Modelo\n" +
                            $"3. Atualizar Status Ativo\n" +
                            $"4. Voltar"
                        );
                var choice = Console.ReadKey().KeyChar;

                var exit = false;

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Digite a nova placa do ônibus");
                        var plate2 = Console.ReadLine();
                        if (string.IsNullOrEmpty(plate2) || plate2.Length > 10)
                        {
                            Console.Clear();
                            Console.WriteLine("Placa não informada ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        if (_service.GetBusByPlate(plate2) is not null)
                        {
                            Console.Clear();
                            Console.WriteLine("Placa já utilizada em outro ônibus");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        bus.Plate = plate2;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateBus(bus))
                        {
                            Console.Clear();
                            Console.WriteLine("Placa atualizada com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Digite o novo modelo do ônibus");
                        var model = Console.ReadLine();
                        if (string.IsNullOrEmpty(model) || model.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Modelo não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();

                            break;
                        }
                        bus.Model = model;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateBus(bus))
                        {
                            Console.Clear();
                            Console.WriteLine("Modelo atualizado com sucesso");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                        }
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("O ônibus está ativo? (S/N)");
                        bus.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S' ? true : false;
                        Console.Clear();
                        Console.WriteLine("Carregando...");
                        if (_service.UpdateBus(bus))
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

        private static void ShowDeleteBus()
        {
            Console.Clear();
            Console.WriteLine("Menu - Excluir ônibus\n");

            Console.WriteLine("Digite a placa do ônibus");
            var plate = Console.ReadLine();

            Console.Clear();
            Console.WriteLine("Carregando...");
            var bus = _service.GetBusByPlate(plate is null ? "" : plate);

            if (bus is not null)
            {
                if (_tripService.GetTripsByBusId(bus.Id) is not null)
                {
                    Console.Clear();
                    Console.WriteLine($"Motorista possui viagens em seu nome e não pode ser excluído");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();
                    return;
                }
                if (_service.DeleteBus(bus))
                {
                    Console.Clear();
                    Console.WriteLine("Ônibus removido com sucesso");
                    Console.WriteLine("\nToque qualquer tecla para voltar...");
                    Console.ReadKey();

                    return;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao remover ônibus");
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Ônibus com a placa {plate} não encontrado");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();

            return;
        }
    }
}
