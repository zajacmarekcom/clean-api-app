using Clean.Application.Commands;
using Clean.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Presentation.Api;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api");

        group.MapGet("/{userId}/invoices", async ([FromServices] IMediator mediator, Guid userId) =>
        await mediator.Send(new GetAllInvoicesForUserQuery(userId)));
        group.MapPost("/invoices", async ([FromServices] IMediator mediator, AddInvoiceCommand command) =>
            await mediator.Send(command));
        
        group.MapGet("/customers", async ([FromServices] IMediator mediator) =>
            await mediator.Send(new GetAllCustomersQuery()));
        
        return app;
    }
}