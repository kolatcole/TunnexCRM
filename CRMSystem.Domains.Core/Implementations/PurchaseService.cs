using CRMSystem.Domains.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class PurchaseService : IPurchaseService
    {

        private readonly IPurchaseRepo _pRepo;
        private readonly ICartService _cService;
        public PurchaseService(IPurchaseRepo pRepo, ICartService cService)
        {

            _pRepo = pRepo;
            _cService = cService;

        }
        public async Task<int> MakePurchase(Purchase data)
        {

           

            // save cart

            data.Cart.TransactionType = true;
            int CID = await _cService.SaveCart(data.Cart);

            //save purchase to get PID

            data.CartID = CID;

            data.TotalAmountForeign = data.Cart.Amount;
            data.TotalAmountNaira = data.NairaEquivalent * data.Cart.Amount;
            int PID = await _pRepo.insertAsync(data);

            return PID;

        }

        public async Task<Purchase> GetPurchaseByInvoiceNo(string invoiceNo)
        {
            var purchase = await _pRepo.getAsync(invoiceNo);
            return purchase;

        }

        public async Task<List<Purchase>> GetPurchasesReportByDate(int supplierID, string startDate, string endDate)
        {

            List<Purchase> purchases;
            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

            if (sdate <= DateTime.MinValue)
                sdate = DateTime.Now.StartOfDay();

            if (edate <= DateTime.MinValue)
                edate = DateTime.Now.EndOfDay();
            else
                edate = edate.EndOfDay();

            // filter by customerID only, if that's what was given

            if ((startDate == "0" || endDate == "0") && supplierID > 0)
            {
                return purchases = await _pRepo.getBySupplierIDAsync(supplierID);
            }






            // filter by dates alone if customerID is not given

            else if (supplierID < 1 && (startDate != "0" || endDate != "0"))
            {
                return purchases = await _pRepo.getPurchaseHistoryByDate(sdate, edate);
            }




            // filter by all given parameters

            else if (startDate != "0" && endDate != "0" && supplierID > 0)
            {
                return purchases = await _pRepo.getBySupplierIDandDateAsync(supplierID, sdate, edate);
            }



            // get without any parameter
            else
                return purchases = await _pRepo.getAllAsync();






        }
    }
}
