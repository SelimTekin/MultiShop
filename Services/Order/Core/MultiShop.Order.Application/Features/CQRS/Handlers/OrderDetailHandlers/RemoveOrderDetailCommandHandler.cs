using MultiShop.Order.Application.Features.CQRS.Commands.OrderDetailCommands;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class RemoveOrderDetailCommandHandler
    {
        private readonly IRepository<OrderDetail> _respository;

        public RemoveOrderDetailCommandHandler(IRepository<OrderDetail> respository)
        {
            _respository = respository;
        }

        public async Task Handle(RemoveOrderDetailCommand command)
        {
            var value = await _respository.GetByIdAsync(command.Id);
            await _respository.DeleteAsync(value);
        }
    }
}
