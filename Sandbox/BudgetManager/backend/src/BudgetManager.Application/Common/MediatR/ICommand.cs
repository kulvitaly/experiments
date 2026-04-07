using MediatR;

namespace BudgetManager.Application.Common.MediatR;

/// <summary>Command marker — use for write operations that return a result.</summary>
public interface ICommand<out TResponse> : IRequest<TResponse> { }

/// <summary>Command marker — use for write operations that return Unit (void).</summary>
public interface ICommand : ICommand<Unit> { }
