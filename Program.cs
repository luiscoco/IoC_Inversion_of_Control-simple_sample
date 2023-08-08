using System;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();

        // Container setup (using built-in .NET Core DI container)
        var startup = new Startup();

        // Simulate ConfigureServices being called by the framework
        startup.ConfigureServices(services);

        var serviceProvider = services.BuildServiceProvider();


        var orderProcessor = serviceProvider.GetRequiredService<OrderProcessor>();

        // Use the components
        orderProcessor.ProcessOrder(new Order());
    }
}

// Container setup (using built-in .NET Core DI container)
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IEmailService, EmailService>();
        services.AddTransient<OrderProcessor>();
    }
}

// Define interfaces
public interface IEmailService
{
    void SendEmail(string message);
}

// Concrete email service implementation
public class EmailService : IEmailService
{
    public void SendEmail(string message)
    {
        Console.WriteLine("Email sent: " + message);
    }
}

public class OrderProcessor
{
    private readonly IEmailService _emailService;

    public OrderProcessor(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public void ProcessOrder(Order order)
    {
        // Process order logic
        Console.WriteLine("Order processed");
        _emailService.SendEmail("Order processed successfully.");
    }
}

public class Order { } // Dummy Order class


