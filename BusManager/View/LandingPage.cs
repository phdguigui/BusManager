namespace BusManager.View
{
    public class LandingPage
    {
        public static void Present()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(
                    $"BusManager Menu Principal\n\n" +
                    $"1. Gerenciar Ônibus\n" +
                    $"2. Gerenciar Motoristas\n" +
                    $"3. Gerenciar Estações\n" +
                    $"4. Gerenciar Linhas\n" +
                    $"5. Gerenciar Viagens\n" +
                    $"6. Relatórios\n" +
                    $"7. Sair"
                );
                var choice = Console.ReadKey().KeyChar;

                switch (choice)
                {
                    case '1':
                        BusView.BusLandingPage();
                        break;
                    case '2':
                        DriverView.DriverLandingPage();
                        break;
                    case '3':
                        StationView.StationLandingPage();
                        break;
                    case '4':
                        LineView.LineLandingPage();
                        break;
                    case '5':
                        TripView.TripLandingPage();
                        break;
                    case '6':
                        ReportView.ReportLandingPage();
                        break;
                    case '7':
                        Environment.Exit(0);
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Opção inválida. Pressione qualquer tecla e tente novamente");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
