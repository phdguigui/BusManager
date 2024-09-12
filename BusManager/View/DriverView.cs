using BusManager.Model.Entities;
using BusManager.Model.Services;
using System.Xml;

namespace BusManager.View
{
    public class DriverView
    {
        private static readonly DriverService _service = new();
        private static readonly TripService _tripService = new();

        public static void DriverLandingPage()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine
                (
                    $"1. Listar todos os motoristas\n" +
                    $"2. Listar motoristas ativos\n" +
                    $"3. Pesquisar motorista por CPF\n" +
                    $"4. Adicionar novo motorista\n" +
                    $"5. Editar motorista\n" +
                    $"6. Excluir motorista\n" +
                    $"7. Voltar"
                );

                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        ShowAllDrivers();
                        break;
                    case '2':
                        ShowActiveDrivers();
                        break;
                    case '3':
                        ShowDriverByCPF();
                        break;
                    case '4':
                        ShowAddDriver();
                        break;
                    case '5':
                        ShowUpdateDriver();
                        break;
                    case '6':
                        ShowDeleteDriver();
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

        private static void ShowAllDrivers()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var driverList = _service.GetAllDrivers();

            if (driverList is not null && driverList.Any())
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar todos os motoristas\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Nome".PadRight(15)}" +
                    $"{"Sobrenome".PadRight(15)}" +
                    $"{"CPF".PadRight(15)}" +
                    $"{"Data Nascimento".PadRight(18)}" +
                    $"{"Data Contratação".PadRight(19)}" +
                    $"{"Ativo?"}");

