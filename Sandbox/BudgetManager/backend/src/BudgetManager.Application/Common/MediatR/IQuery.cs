using MediatR;

namespace BudgetManager.Application.Common.MediatR;

/// <summary>Query marker — use for read-only operations.</summary>
public interface IQuery<out TResponse> : IRequest<TResponse> { }
