using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains 
{ 
    public class SaleService:ISaleService
    {
        private readonly IRepo<Sale> _repo;
        private readonly ICartService _cService;
        private readonly IInvoiceService _inService;
        private readonly IRepo<Payment> _pRepo;
        private readonly IPaymentRepo _payRepo;
        private readonly ISaleRepo _sRepo;
        private readonly IRepo<Customer> _custRepo;
        private readonly IWaybillService _wService;
        private readonly IRepo<Product> _proRepo;
        private readonly IRepo<Item> _iRepo;
        private readonly IRepo<Cart> _cRepo;
        private readonly IReturnedStockRepo _rRepo;


        public SaleService(IRepo<Sale> repo,  IInvoiceService inService, ICartService cService, IRepo<Payment> pRepo, 
            IPaymentRepo payRepo, IRepo<Customer> custRepo, ISaleRepo sRepo, IWaybillService wService, IRepo<Product> proRepo, IRepo<Item> iRepo,
            IRepo<Cart> cRepo, IReturnedStockRepo rRepo)
        {
            _repo = repo;
            _cService = cService;
            _inService = inService;
            _pRepo=pRepo;
            _payRepo = payRepo;
            _custRepo = custRepo;
            _sRepo = sRepo;
            _wService = wService;
            _proRepo = proRepo;
            _iRepo = iRepo;
            _cRepo = cRepo;
            _rRepo = rRepo;
        }
        public async Task<int> Save(Sale data)
        {

            


            // save cart

            var CID = await _cService.SaveCart(data.Cart);

            



            // get last Invoice ID to generate invoice number

            var LIID = await _inService.getLastAsync();
            string invNo = "";

            if (LIID == 0)
            {
                data.Invoice.InvoiceNo = "0000001";
                invNo = data.Invoice.InvoiceNo;
            }
            else 
            {
                LIID += 1;
                data.Invoice.InvoiceNo = LIID.ToString();
                invNo = data.Invoice.InvoiceNo.PadLeft(7, '0');
                
                

            }



            var invoice = new Invoice
            {
                Amount = data.Cart.Amount - (data.Cart.Amount * (data.Invoice.DiscountPercent / 100))
             };

            //  add delivery Fee to amount if delivery is checked

            if (data.ToDeliver)
            {
                invoice.DeliveryFee = data.DeliveryFee;
                invoice.Amount += data.DeliveryFee;
                
            }

            invoice.Balance += invoice.Amount;

            // save payment if payment is available
            decimal totalAmt = 0;
            var invIsPaid = false;

            if (data.Payment != null)
            {
                foreach (var payment in data.Payment)
                {
                    payment.CustomerID = data.CustomerID;
                    payment.DatePaid = DateTime.Now;
                    payment.InvoiceNo = invNo;
                    var PID = await _pRepo.insertAsync(payment);
                    totalAmt += payment.Amount;

                    // Change payment status to true if payment amount equals cart amount
                    if (payment.Amount == invoice.Amount)
                        invIsPaid = true;
                }

            }


            invoice.CustomerID = data.CustomerID;
            invoice.DateCreated = data.DateCreated;
            invoice.InvoiceDate = data.DateCreated;
            invoice.InvoiceNo = invNo;
            invoice.CartID = CID;
            invoice.DiscountPercent = data.Invoice.DiscountPercent;
            invoice.Type = "sale";
            invoice.UserCreated = data.UserCreated;
            invoice.AmountPaid = totalAmt;
            invoice.Balance -= totalAmt;
            invoice.IsPaid = invIsPaid;


            // save invoice


            var IID = await _inService.SaveInvoice(invoice);



            



            // update customer 

            var customer = new Customer
            {
                ID=data.CustomerID,
                TotalSales=1
            };

            await _custRepo.updateAsync(customer);

            // save sale

            data.CartID = CID;
            // data.CartID = CID;
            //was included initially  data.InvoiceID = IID;
            data.InvoiceID = IID;

            

            var SID = await _repo.insertAsync(data);


            // save waybill

            var waybill = new Waybill
            {
                DateCreated = DateTime.Now,
                InvoiceNo = invNo,
                UserCreated = data.UserCreated,
                customerID=data.CustomerID
               
            };




            

            var waybillProds = new List<WaybillProduct>();

            // get productID from the cart items

            foreach(var item in data.Cart.Items)
            {
                var prod = new WaybillProduct();
                prod.ProductID = item.ProductID;
                prod.Quantity = item.Quantity;
                waybillProds.Add(prod);

            }

            // add products to waybill
            waybill.WaybillProducts = waybillProds;

            // Save both waybill with the listed products
            var WID = await _wService.SaveWaybillAndProducts(waybill);








            return SID;
        }
        //public async Task<List<Sale>> GetSalesByCustomerIDAsync(int customerID)
        //{
        //    var sales = await _repo.getByCustomerIDAsync(customerID);

        //    foreach (var sale in sales)
        //    {
        //        sale.Payment = await _payRepo.getPaymentByInvoiceNo(sale.Invoice.InvoiceNo);
        //    }
            
        //    return sales;
        //} b4 deployemnt
        public async Task<List<Sale>> GetSalesByCustomerIDAsync(int customerID)
        {
            var sales = await _sRepo.getByCustomerIDAsync(customerID);

            foreach (var sale in sales)
            {
                sale.Payment = await _payRepo.getPaymentByInvoiceNo(sale.Invoice.InvoiceNo);
            }

            return sales;
        }

        public async Task<Sale> GetSaleByIDAsync(int ID)
        {
            var sale = await _repo.getAsync(ID);
            sale.Payment = await _payRepo.getPaymentByInvoiceNo(sale.Invoice.InvoiceNo);
            return sale;
        }

       // public async Task<Sale> GetSale

        public async Task<Sale> GetSaleWithPaymentsByIDAsync(string invNo)
        {
            // get invoice
            var invoice = await _inService.GetInvoiceByinvNo(invNo);

            // get sale with invoiceID

            var sale = await _repo.getAsync(invoice.ID);

            // get returnedstock

            var stock = await _rRepo.getAsync(invNo);
            // get paymwnt with invNo

            var payments = await _payRepo.getAllByInvAsync(invNo);

            sale.ReturnedStock = stock;
            sale.Payment = payments;

            return sale;
        }
        public async Task<List<Sale>> GetAllSalesAsync()
        {
            var sales = await _repo.getAllAsync();

            foreach (var sale in sales)
            {
                
                sale.Payment= await _payRepo.getPaymentByInvoiceNo(sale.Invoice.InvoiceNo);
            
            }


            return sales;
        }
      

        public async Task<List<Sale>> getSaleHistoryByDateAsync(DateTime startdate, DateTime enddate)
        {
            var sales = await _sRepo.getSaleHistoryByDate(startdate, enddate);
            return sales;
        
        
        }
        public async Task<List<Sale>> GetSingleDaySalesAsync(DateTime date)
        {
            var sales = await _sRepo.GetSingleDaySales(date);
            return sales;


        }

        public async Task<List<Sale>> GetSalesReportByDate(int customerID, string startDate,string endDate)
        {

            List<Sale> sales;
            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

            if (sdate <= DateTime.MinValue)
                sdate = DateTime.Now.StartOfDay();

            if (edate <= DateTime.MinValue)
                edate = DateTime.Now.EndOfDay();
            else
                edate = edate.EndOfDay();

            // filter by customerID only, if that's what was given

            if ((startDate == "0" || endDate == "0") && customerID > 0)
            {
                return sales = await _sRepo.getByCustomerIDAsync(customerID);
            }






            // filter by dates alone if customerID is not given

            else if (customerID < 1 && (startDate != "0" || endDate != "0"))
            {
                return sales = await _sRepo.getSaleHistoryByDate(sdate, edate);
            }




            // filter by all given parameters

            else if (startDate != "0" && endDate != "0" && customerID > 0)
            {
                return sales = await _sRepo.getByCustomerIDandDateAsync(customerID, sdate, edate);
            }



            // get without any parameter
            else
                return sales = await _sRepo.getAllSalesAsync();



           


        }

        public async Task<List<Waybill>> GetWaybillByDate(string startDate, string endDate)
        {
            var waybills = await _wService.GetAllWaybillByDates(startDate, endDate);
            return waybills;
        }

        public async Task DeleteSale(string invNo)
        {
            var invoice = await _inService.GetInvoiceByinvNo(invNo);

            // use invoiceID to get the sale

            // soft delete invoice

            await _inService.deleteInvoice(invoice.ID);

            // soft delete the sale
            await _repo.deleteAsync(invoice.ID);

        
        }
        
        public async Task<int> CreateRefund(ReturnedStock data)
        {
            // get invoice
            var invoice = await _inService.GetInvoiceByinvNo(data.InvoiceNo);

            //use invoiceID to get sale

            var sale = await _sRepo.getByInvIDAsync(invoice.ID);

            var CID = await _cRepo.insertAsync(data.Cart);


            List<Item> items = new List<Item>();

            decimal refundAmount = 0;

            decimal itemAmount = 0;

            foreach (var item in data.Cart.Items)
            {


                // get product by productID

                var product = await _proRepo.getAsync(item.ProductID);

                item.CartID = CID;

                item.Amount = product.SalePrice * item.Quantity; // to get the total amount of product returnedd

                

                if (invoice.DiscountPercent>0.00M)
                {
                    item.Amount = item.Amount-( item.Amount * (invoice.DiscountPercent/100));
                    itemAmount = item.Amount;
                    refundAmount = refundAmount + itemAmount;
                }
                else
                {
                    itemAmount = item.Amount;
                    refundAmount += itemAmount;
                }

                item.Name = product.Name;


                //refundAmount += item.Amount;
                //item.Name = product.Name;  was working initially


                // Reduce product in cart with the returned items(I think it's not needed to reduce the qty in the cart,
                // since the refunded items will be vailable in refundStock)




                // increase product by the amount returned and update

                product.Quantity += item.Quantity;
                product.TotalSold -= item.Quantity;

                await _proRepo.updateAsync(product);

                items.Add(item);


                // update existing cart items


                
                foreach (var exitem in invoice.Cart.Items)
                {

                    
                    if (exitem.ProductID==item.ProductID)
                    {
                        exitem.Quantity -= item.Quantity;
                        decimal amt = exitem.Quantity * product.SalePrice;
                        if (invoice.DiscountPercent>0.00M)
                        {
                            exitem.Amount = amt - (amt * (invoice.DiscountPercent / 100));
                        }
                        else
                        {
                            exitem.Amount = amt;
                        }
                        

                        await _iRepo.updateAsync(exitem);
                    }
                }

                
                

            }

            // save the returned items 
            await _iRepo.insertListAsync(items);

            // update cart to add total amount

            
            data.RefundAmount= refundAmount;
            data.Cart.ID = CID;
            await _cRepo.updateAsync(data.Cart);
            


            //save refunded items
            data.CartID = CID;
            var RID = await _rRepo.insertAsync(data);


            // update invoice

            invoice.Amount -= refundAmount;
            invoice.Balance -= refundAmount;

            if (invoice.IsPaid)
                invoice.AmountPaid -= refundAmount;

            invoice.Cart.Amount = invoice.Amount;
            await _cRepo.updateAsync(invoice.Cart);

            await _inService.updateAsync(invoice);

            return RID;


        }
    }
}
