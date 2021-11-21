using System;
using System.Threading.Tasks;
using Api.Controllers.Base;
using Business.Contract.Model;
using Business.Contract.Services.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class ProductServiceController : BaseController
    {
        private readonly IProductServiceService _productServiceService;

        public ProductServiceController(IProductServiceService productService)
        {
            _productServiceService = productService;
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAll()
        {
            var services = await _productServiceService.GetAll();

            return Ok(services);
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllNotCanceled()
        {
            var services = await _productServiceService.GetAllNotCanceled();

            return Ok(services);
        }

        [HttpGet]
        [Route("[action]/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById([FromRoute] Guid id)
        {
            try
            {
                var services = await _productServiceService.GetById(id);
                return Ok(services);
            }
            catch (Exception ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> CreateService([FromBody] CreateServiceViewModel createService)
        {
            try
            {
                await _productServiceService.Create(createService);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPut]
        [Route("[action]/{id}")]
        public async Task<ActionResult> Edit([FromRoute]Guid id, [FromBody] UpdateServiceViewModel updateService)
        {
            try
            {
                await _productServiceService.Update(id, updateService);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpDelete]
        [Route("[action]/{id}")]
        public async Task<ActionResult> Delete([FromRoute]Guid id)
        {
            try
            {
                await _productServiceService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<ActionResult> DisableService([FromRoute] Guid id)
        {
            try
            {
                await _productServiceService.DisableService(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<ActionResult> EnableService([FromRoute] Guid id)
        {
            try
            {
                await _productServiceService.EnableService(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult> GetCreationData()
        {
            var materialsAndFormats = JsonConvert.SerializeObject(await _productServiceService.GetDataForCreation());
            return Ok(materialsAndFormats);
        }
    }
}
