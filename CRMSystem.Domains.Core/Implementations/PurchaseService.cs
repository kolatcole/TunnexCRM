using CRMSystem.Domains.Core;
using System;
using System.Collections.Generic;
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
    }
}
