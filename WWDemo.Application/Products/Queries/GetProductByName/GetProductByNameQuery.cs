using MediatR;
using WWDemo.Application.DTOs;

namespace WWDemo.Application.Products.Queries.GetProductBySerialNumber
{
    public class GetProductByNameQuery : IRequest<ProductRepresentation>
    {
        public string name;

        public string? SerialNumber { get; set; }
    }
}
