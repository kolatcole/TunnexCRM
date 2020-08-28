using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public class CartService : ICartService
    {

        private readonly IRepo<Item> _iRepo;
        private readonly IRepo<Cart> _cRepo;
        private readonly IRepo<Product> _pRepo;
        private readonly IRepo<Product> _proRepo;
        private readonly IRepo<PurchaseProduct> _ppRepo;

        public CartService(IRepo<Item> iRepo, IRepo<Cart> cRepo, IRepo<Product> pRepo, IRepo<Product> proRepo, IRepo<PurchaseProduct> ppRepo)
        {
            _iRepo = iRepo;
            _cRepo = cRepo;
            _pRepo = pRepo;
            _proRepo = proRepo;
            _ppRepo = ppRepo;
        }
        public async Task<int> SaveCart(Cart data)
        {
            int CID = await _cRepo.insertAsync(data);

            List<Item> items = new List<Item>();

            decimal amount = 0;

            // Increase product because it's a purchase transaction
            if (data.TransactionType)
            {

                foreach (var item in data.Items)
                {

                    item.CartID = CID;


                    // get product by productID

                    var product = await _pRepo.getAsync(item.ProductID);


                    // for supply

                    var pProduct = new PurchaseProduct
                    {

                        cartID = CID,
                        productID = item.ProductID,
                        Amount = item.Amount  // a unit amount, will be gotten from user
                    };

                    // save purchase Product to know the current value of the item bought in foreign currency

                    await _ppRepo.insertAsync(pProduct);


                    item.Amount *= item.Quantity; // to get the total amount of product bought
                   
                    amount += item.Amount;
                    item.Name = product.Name;

                    // increase product by the amount bought and update

                    product.Quantity += item.Quantity;

                    await _proRepo.updateAsync(product);


                    items.Add(item);
                }

            }

            else
            {
                foreach (var item in data.Items)
                {

                    item.CartID = CID;


                    // get product by productID

                    var product = await _pRepo.getAsync(item.ProductID);
                    item.Amount = item.Quantity * product.SalePrice;
                    amount += item.Amount;
                    item.Name = product.Name;

                    // mark up product by the amount bought and update

                    product.TotalSold += item.Quantity;
                    product.Quantity -= item.Quantity;

                    await _proRepo.updateAsync(product);


                    items.Add(item);
                }

            }
            
            

            await _iRepo.insertListAsync(items);

            // update cart to add total amount

            data.ID = CID;

            data.Amount = amount;
            await _cRepo.updateAsync(data);
            return CID;
        }

    }
}
