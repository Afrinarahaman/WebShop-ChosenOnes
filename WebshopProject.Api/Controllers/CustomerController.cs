using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopProject.Api.Authorization;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Helpers;
using WebshopProject.Api.Services;

namespace WebshopProject.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

       

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate(LoginRequest login)
        {
            try
            {
                LoginResponse response = await _customerService.Authenticate(login);

                if (response == null)
                {
                    return Unauthorized();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register([FromBody] RegisterCustomerRequest newCustomer)
        {
            try
            {

                CustomerResponse customer = await _customerService.Register(newCustomer);
                return Ok(customer);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)] // only admins are allowed entry to this endpoint
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<CustomerResponse> customers = await _customerService.GetAll();

                if (customers == null)
                {
                    return Problem("Got no data, not even an empty list, this is unexpected");
                }

                if (customers.Count == 0)
                {
                    return NoContent();
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Customer, Role.Admin)]
        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]

        public async Task<IActionResult> GetById([FromRoute] int customerId)
        {

            try
            {

                CustomerResponse customer = await _customerService.GetById(customerId);

                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);

                CustomerResponse currentCustomer = (CustomerResponse)HttpContext.Items["Customer"];
                if (customerId != currentCustomer.Id && currentCustomer.Role != Role.Admin)
                {
                    return Unauthorized(new { message = "Unauthorized" });
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            // only admins can access other customer records


        }


        [Authorize(Role.Customer, Role.Admin)]
        [HttpPut("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute] int customerId, [FromBody] RegisterCustomerRequest updateCustomer)
        {
            try
            {
                CustomerResponse customer = await _customerService.Update(customerId, updateCustomer);

                if (customer == null)
                {
                    return Problem("Customer was not updated, something went wrong");
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [Authorize(Role.Admin)]
        [HttpDelete("{customerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute] int customerId)
        {

            try
            {
                CustomerResponse result = await _customerService.Delete(customerId);

                if (result == null)
                {
                    return NotFound();// Problem("Customer was not deleted, something went wrong");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}
