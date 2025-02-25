using MediatR;
using WWDemo.Application.DTOs;
using WWDemo.Data.Products;

namespace WWDemo.Application.Products.Queries.GetProductBySerialNumber
{
    public class GetProductByNameHandler : IRequestHandler<GetProductBySerialNumberQuery, ProductRepresentation>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByNameHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductRepresentation> Handle(GetProductBySerialNumberQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductBySerialNumber(request.SerialNumber);
            return new ProductRepresentation
            {
                Name = product.Name,
                Price = product.Price,
                SerialNumber = product.SerialNumber,
                Category = product.Category
            };
        }
    }
}
