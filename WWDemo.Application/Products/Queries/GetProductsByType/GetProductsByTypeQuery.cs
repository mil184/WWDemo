using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WWDemo.Application.DTOs;

namespace WWDemo.Application.Products.Queries.GetProductsByType
{
    public class GetProductsByTypeQuery : IRequest<List<DTOs.ProductRepresentation>>
    {
        public string? Type { get; set; }
    }
}
