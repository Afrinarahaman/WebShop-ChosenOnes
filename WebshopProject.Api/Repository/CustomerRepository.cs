using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopProject.Api.Database;
using WebshopProject.Api.Database.Entities;

namespace WebshopProject.Api.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAll();
        Task<Customer> Create(Customer customer);
        Task<Customer> GetByEmail(string email);
        Task<Customer> GetById(int customerId);
        Task<Customer> Update(int customerId, Customer customer);
        Task<Customer> Delete(int customerId);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly WebshopProjectContext _context;

        public CustomerRepository(WebshopProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetAll()
        {
            return await _context.Customer.ToListAsync();
           
        }

        public async Task<Customer> Create(Customer customer)
        {
            

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> GetById(int customerId)
        {
            return await _context.Customer.FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public async Task<Customer> GetByEmail(string Email)
        {
            return await _context.Customer.FirstOrDefaultAsync(c => c.Email == Email);
        }


        public async Task<Customer> Update(int customerId, Customer customer)
        {
            Customer updateCustomer = await _context.Customer
                .FirstOrDefaultAsync(a => a.Id == customerId);

            if (updateCustomer != null)
            {
                updateCustomer.Email = customer.Email;
                updateCustomer.FirstName = customer.FirstName;
                updateCustomer.LastName = customer.LastName;
                updateCustomer.Address = customer.Address;
                updateCustomer.Telephone = customer.Telephone;
                updateCustomer.Password = customer.Password;
                updateCustomer.Role = customer.Role;
                await _context.SaveChangesAsync();
            }
            return updateCustomer;
        }

        public async Task<Customer> Delete(int customerId)
        {
            Customer deletecustomer = await _context.Customer
                .FirstOrDefaultAsync(c => c.Id == customerId);

            if (deletecustomer != null)
            {
                _context.Customer.Remove(deletecustomer);
                await _context.SaveChangesAsync();
            }
            return deletecustomer;
        }
    }
}
