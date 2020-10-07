using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{ 
    public class InvoiceService: IInvoiceService
    {
        private readonly IRepo<Invoice> _inRepo;
        private readonly ICartService _cService;
        private readonly IInvoiceRepo _iRepo;
        public InvoiceService(IRepo<Invoice> inRepo,ICartService cService, IInvoiceRepo iRepo)
        {
            _inRepo = inRepo;
            _cService = cService;
            _iRepo = iRepo;
        }

        public async Task<int> SaveInvoice(Invoice data)
        {
            // PENDING  var CID = await _cRepo.insertAsync(data.Cart);

            // PENDING data.CartID = CID;
            var IID = await _inRepo.insertAsync(data);
            return IID;
        }

        public async Task<int> SaveProformaInvoice(Invoice data)
        {
            var CID = await _cService.SaveProformaCart(data.Cart);

            data.CartID = CID;

            Random rand = new Random();

            int number = rand.Next(1, 1000000);
            data.Amount = data.Cart.Amount - (data.Cart.Amount * (data.DiscountPercent / 100));
            data.InvoiceNo = "00" + number.ToString();
            var IID = await _inRepo.insertAsync(data);
            return IID;
        }

        public async Task<Invoice> GetInvoiceByinvNo(string invNumber)
        {
            return await _iRepo.getByinvNumberAsync(invNumber);
        }

        public async Task<Invoice> GetInvoiceByNumber(string InvNumber, int customerID)
        {

            var invoice = await _iRepo.getByNumberAsync(InvNumber,customerID);
            return invoice;

        }
        public async Task<List<Invoice>> GetInvoiceByCustomerID(int customerID)
        {

            var invoices = await _iRepo.getByCustomerIDAsync(customerID);
            return invoices;

        }
        public async Task<int> getLastAsync()
        {
            var lastNumber = await _iRepo.getLastAsync();
            return lastNumber;
        }

        public async Task<int> updateAsync(Invoice data)
        {
            var result = await _inRepo.updateAsync(data);
            return result;
        }
        public async Task<List<Invoice>> getDebtorInvoice(DateTime startdate,DateTime enddate)
        {
            var result = await _iRepo.getAllDebtorsAsync(startdate,enddate);
            return result;
        }

        public async Task<List<Invoice>> getProformaByDate(int customerID, string startDate, string endDate)
        {

            List<Invoice> invoices;
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
                return invoices = await _iRepo.getProformaByCustomerIDAsync(customerID);
            }






            // filter by dates alone if customerID is not given

            else if (customerID < 1 && (startDate != "0" || endDate != "0"))
            {
                return invoices = await _iRepo.getAllProformaByDateAsync(sdate, edate);
            }




            // filter by all given parameters

            else if (startDate != "0" && endDate != "0" && customerID > 0)
            {
                return invoices = await _iRepo.getAllProformaByDateandCustomerAsync(customerID, sdate, edate);
            }



            // get without any parameter
            else
                return invoices = await _iRepo.getAllProformaAsync();



        }

        public async Task deleteInvoice(int ID)
        {
            await _inRepo.deleteAsync(ID);
        }
    }
}
