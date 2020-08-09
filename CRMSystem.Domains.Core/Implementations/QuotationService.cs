using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class QuotationService : IQuotationService
    {

        private readonly IQuotationRepo _qRepo;
        private readonly IRepo<QuotProduct> _pRepo;

        public QuotationService(IQuotationRepo qRepo, IRepo<QuotProduct> pRepo)
        {
            _qRepo = qRepo;
            _pRepo = pRepo;
        }

        public async Task<List<Quotation>> GetAllByCustomerAndDates(int customerID, string startDate, string endDate)
        {
            List<Quotation> quotations;
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
                return quotations = await _qRepo.getAllByCustomerAsync(customerID);
            }






            // filter by dates alone if customerID is not given

            else if (customerID < 1 && (startDate != "0" || endDate != "0"))
            {
                return quotations = await _qRepo.getAllByDatesAsync(sdate, edate);
            } 

            

            
            // filter by all given parameters

            else if (startDate != "0" && endDate != "0" && customerID > 0)
            {
                return quotations = await _qRepo.getAllByCustomerandDateAsync(customerID, sdate, edate);
            }
                


            // get without any parameter
            else
            return quotations = await _qRepo.getAllQuotationsAsync();


        }

        public Task<List<Quotation>> GetAllQuotations()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveQuotationAndProducts(Quotation data)
        {
            var QID = await _qRepo.insertAsync(data);

            var products = new List<QuotProduct>();
            foreach(var product in data.QuotProducts)
            {
                product.QuotationID = QID;
                products.Add(product);
            }

            await _pRepo.insertListAsync(products);

            return QID;
        }

        
    }
}
