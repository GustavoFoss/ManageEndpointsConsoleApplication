// See https://aka.ms/new-console-template for more information

using manage_endpoints.Model;
using manage_endpoints.Service;
using manage_endpoints.Service.Interface;

class Program
{
    private static IEndpointService _endpointService = new EndpointService();

    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    InsertNewEndpoint();
                    break;
                case "2":
                    EditExistingEndpoint();
                    break;
                case "3":
                    DeleteExistingEndpoint();
                    break;
                case "4":
                    ListAllEndpoints();
                    break;
                case "5":
                    FindEndpointBySerialNumber();
                    break;
                case "6":
                    ExitApplication();
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
            
            Thread.Sleep(2000);
            Console.WriteLine("");
        }
    }
    
    static void InsertNewEndpoint()
    {
        try
        {
            Console.Write("Enter Serial Number: ");
            var serialNumber = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(serialNumber))
            {
                throw new ArgumentException("Serial Number cannot be empty.");
            }

            Console.Write("Enter Meter Model Id (16, 17, 18, 19): ");
            if (!int.TryParse(Console.ReadLine(), out int meterModelId) || !IsValidMeterModelId(meterModelId))
            {
                throw new ArgumentException("Invalid Meter Model Id. It must be 16, 17, 18, or 19.");
            }

            Console.Write("Enter Meter Number: ");
            if (!int.TryParse(Console.ReadLine(), out int meterNumber) || meterNumber <= 0)
            {
                throw new ArgumentException("Meter Number must be a positive integer.");
            }

            Console.Write("Enter Firmware Version: ");
            var firmwareVersion = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firmwareVersion))
            {
                throw new ArgumentException("Firmware Version cannot be empty.");
            }

            Console.Write("Enter Switch State (0: Disconnected, 1: Connected, 2: Armed): ");
            if (!int.TryParse(Console.ReadLine(), out int switchState) || !IsValidSwitchState(switchState))
            {
                throw new ArgumentException("Invalid Switch State. It must be 0, 1, or 2.");
            }

            var endpoint = new Endpoint(serialNumber, meterModelId, meterNumber, firmwareVersion, switchState);
            _endpointService.AddEndpoint(endpoint);

            Console.WriteLine("Endpoint added successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static bool IsValidMeterModelId(int meterModelId)
    {
        return meterModelId == 16 || meterModelId == 17 || meterModelId == 18 || meterModelId == 19;
    }

    static bool IsValidSwitchState(int switchState)
    {
        return switchState == 0 || switchState == 1 || switchState == 2;
    }


    static void ShowMenu()
    {
        Console.WriteLine("1) Insert a new endpoint");
        Console.WriteLine("2) Edit an existing endpoint");
        Console.WriteLine("3) Delete an existing endpoint");
        Console.WriteLine("4) List all endpoints");
        Console.WriteLine("5) Find an endpoint by Serial Number");
        Console.WriteLine("6) Exit");
    }
    

    static void EditExistingEndpoint()
    {
        try
        {
            Console.Write("Enter Serial Number: ");
            var serialNumber = Console.ReadLine();

            Console.Write("Enter new Switch State (0: Disconnected, 1: Connected, 2: Armed): ");
            var switchState = int.Parse(Console.ReadLine());

            _endpointService.EditEndpoint(serialNumber, switchState);

            Console.WriteLine("Endpoint updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void DeleteExistingEndpoint()
    {
        try
        {
            Console.Write("Enter Serial Number: ");
            var serialNumber = Console.ReadLine();

            _endpointService.DeleteEndpoint(serialNumber);

            Console.WriteLine("Endpoint deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ListAllEndpoints()
    {
        var endpoints = _endpointService.GetAllEndpoints();

        if (endpoints.Any())
        {
            foreach (var endpoint in endpoints)
            {
                Console.WriteLine(endpoint.ToString());
            }
        }
        else
        {
            Console.WriteLine("No endpoints found.");
        }
    }

    static void FindEndpointBySerialNumber()
    {
        try
        {
            Console.Write("Enter Serial Number: ");
            var serialNumber = Console.ReadLine();

            var endpoint = _endpointService.FindEndpointBySerialNumber(serialNumber);

            if (endpoint != null)
            {
                Console.WriteLine(endpoint.ToString());
            }
            else
            {
                Console.WriteLine("Endpoint not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ExitApplication()
    {
        Console.WriteLine("Are you sure you want to exit? (y/n): ");
        var response = Console.ReadLine();

        if (response?.ToLower() == "y")
        {
            Environment.Exit(0);
        }
    }
}