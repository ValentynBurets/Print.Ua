using Api.Controllers.Base;
using Api.Models;
using Api.Models.OrderController;
using Api.Models.OrderController.DetailOrderInfoModels;
using Api.Models.OrderController.OrderInfoModels;
using AutoMapper;
using Business.Interface;
using Business.Interface.Services;
using Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UserIdentity.Data;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrdersInfoService _ordersInfoService;
        private readonly IOrderProcessingService _orderProcessingService;

        public OrdersController(IMapper mapper, IOrdersInfoService ordersInfoService, 
            IOrderProcessingService orderProcessingService)
        {
            _mapper = mapper;
            _ordersInfoService = ordersInfoService;
            _orderProcessingService = orderProcessingService;
        }

        // info-------------------
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAll()
        {
            var orders = await _ordersInfoService.GetAll<EmployeeRoleOrderInfoModel>();

            return Ok(orders);
        }

        [HttpGet]
        [Route("my")]
        [Authorize(Roles = "Customer, Employee")]
        public async Task<IActionResult> GetMy()
        {
            if (User.IsInRole("Customer"))
            {
                var userId = GetUserId();
                var orders = await _ordersInfoService.GetMy<CustomerRoleOrderInfoModel>(userId, "Customer");
                return Ok(orders);
            }
            else if (User.IsInRole("Employee"))
            {
                var userId = GetUserId();
                var orders = await _ordersInfoService.GetMy<EmployeeRoleOrderInfoModel>(userId, "Employee");
                return Ok(orders);
            }

            return Unauthorized("Incorrect role error.");
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize(Roles = "Customer, Employee")]
        public async Task<IActionResult> GetByIdDetail([FromRoute] Guid id)
        {
            string notFoundMessage = "Incorrect order id.";

            if (User.IsInRole("Customer"))
            {
                var userId = GetUserId();
                var order = await _ordersInfoService.GetById<CustomerRoleDetailOrderInfoModel>(userId, "Customer", id);
                
                if (order == null)
                {
                    return NotFound(notFoundMessage);
                }

                return Ok(order);
            }
            else if (User.IsInRole("Employee"))
            {
                var userId = GetUserId();
                var order = await _ordersInfoService.GetById<EmployeeRoleDetailOrderInfoModel>(userId, "Employee", id);

                if (order == null)
                {
                    return NotFound(notFoundMessage);
                }

                return Ok(order);
            }

            return Unauthorized("Incorrect role error.");
        }

        //processing---------------
        [HttpPost]
        [Route("create")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create([FromBody] CreateOrderModel orderData)
        {
            if (ModelState.IsValid)
            {
                ICollection<Product> products = new List<Product>();
                products = _mapper.Map<ICollection<CreateProductModel>, ICollection<Product>>(orderData.Products);
                
                ICollection<OrderComment> comments = new List<OrderComment>();
                comments = _mapper.Map<ICollection<CreateCommentViewModel>, ICollection<OrderComment>>(orderData.Comments);

                try
                {
                    await _orderProcessingService.CreateOrder(GetUserId(), products, comments);
                    return Ok("Created!");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.ToString());
                }
            }

            return BadRequest("Invalid data");
        }

        [HttpPut]
        [Route("edit/{id}/products")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> EditProducts([FromRoute] Guid id, [FromBody] ICollection<CreateProductModel> newProducts)
        {
            var products = _mapper.Map<ICollection<CreateProductModel>, ICollection<Product>>(newProducts);

            bool isSuccess = await _orderProcessingService.SetOrderProducts(id, products);

            if (isSuccess == true)
            {
                return Accepted("Сhanges applied!");
            }

            return StatusCode(500, "Edit error!");
        }

        //possibly useless
        [HttpPost]
        [Route("[action]")]
        [Authorize(Roles = "Customer")]
        public IActionResult UploadPhoto([FromBody] UploadPhotoModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _orderProcessingService.UploadPhoto(GetUserId(), model.productId, model.base64String);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("startWorking/{id}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> StartWorkingById([FromRoute] Guid id)
        {
            string notFoundMessage = "Incorrect order id.";

            var empId = GetUserId();

            var order = await _orderProcessingService.StartWorkingWithOrder<EmployeeRoleDetailOrderInfoModel>(id, empId);

            if (order == null)
            {
                return NotFound(notFoundMessage);
            }

            return Ok(order);
        }

        [HttpPut]
        [Route("edit/{id}/ttn/{newTTNstr}")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> EditTTN([FromRoute] Guid id, [FromRoute] string newTTNstr)
        {
            long newTTN = 0;

            try
            {
                newTTN = Convert.ToInt64(newTTNstr);
            }
            catch
            {
                return BadRequest("TTN wrong format (example: 20101030304040)");
            }

            try
            {
                await _orderProcessingService.SetOrderTTN(id, newTTN);
                return Accepted("Сhanges applied!");
            }
            catch
            {
                return StatusCode(500, "Edit error!");
            }
        }

        [HttpPut]
        [Route("cancel/{id}")]
        [Authorize(Roles = "Customer, Employee")]
        public async Task<IActionResult> CancelOrder([FromRoute] Guid id)
        {
            try
            {
                await _orderProcessingService.CancelOrder(id, GetUserId());
                return Accepted("Order cancelled");
            }
            catch
            {
                return StatusCode(500, "Error, order was not cancelled");
            }
        }

        // possibly useless
        [HttpGet]
        [Route("download/{id}")]
        [Authorize(Roles = "Customer, Employee")]
        public async Task<IActionResult> DownloadImages([FromRoute] Guid id)
        {
            var products = await _orderProcessingService.GetProducts(id);

            List<ProductInfoModel> productsDTO = new List<ProductInfoModel>();

            foreach (var product in products)
            {
                productsDTO.Add(new ProductInfoModel
                {
                    Id = product.Id,
                    Amount = product.Amount,
                    Picture = product.Picture,
                    Service = _mapper.Map<ServiceInfoModel>(product.ServiceInProduct)
                });
            }

            return Ok(productsDTO);
        }
    }
}