                foreach (var driver in driverList)
                {
                    Console.WriteLine($"{driver.Id.ToString().PadRight(3)}" +
                        $"{driver.Name.PadRight(15)}" +
                        $"{driver.Surname.PadRight(15)}" +
                        $"{driver.CPF.PadRight(15)}" +
                        $"{driver.Birthday.ToString("dd/MM/yyyy").PadRight(18)}" +
                        $"{driver.HireDate.ToString("dd/MM/yyyy").PadRight(19)}" +
                        $"{(driver.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nNenhum motorista encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }

        private static void ShowActiveDrivers()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            var driverList = _service.GetActiveDrivers();

            if (driverList is not null && driverList.Any())
            {
                Console.Clear();
                Console.WriteLine("Menu - Listar motoristas ativos\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Nome".PadRight(15)}" +
                    $"{"Sobrenome".PadRight(15)}" +
                    $"{"CPF".PadRight(15)}" +
                    $"{"Data Nascimento".PadRight(18)}" +
                    $"{"Data Contratação".PadRight(19)}" +
                    $"{"Ativo?"}");

                foreach (var driver in driverList)
                {
                    Console.WriteLine($"{driver.Id.ToString().PadRight(3)}" +
                        $"{driver.Name.PadRight(15)}" +
                        $"{driver.Surname.PadRight(15)}" +
                        $"{driver.CPF.PadRight(15)}" +
                        $"{driver.Birthday.ToString("dd/MM/yyyy").PadRight(18)}" +
                        $"{driver.HireDate.ToString("dd/MM/yyyy").PadRight(19)}" +
                        $"{(driver.Active ? "Sim" : "Não")}");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\nNenhum motorista ativo encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }

        private static void ShowDriverByCPF()
        {
            Console.Clear();
            Console.WriteLine("Carregando...");

            Console.WriteLine("Digite o CPF do motorista:");
            var cpf = Console.ReadLine();

            var driver = _service.GetDriverByCPF(cpf ?? "");

            if (driver is not null)
            {
                Console.Clear();
                Console.WriteLine("Menu - Pesquisar Motorista por CPF\n");
                // Cabeçalho
                Console.WriteLine(
                    $"{"Id".PadRight(3)}" +
                    $"{"Nome".PadRight(15)}" +
                    $"{"Sobrenome".PadRight(15)}" +
                    $"{"CPF".PadRight(15)}" +
                    $"{"Data Nascimento".PadRight(18)}" +
                    $"{"Data Contratação".PadRight(19)}" +
                    $"{"Ativo?"}");

                Console.WriteLine($"{driver.Id.ToString().PadRight(3)}" +
                    $"{driver.Name.PadRight(15)}" +
                    $"{driver.Surname.PadRight(15)}" +
                    $"{driver.CPF.PadRight(15)}" +
                    $"{driver.Birthday.ToString("dd/MM/yyyy").PadRight(18)}" +
                    $"{driver.HireDate.ToString("dd/MM/yyyy").PadRight(19)}" +
                    $"{(driver.Active ? "Sim" : "Não")}");

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"Nenhum motorista com o CPF {cpf} encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }

        private static void ShowAddDriver()
        {
            Console.Clear();
            Console.WriteLine("Menu - Adicionar Motorista\n");

            var driver = new Driver();

            Console.WriteLine("Digite o nome do motorista:");
            driver.Name = Console.ReadLine();
            if (string.IsNullOrEmpty(driver.Name) || driver.Name.Length > 15)
            {
                Console.Clear();
                Console.WriteLine("Nome não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Digite o sobrenome do motorista:");
            driver.Surname = Console.ReadLine();
            if (string.IsNullOrEmpty(driver.Surname) || driver.Surname.Length > 15)
            {
                Console.Clear();
                Console.WriteLine("Sobrenome não informado ou ultrapassa o limite de caracteres");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Digite o CPF do motorista:");
            driver.CPF = Console.ReadLine();
            if (string.IsNullOrEmpty(driver.CPF) || driver.CPF.Length != 11)
            {
                Console.Clear();
                Console.WriteLine("CPF não informado ou inválido");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            if (_service.GetDriverByCPF(driver.CPF) is not null)
            {
                Console.Clear();
                Console.WriteLine("CPF já utilizado por outro motorista");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.Write("Digite a data de nascimento do Motorista (yyyy-MM-dd): ");
            var birthdayInput = Console.ReadLine();
            if (!DateTime.TryParse(birthdayInput, out var birthday))
            {
                Console.Clear();
                Console.WriteLine("Data inválida");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            driver.Birthday = birthday;

            Console.Write("Digite a data de contratação do Motorista (yyyy-MM-dd): ");
            var hireDateInput = Console.ReadLine();
            if (!DateTime.TryParse(hireDateInput, out var hireDate))
            {
                Console.Clear();
                Console.WriteLine("Data inválida");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }
            driver.HireDate = hireDate;

            Console.WriteLine("O motorista está ativo? (S/N)");
            driver.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S';

            Console.Clear();
            Console.WriteLine("Carregando...");
            var result = _service.AddDriver(driver);

            if (result)
            {
                Console.Clear();
                Console.WriteLine("Motorista adicionado com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Falha ao adicionar motorista");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();
        }

        private static void ShowUpdateDriver()
        {
            Console.Clear();
            Console.WriteLine("Menu - Editar Motorista\n");

            Console.WriteLine("Digite o CPF do motorista a editar:");
            var cpf = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Carregando...");
            var driver = _service.GetDriverByCPF(cpf ?? "");

            if (driver is null)
            {
                Console.Clear();
                Console.WriteLine($"Motorista com o CPF {cpf} não encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Menu - Editar Motorista\n");

                Console.WriteLine(
                    $"1. Atualizar Nome\n" +
                    $"2. Atualizar Sobrenome\n" +
                    $"3. Atualizar Status Ativo\n" +
                    $"4. Voltar"
                );

                var choice = Console.ReadKey().KeyChar;

                var exit = false;

                switch (choice)
                {
                    case '1':
                        Console.Clear();
                        Console.WriteLine("Digite o novo nome do motorista:");
                        var name = Console.ReadLine();
                        if (string.IsNullOrEmpty(name) || name.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Nome não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                            break;
                        }
                        driver.Name = name;
                        break;
                    case '2':
                        Console.Clear();
                        Console.WriteLine("Digite o novo sobrenome do motorista:");
                        var surname = Console.ReadLine();
                        if (string.IsNullOrEmpty(surname) || surname.Length > 50)
                        {
                            Console.Clear();
                            Console.WriteLine("Sobrenome não informado ou ultrapassa o limite de caracteres");
                            Console.WriteLine("\nToque qualquer tecla para voltar...");
                            Console.ReadKey();
                            break;
                        }
                        driver.Surname = surname;
                        break;
                    case '3':
                        Console.Clear();
                        Console.WriteLine("O motorista está ativo? (S/N)");
                        driver.Active = Console.ReadKey().KeyChar == 's' || Console.ReadKey().KeyChar == 'S';
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
                    break;

                Console.Clear();
                Console.WriteLine("Carregando...");
                var result = _service.UpdateDriver(driver);

                if (result)
                {
                    Console.Clear();
                    Console.WriteLine("Motorista atualizado com sucesso");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Falha ao atualizar motorista");
                }

                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
            }
        }

        private static void ShowDeleteDriver()
        {
            Console.Clear();
            Console.WriteLine("Menu - Excluir Motorista\n");

            Console.WriteLine("Digite o CPF do motorista a excluir:");
            var cpf = Console.ReadLine();

            var driver = _service.GetDriverByCPF(cpf ?? "");

            if (driver is null)
            {
                Console.Clear();
                Console.WriteLine($"Motorista com o CPF {cpf} não encontrado");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            if (_tripService.GetTripsByDriverId(driver.Id) is not null)
            {
                Console.Clear();
                Console.WriteLine($"Ônibus possui viagens registradas e não pode ser excluído");
                Console.WriteLine("\nToque qualquer tecla para voltar...");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Carregando...");
            var result = _service.DeleteDriver(driver);

            if (result)
            {
                Console.Clear();
                Console.WriteLine("Motorista excluído com sucesso");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Falha ao excluir motorista");
            }

            Console.WriteLine("\nToque qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
