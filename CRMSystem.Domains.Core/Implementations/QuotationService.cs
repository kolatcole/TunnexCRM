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
        private readonly ISaleService _service;
        private readonly IRepo<Product> _proRepo;
        public QuotationService(IQuotationRepo qRepo, IRepo<QuotProduct> pRepo, IRepo<Sale> sRepo, ISaleService service, IRepo<Product> proRepo)
        {
            _qRepo = qRepo;
            _pRepo = pRepo;
            _service = service;
            _proRepo = proRepo;
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
            foreach (var product in data.QuotProducts)
            {
                product.QuotationID = QID;
                products.Add(product);
            }

            await _pRepo.insertListAsync(products);

            return QID;
        }

        public async Task<int> ChangeQuoteToSale(Quotation data, string Lpo, bool IsDeliver, decimal DeliveryFee, decimal discount)
        {

            // save quotation

           await SaveQuotationAndProducts(data);

            var sale = new Sale
            {
                DateCreated = DateTime.Now,
                CustomerID = data.CustomerID,
                DeliveryFee = DeliveryFee,
                ToDeliver = IsDeliver,
                LPO=Lpo,
                Cart=new Cart(),
                Invoice=new Invoice()
            };
            var items = new List<Item>();
            sale.Invoice.DiscountPercent = discount;

            // get sale cart 

            foreach (var Quoteprod in data.QuotProducts)
            {
                // get each cart item with product id

                var prod = await _proRepo.getAsync(Quoteprod.ProductID);

                var item = new Item
                {
                    
                    Amount=(Quoteprod.UnitPrice!=0)?Quoteprod.UnitPrice: prod.SalePrice,
                    ProductID = prod.ID,
                    Quantity = Quoteprod.Quantity
                };
                
                items.Add(item);
                
            };

            sale.Cart.Items = items;

            int SID = await _service.Save(sale);
            return SID;


        }

    }
}
