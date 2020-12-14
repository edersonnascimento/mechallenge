using MEChallenge.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MEChallenge.Data.Repositories
{
    public abstract class ChallengeAbstractRepository 
    {
        protected readonly ChallengeContext _context;
        public ChallengeAbstractRepository(ChallengeContext context) => _context = context;
    }
}
