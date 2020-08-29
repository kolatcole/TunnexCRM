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

        public SaleService(IRepo<Sale> repo,  IInvoiceService inService, ICartService cService, IRepo<Payment> pRepo, 
            IPaymentRepo payRepo, IRepo<Customer> custRepo, ISaleRepo sRepo, IWaybillService wService)
        {
            _repo = repo;
            _cService = cService;
            _inService = inService;
            _pRepo=pRepo;
            _payRepo = payRepo;
            _custRepo = custRepo;
            _sRepo = sRepo;
            _wService = wService;
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
                Amount=data.Cart.Amount-(data.Cart.Amount * (data.Invoice.DiscountPercent / 100)),
                AmountPaid=totalAmt,
                Balance=(data.Cart.Amount - (data.Cart.Amount * (data.Invoice.DiscountPercent / 100))) -totalAmt,
                IsPaid=invIsPaid,
                DiscountPercent=data.Invoice.DiscountPercent,
                Type="sale"

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
            //was included initially  data.InvoiceID = IID;
            data.InvoiceID = IID;
            var SID = await _repo.insertAsync(data);


            // save waybill

            var waybill = new Waybill
            {
                DateCreated = DateTime.Now,
                InvoiceNo = invNo,
                UserCreated = data.UserCreated
               
            };




            

            var waybillProds = new List<WaybillProduct>();

            // get productID from the cart items

            foreach(var item in data.Cart.Items)
            {
                var prod = new WaybillProduct();
                prod.ProductID = item.ProductID;
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
    }
}
