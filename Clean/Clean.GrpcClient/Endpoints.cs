using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clean.GrpcClient;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api");
        group.RequireAuthorization();

        group.MapGet("/{userId}/invoices",
            async ([FromRoute] string userId) =>
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7002");
                var client = new Invoice.InvoiceClient(channel);
                var response = await client.GetAllInvoicesAsync(new AllInvoicesRequest() { UserId = userId });

                return response;
            });

        group.MapGet("/invoices",
            async ([FromServices] UserManager<IdentityUser> userManager, HttpContext context) =>
            {
                var userId = userManager.FindByNameAsync(context.User.Identity.Name).Result.Id;
                var channel = GrpcChannel.ForAddress("https://localhost:7002");
                var client = new Invoice.InvoiceClient(channel);
                var response = await client.GetAllInvoicesAsync(new AllInvoicesRequest() { UserId = userId });

                return response;
            });


        group.MapGet("/invoices/{id}",
            async ([FromRoute] string id) =>
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7002");
                var client = new Invoice.InvoiceClient(channel);
                var response = await client.GetInvoiceDetailsAsync(new InvoiceDetailsRequest { InvoiceId = id });

                return response;
            });

        group.MapPost("/invoices",
            async (AddInvoiceRequest request) =>
            {
                var channel = GrpcChannel.ForAddress("https://localhost:7002");
                var client = new Invoice.InvoiceClient(channel);
                var response = await client.CreateInvoiceAsync(new CreateInvoiceRequest()
                {
                    UserId = request.UserId,
                    InvoiceDate = request.InvoiceDate.ToTimestamp(),
                    Customer = new CustomerRequest()
                    {
                        Name = request.Customer.Name, TaxNumber = request.Customer.TaxNumber,
                        Email = request.Customer.Email, Phone = request.Customer.Phone,
                        Address = request.Customer.Address
                    },
                    Items = { request.Items.Select(x => new InvoiceItemRequest()
                    {
                        ItemName = x.Name,
                        Price = (double)x.Price,
                        Quantity = x.Quantity
                    }) }
                });

                return response;
            });

        group.MapGet("/customers", async () =>
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7002");
            var client = new Invoice.InvoiceClient(channel);
            var response = await client.GetCustomersAsync(new empty());

            return response;
        });

        return app;
    }
}