using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Authorization;
using WebshopProject.Api.Database.Entities;
using WebshopProject.Api.DTOs;
using WebshopProject.Api.Repositories;

namespace WebshopProject.Api.Services
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAll();
        Task<CustomerResponse> GetById(int customerId);
        Task<LoginResponse> Authenticate(LoginRequest login);
        Task<CustomerResponse> Register(RegisterCustomerRequest newCustomer);
        Task<CustomerResponse> Update(int customerId, RegisterCustomerRequest updateCustomer);
        Task<CustomerResponse> Delete(int customerId);

    }

    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IJwtUtils _jwtUtils;

        public CustomerService(ICustomerRepository customerRepository, IJwtUtils jwtUtils)
        {
            _customerRepository = customerRepository;
            _jwtUtils = jwtUtils;
        }

        //public CustomerService(ICustomerRepository @object)
        //{
        //    this.@object = @object;
        //}

        public async Task<List<CustomerResponse>> GetAll()
        {
            /**/
            List<Customer> customers = await _customerRepository.GetAll();

            return customers == null ? null : customers.Select(c => new CustomerResponse
            {
                Id = c.Id,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Telephone = c.Telephone,
                Password = c.Password,
                Role = c.Role
            }).ToList();
        }

        public async Task<CustomerResponse> GetById(int customerId)
        {
            Customer customer = await _customerRepository.GetById(customerId);

            if (customer != null)
            {

                return MapCustomerToCustomerResponse(customer);
            }
            return null;
        }





        public async Task<LoginResponse> Authenticate(LoginRequest login)
        {

            Customer customer = await _customerRepository.GetByEmail(login.Email);
            if (customer == null)
            {
                return null;
            }

            if (customer.Password == login.Password)
            {
                LoginResponse response = new LoginResponse
                {
                    Id = customer.Id,
                    Email = customer.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Telephone = customer.Telephone,
                    Password = customer.Password,
                    Role = customer.Role,
                    Token = _jwtUtils.GenerateJwtToken(customer)
                };
                return response;
            }

            return null;
        }

        public async Task<CustomerResponse> Register(RegisterCustomerRequest newCustomer)
        {
            Customer customer = new Customer
            {
                Email = newCustomer.Email,
                FirstName = newCustomer.FirstName,
                LastName = newCustomer.LastName,
                Address = newCustomer.Address,
                Telephone = newCustomer.Telephone,
                Password = newCustomer.Password,
                Role = Helpers.Role.Customer // force all customers created through Register, to Role.Customer
            };

            customer = await _customerRepository.Create(customer);

            return MapCustomerToCustomerResponse(customer);
        }

        public async Task<CustomerResponse> Update(int customerId, RegisterCustomerRequest updateCustomer)
        {
            Customer customer = new Customer
            {
                FirstName = updateCustomer.FirstName,
                LastName = updateCustomer.LastName,
                Address = updateCustomer.Address,
                Telephone = updateCustomer.Telephone,
                Email = updateCustomer.Email,
                Password = updateCustomer.Password,

            };

            customer = await _customerRepository.Update(customerId, customer);

            return customer == null ? null : new CustomerResponse
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Telephone = customer.Telephone,
                Email = customer.Email,
                Password = customer.Password,
                Role = customer.Role
            };
        }

        public async Task<CustomerResponse> Delete(int customerId)

        {
            Customer customer = await _customerRepository.Delete(customerId);

            if (customer != null)
            {
                return MapCustomerToCustomerResponse(customer);
            }

            return null;
        }


        private CustomerResponse MapCustomerToCustomerResponse(Customer customer)
        {
            return customer == null ? null : new CustomerResponse
            {
                Id = customer.Id,
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Address = customer.Address,
                Telephone = customer.Telephone,
                Password = customer.Password,
                Role = customer.Role
            };
        }



    }
}
