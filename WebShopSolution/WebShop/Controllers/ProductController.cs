using Microsoft.AspNetCore.Mvc;
using WebShop.UnitOfWork;

namespace WebShop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            var products = _unitOfWork.Products.GetAll();
            return Ok(products);
        }

        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            _unitOfWork.Products.Add(product);
            _unitOfWork.NotifyProductAdded(product);
            return Ok();
        }
    }
}
