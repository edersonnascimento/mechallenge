﻿using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MEChallenge.Data.Repositories
{
    public class OrderRepository : ChallengeAbstractRepository, IRepository<Order, string>
    {
        public OrderRepository(ChallengeContext context) : base(context) { }

        public async Task<Order> GetById(string id) => await _context.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id);
        public async Task<IEnumerable<Order>> GetAll() => await _context.Orders.Include(o => o.Items).ToListAsync();
        public async Task<IEnumerable<Order>> Find(Expression<Func<Order, bool>> filter) => await _context.Orders.Where(filter).ToListAsync();
        public async Task Save(Order entity)
        {
            var order = await GetById(entity.Id);
            if(order == null) {
                _context.Orders.Add(entity);
                return;
            }

            order.ApprovedItens = entity.ApprovedItens;
            order.ApprovedValue = entity.ApprovedValue;
            _context.Orders.Update(order);
        }
        public async Task Delete(string id)
        {
            var order = await GetById(id);
            if(order != null) {
                _context.Orders.Remove(order);
            }
        }
    }
}
