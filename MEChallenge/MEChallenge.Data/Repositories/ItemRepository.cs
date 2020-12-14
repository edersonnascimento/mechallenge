using MEChallenge.Domain.Interfaces;
using MEChallenge.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MEChallenge.Data.Repositories
{
    public class ItemRepository : ChallengeAbstractRepository, IRepository<Item, string>
    {
        public ItemRepository(ChallengeContext context) : base(context) { }

        public async Task<Item> GetById(string id) => await _context.Items.FirstOrDefaultAsync(i => i.Description == id);
        public async Task<IEnumerable<Item>> GetAll() => await _context.Items.ToListAsync();
        public async Task<IEnumerable<Item>> Find(Expression<Func<Item, bool>> filter) => await _context.Items.Where(filter).ToListAsync();
        public async Task Save(Item entity)
        {
            var item = await GetById(entity.Description);
            if(item == null) {
                _context.Items.Add(entity);
                return;
            }

            item.UnitPrice = entity.UnitPrice;
            item.Quantity = entity.Quantity;
            _context.Items.Update(item);
        }
        public async Task Delete(string id)
        {
            var item = await GetById(id);
            if(item != null) {
                _context.Items.Remove(item);
            }
        }




    }
}
