using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;


namespace SportsStore.Domain.Concrete
{
    public class EfProductRepository : IProductRepository
    {
        private EfDbContext Context = new EfDbContext();

        public IQueryable<Product> Products
        {
            get { return Context.Products; }
        }

    }
}
