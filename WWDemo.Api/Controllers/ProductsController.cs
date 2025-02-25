using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors;
using WWDemo.Api.Requests;
using WWDemo.Application.DTOs;
using WWDemo.Application.Products.Commands.AddProduct;
using WWDemo.Application.Products.Queries.GetAllProducts;
using WWDemo.Application.Products.Queries.GetProductBySerialNumber;
using WWDemo.Application.Products.Queries.GetProductsByType;

namespace WWDemo.Api.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> AddProduct(AddProductRequest request)
		{
			await _mediator.Send(new AddProductCommand
			{
				Name = request.Name,
				Price = request.Price,
				SerialNumber = request.SerialNumber,
			});

			return Ok();
		}

		[HttpGet]
		[ProducesResponseType(typeof(List<ProductRepresentation>), StatusCodes.Status200OK)]
		public async Task<List<ProductRepresentation>> GetAllProducts()
		{
			var result = await _mediator.Send(new GetAllProductsQuery());

			return result;
		}

		[HttpGet("serial-number")]
		public async Task<IActionResult> GetProductBySerialNumber([FromRoute] string serialNumber)
		{
			var result = await _mediator.Send(new GetProductBySerialNumberQuery() { SerialNumber=serialNumber });

            if (result == null)
            {
                return NotFound("Product not found");
            }

            return Ok();
		}

        [HttpGet("type")]
        [ProducesResponseType(typeof(List<ProductRepresentation>), StatusCodes.Status200OK)]
        public async Task<List<ProductRepresentation>> GetProductsByType([FromRoute] string type)
        {
            var result = await _mediator.Send(new GetProductsByTypeQuery() { Type = type });

            return result;
        }

        [HttpDelete]
		public async Task<IActionResult> DeleteProduct()
		{
			return Ok();
		}
	}
}
