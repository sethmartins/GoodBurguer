

using GoodBurger.Application.Abstractions;

namespace GoodBurger.Application.Contracts.Responses;

public record ErrorResponse : IResponse
{
    public ErrorResponse(string type, object? correlationId, string message)
    {
        Type = type;
        CorrelationId = correlationId;
        Message = message;
    }
    public string Type { get; init; }
    public object? CorrelationId { get; init; }
    public string Message { get; init; }
}
