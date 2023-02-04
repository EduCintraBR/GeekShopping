using AutoMapper;
using GeekShopping.ProductApi.Data.ValueObjects;
using GeekShopping.ProductApi.Model.Context;
using GeekShopping.ProductApi.Model.EPs;
using GeekShopping.ProductApi.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace GeekShopping.ProductApi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly MySqlContext _contexto;
        private readonly IMapper _mapper;
        public ProductRepository(MySqlContext contexto, IMapper mapper)
        {
            _contexto = contexto;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductVO>> FindAll()
        {
            return _mapper.Map<IEnumerable<ProductVO>>(await _contexto.Products.ToListAsync());
        }

        public async Task<ProductVO> FindById(long id)
        {
            return _mapper.Map<ProductVO>(await _contexto.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new ProductEP());
        }

        public async Task<ProductVO> Create(ProductVO prodVO)
        {
            var product = _mapper.Map<ProductEP>(prodVO);
            _contexto.Products.Add(product);
            await _contexto.SaveChangesAsync();
            
            return prodVO;
        }

        public async Task<ProductVO> Update(ProductVO prodVO)
        {
            var product = _mapper.Map<ProductEP>(prodVO);
            _contexto.Products.Update(product);
            await _contexto.SaveChangesAsync();

            return prodVO;
        }

        public async Task<bool> Delete(long id)
        {
            try
            {
                var product = await _contexto.Products.Where(p => p.Id == id).FirstOrDefaultAsync() ?? new ProductEP();
                if (product.Id <= 0) return false;

                _contexto.Products.Remove(product);
                await _contexto.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
