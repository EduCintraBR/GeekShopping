using GeekShopping.ProductApi.Data.ValueObjects;

namespace GeekShopping.ProductApi.Repository.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductVO>> FindAll();
        Task<ProductVO> FindById(long id);
        Task<ProductVO> Create(ProductVO prodVO);
        Task<ProductVO> Update(ProductVO prodVO);
        Task<bool> Delete(long id);
    }
}
