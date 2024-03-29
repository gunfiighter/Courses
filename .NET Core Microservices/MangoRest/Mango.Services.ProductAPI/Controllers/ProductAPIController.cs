﻿using Mango.Services.ProductAPI.Models.DTO;
using Mango.Services.ProductAPI.Models.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        protected ResponseDTO _response;
        private IProductRepository _productRepository;


        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            this._response = new ResponseDTO();
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<ProductDTO> productDTOs = await _productRepository.GetProducts();
                _response.Result = productDTOs;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                ProductDTO productDTO = await _productRepository.GetProductById(id);
                _response.Result = productDTO;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [Authorize]
        public async Task<object> Post([FromBody] ProductDTO productDTO)
        {
            try
            {
                ProductDTO prod = await _productRepository.CreateUpdateProduct(productDTO);
                _response.Result = prod;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut]
        [Authorize]
        public async Task<object> Put([FromBody] ProductDTO productDTO)
        {
            try
            {
                ProductDTO prod = await _productRepository.CreateUpdateProduct(productDTO);
                _response.Result = prod;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _productRepository.DeleteProduct(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }
    }
}
