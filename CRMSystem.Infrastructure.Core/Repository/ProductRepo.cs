﻿using CRMSystem.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Infrastructure
{
    public class ProductRepo : IRepo<Product>, IProductRepo
    {
        private readonly TContext _context;
        public ProductRepo(TContext context)
        {
            _context = context;
        }

        public Task deleteAllAsync(List<Product> data)
        {
            throw new NotImplementedException();
        }

        public async Task  deleteAsync(int ID)
        {
            try
            {
                var product = await _context.Products.FindAsync(ID);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Product>> getAllAsync()
        {
            throw new NotImplementedException();
            

        }

        public async Task<List<Product>> getAllAsync(string type)
        {
            var product = new List<Product>();
            try
            {
                if (type.ToLower() == "namedesc")
                {
                    product = await _context.Products.OrderByDescending(x => x.Name).ToListAsync();
                    return product;
                }
                else if(type.ToLower() == "nameasc")
                {
                    product = await _context.Products.OrderBy(x => x.Name).ToListAsync();
                    return product;
                }
                else if(type.ToLower() == "dateasc")
                {
                    product = await _context.Products.OrderBy(x => x.DateCreated).ToListAsync();
                    return product;
                }
                else
                {
                    product = await _context.Products.OrderByDescending(x => x.DateCreated).ToListAsync();
                    return product;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }


        public async Task<List<Product>> getAllAvailableAsync()
        {

            try
            {
                var product = await _context.Products.Where(x=>x.Quantity > 0).OrderByDescending(x => x.DateCreated).ToListAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        

        public async Task<Product> getAsync(int ID)
        {
            try
            {
                var product = await _context.Products.Where(x => x.ID == ID).FirstOrDefaultAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public Task<List<Product>> getByCustomerIDAsync(int customerID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetTopSellingProducts()
        {
            try
            {
                var products = await _context.Products.OrderByDescending(x => x.TotalSold).ToListAsync();
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> insertAsync(Product data)
        {
            var product = new Product();
            try
            {
                if (data != null)
                {

                    product = new Product
                    {
                        DateCreated = DateTime.Now,
                        UserCreated = data.UserCreated,
                        Image=data.Image,
                        Name=data.Name,
                        Quantity=data.Quantity,
                        SalePrice=data.SalePrice,
                        CostPrice=data.CostPrice,
                        TotalSold=data.TotalSold,
                        Description=data.Description,
                        Location=data.Location
                    };
                    await _context.Products.AddAsync(product);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return product.ID;
        }

        public async Task<int> insertListAsync(List<Product> data)
        {
            try
            {
                 _context.Products.AddRange(data);
                await _context.SaveChangesAsync();
                return data.Count;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> updateAsync(Product data)
        {
            
            var newProduct = await _context.Products.FindAsync(data.ID);
            try
            {
                if (newProduct != null)
                {
                    newProduct.DateModified = DateTime.Now;
                    newProduct.UserModified = data.UserModified;
                    newProduct.Quantity = data.Quantity;
                    newProduct.Name = data.Name;
                    newProduct.Image = data.Image;
                    newProduct.SalePrice = data.SalePrice;
                    newProduct.CostPrice = data.CostPrice;
                    newProduct.TotalSold = data.TotalSold;
                    newProduct.Description = data.Description;
                    newProduct.Location = data.Location;


                    _context.Products.Update(newProduct);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newProduct.ID;
        }
    }
}
