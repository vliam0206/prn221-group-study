//using Domain.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Infrastructure.DAOs;

//public class AccountDAO
//{
//    #region Singleton pattern
//    private static AccountDAO instance = null;
//    private static readonly object instanceLock = new object();
//    private AccountDAO() { }
//    public static AccountDAO Instance
//    {
//        get
//        {
//            lock (instanceLock)
//            {
//                if (instance == null)
//                {
//                    instance = new AccountDAO();
//                }
//                return instance;
//            }
//        }
//    }
//    #endregion
//    public IEnumerable<Account> GetAccountList()
//    {
//        var dbcontext = new App();
//        return dbcontext.Customers.ToList();
//    }
//    public Customer GetCustomerById(int id)
//    {
//        var dbcontext = new FUFlowerBouquetManagementContext();
//        var cus = dbcontext.Customers.SingleOrDefault(c => c.CustomerId == id);
//        return cus;
//    }
//    public IEnumerable<Customer> GetCustomerByName(string name)
//    {
//        var dbcontext = new FUFlowerBouquetManagementContext();
//        return dbcontext.Customers.Where(c => c.CustomerName.Contains(name)).ToList();
//    }
//    public void AddCustomer(Customer customer)
//    {
//        var eCustomer = GetCustomerById(customer.CustomerId);
//        if (eCustomer == null)
//        {
//            var dbcontext = new FUFlowerBouquetManagementContext();
//            dbcontext.Customers.Add(customer);
//            dbcontext.SaveChanges();
//        }
//        else
//        {
//            throw new Exception("The customer is already existed. (ID duplicated)");
//        }
//    }
//    public void UpdateCustomer(Customer customer)
//    {
//        var eCustomer = GetCustomerById(customer.CustomerId);
//        if (eCustomer != null)
//        {
//            var cus = GetCustomerByEmail(customer.Email);
//            if (cus != null && cus.CustomerId != customer.CustomerId)
//            {
//                throw new Exception("The email address is already registered to another account.");
//            }
//            var dbcontext = new FUFlowerBouquetManagementContext();
//            dbcontext.Entry<Customer>(customer).State = EntityState.Modified;
//            dbcontext.SaveChanges();
//        }
//        else
//        {
//            throw new Exception("The customer is not exist.");
//        }
//    }
//    public void DeleteCustomer(Customer customer)
//    {
//        var eCustomer = GetCustomerById(customer.CustomerId);
//        if (eCustomer != null)
//        {
//            var dbcontext = new FUFlowerBouquetManagementContext();
//            dbcontext.Customers.Remove(customer);
//            dbcontext.SaveChanges();
//        }
//        else
//        {
//            throw new Exception("The customer is not exist.");
//        }
//    }

//    public Customer GetCustomerByEmail(string email)
//    {
//        var dbcontext = new FUFlowerBouquetManagementContext();
//        return dbcontext.Customers.FirstOrDefault(c => c.Email.Equals(email));
//    }

//}
