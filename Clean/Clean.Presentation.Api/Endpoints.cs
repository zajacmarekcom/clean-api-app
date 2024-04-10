using System.Security.Claims;
using Clean.Application.Commands;
using Clean.Application.Queries;
using Clean.Presentation.Api.Requests;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Presentation.Api;

public static class Endpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api");
        group.RequireAuthorization();

        group.MapGet("/invoices",
            async ([FromServices] IMediator mediator, [FromServices] UserManager<IdentityUser> userManager,
                    ClaimsPrincipal user) =>
                await mediator.Send(
                    new GetAllInvoicesForUserQuery((await userManager.FindByNameAsync(user.Identity!.Name!))!.Id)));
        group.MapGet("/invoices/{id}",
            async ([FromServices] IMediator mediator, [FromRoute] string id) =>
            await mediator.Send(new GetInvoiceDetailsQuery(id)));
        group.MapGet("/{userId}/invoices", async ([FromServices] IMediator mediator, string userId) =>
            await mediator.Send(new GetAllInvoicesForUserQuery(userId)))
            .RequireAuthorization("Admin");
        group.MapPost("/invoices",
            async ([FromServices] IMediator mediator, AddInvoiceRequest request,
                    [FromServices] UserManager<IdentityUser> userManager, ClaimsPrincipal user) =>
                await mediator.Send(new AddInvoiceCommand((await userManager.FindByNameAsync(user.Identity!.Name!))!.Id,
                    request.InvoiceDate, request.Customer, request.Items)));

        group.MapGet("/customers", async ([FromServices] IMediator mediator) =>
        await mediator.Send(new GetAllCustomersQuery()));

        return app;
    }
}