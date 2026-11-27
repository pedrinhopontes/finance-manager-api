using FinanceManager.Domain.Exceptions;

namespace FinanceManager.API.Middleware;

/// <summary>
/// Traduz exceções em respostas ProblemDetails com o status HTTP correto.
/// Erros previstos (<see cref="DomainException"/>) devolvem a mensagem ao
/// cliente; qualquer outro vira 500 genérico e só aparece no log, para não
/// vazar stack trace nem detalhe interno.
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleAsync(context, ex);
        }
    }

    private async Task HandleAsync(HttpContext context, Exception ex)
    {
        var (status, title, detail) = ex switch
        {
            NotFoundException => (
                StatusCodes.Status404NotFound,
                "Recurso não encontrado",
                ex.Message),

            DomainValidationException => (
                StatusCodes.Status400BadRequest,
                "Requisição inválida",
                ex.Message),

            UnauthorizedAccessException => (
                StatusCodes.Status401Unauthorized,
                "Não autorizado",
                "Token ausente ou inválido."),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Erro interno",
                "Ocorreu um erro inesperado ao processar a requisição.")
        };

        if (status == StatusCodes.Status500InternalServerError)
        {
            _logger.LogError(ex, "Erro não tratado em {Method} {Path}",
                context.Request.Method, context.Request.Path);
        }
        else
        {
            _logger.LogWarning("{Exception} em {Method} {Path}: {Message}",
                ex.GetType().Name, context.Request.Method, context.Request.Path, ex.Message);
        }

        // Se a resposta já começou a ser enviada não há o que reescrever.
        if (context.Response.HasStarted)
            return;

        context.Response.Clear();

        await Results
            .Problem(
                title: title,
                detail: detail,
                statusCode: status,
                instance: context.Request.Path)
            .ExecuteAsync(context);
    }
}
