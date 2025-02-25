using AutoMapper;
using MediatR;
using WWDemo.Data.Products;
using WWDemo.Models;

namespace WWDemo.Application.Products.Queries.GetProductsByType
{
    public class GetProductsByTypeHandler : IRequestHandler<GetProductsByTypeQuery, List<DTOs.ProductRepresentation>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByTypeHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<DTOs.ProductRepresentation>> Handle(GetProductsByTypeQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetProductsByType(request.Type1);
            var result = _mapper.Map<List<Models.Product>, List<DTOs.ProductRepresentation>>(products);
            return result;
        }
    }
}
