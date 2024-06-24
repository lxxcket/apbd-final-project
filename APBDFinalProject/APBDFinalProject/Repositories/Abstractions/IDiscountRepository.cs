using APBDFinalProject.Models;

namespace APBDFinalProject.Repositories;

public interface IDiscountRepository
{
    Task<Discount?> GetBiggestDiscount();
}