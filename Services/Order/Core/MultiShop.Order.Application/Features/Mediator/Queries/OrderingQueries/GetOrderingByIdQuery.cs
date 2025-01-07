using MediatR;
using MultiShop.Order.Application.Features.Mediator.Results.OrderingResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Mediator'u CQRS'ten ayıran özellik Queries'tir
namespace MultiShop.Order.Application.Features.Mediator.Queries.OrderingQueries
{
	public class GetOrderingByIdQuery : IRequest<GetOrderingByIdQueryResult> // MediatR içinde yer alan bir interface
	{
        public int Id { get; set; }

		public GetOrderingByIdQuery(int id)
		{
			Id = id;
		}
	}
}
