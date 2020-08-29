using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class WaybillService : IWaybillService
    {

        private readonly IWaybillRepo _wRepo;
        private readonly IRepo<WaybillProduct> _pRepo;

        public WaybillService(IWaybillRepo wRepo, IRepo<WaybillProduct> pRepo)
        {
            _wRepo = wRepo;
            _pRepo = pRepo;
        }

        public async Task<List<Waybill>> GetAllWaybillByDates(string startDate, string endDate)
        {

            DateTime.TryParseExact(startDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime sdate);
            DateTime.TryParseExact(endDate, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime edate);

            if (sdate <= DateTime.MinValue)
                sdate = DateTime.Now.StartOfDay();

            if (edate <= DateTime.MinValue)
                edate = DateTime.Now.EndOfDay();
            else
                edate = edate.EndOfDay();

            List<Waybill> waybills;
            if (startDate == "0" || endDate == "0")
                return waybills = await _wRepo.getAllAsync();



            return waybills=await _wRepo.getAllByDatesAsync(sdate, edate);
            


        }


        public async Task<int> SaveWaybillAndProducts(Waybill data)
        {
            var WID = await _wRepo.insertAsync(data);

            var products = new List<WaybillProduct>();
            foreach (var product in data.WaybillProducts)
            {
                
                product.WaybillID = WID;
                products.Add(product);
            }

            await _pRepo.insertListAsync(products);

            return WID;
        }


    }
}
