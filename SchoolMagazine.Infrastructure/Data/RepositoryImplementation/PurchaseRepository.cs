using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolMagazine.Domain.Entities.VendorEntities;
using SchoolMagazine.Domain.Interface;

namespace SchoolMagazine.Infrastructure.Data.RepositoryImplementation
{
    //public class PurchaseRepository : IPurchaseRepository
    //{
    //    private readonly MagazineContext _db;

    //    public PurchaseRepository(MagazineContext db)
    //    {
    //        _db = db;
    //    }

    //    public async Task AddPurchaseAsync(PurchaseProduct purchase)
    //    {
    //        await _db.PurchaseProducts.AddAsync(purchase);
    //        await _db.SaveChangesAsync();
    //    }

    //    public async Task PurchaseProductAsync(Guid schoolAdminId, Guid productId, int quantity)
    //    {
    //        // Find the product and reduce quantity
    //        var product = await _db.SchoolProducts.FindAsync(productId);
    //        if (product != null && product.AvailableQuantity >= quantity)
    //        {
    //            product.AvailableQuantity -= quantity;
    //            await _db.SaveChangesAsync();
    //        }
    //        else
    //        {
    //            throw new Exception("Insufficient product quantity.");
    //        }

    //    }

    //    public async Task PurchaseProductAsync(Guid userId, SchoolProduct product, Guid productId, int quantity)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
