using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<T> CreateProductAsync<T>(ProductDTO productDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.APIType.POST,
                Data = productDTO,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProductAsync<T>(int productId)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.APIType.DELETE,
                Url = SD.ProductAPIBase + $"/api/products/{productId}",
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.APIType.GET,
                Url = SD.ProductAPIBase + $"/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductAsync<T>(int productId)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.APIType.GET,
                Url = SD.ProductAPIBase + $"/api/products/{productId}",
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProductAsync<T>(ProductDTO productDTO)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.APIType.POST,
                Data = productDTO,
                Url = SD.ProductAPIBase + $"/api/products",
                AccessToken = ""
            });
        }
    }
}
