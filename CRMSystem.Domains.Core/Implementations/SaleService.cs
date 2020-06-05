using System;
using System.Collections.Generic;
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

        public SaleService(IRepo<Sale> repo,  IInvoiceService inService, ICartService cService, IRepo<Payment> pRepo, IPaymentRepo payRepo, IRepo<Customer> custRepo, ISaleRepo sRepo)
        {
            _repo = repo;
            _cService = cService;
            _inService = inService;
            _pRepo=pRepo;
            _payRepo = payRepo;
            _custRepo = custRepo;
            _sRepo = sRepo;
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

            // save payment if payment is available
            decimal totalAmt = 0;
            var invIsPaid = false;

            if (data.Payment != null)
            {
                foreach (var payment in data.Payment)
                {
                    payment.CustomerID = data.CustomerID;
                    payment.DatePaid = DateTime.Now;
                    payment.InvoiceNo = data.Invoice.InvoiceNo;
                    var PID = await _pRepo.insertAsync(payment);
                    totalAmt += payment.Amount;

                    // Change payment status to true if payment amount equals cart amount
                    if (payment.Amount == data.Cart.Amount)
                        invIsPaid = true;
                }

            }


            var invoice = new Invoice
            { 
                CustomerID=data.CustomerID,
                DateCreated=data.DateCreated,
                InvoiceDate=data.DateCreated,
                InvoiceNo= invNo,
                CartID = CID,
                Amount=data.Cart.Amount,
                AmountPaid=totalAmt,
                Balance=data.Cart.Amount-totalAmt,
                IsPaid=invIsPaid

            };

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
            data.InvoiceID = IID;
            var SID = await _repo.insertAsync(data);






            


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
        

    }
}
