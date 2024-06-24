using APBDFinalProject.Contexts;
using APBDFinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace APBDFinalProject.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private IncomeContext _incomeContext;

    public DiscountRepository(IncomeContext incomeContext)
    {
        _incomeContext = incomeContext;
    }

    public async Task<Discount?> GetBestSavedDiscount()
    {
        return await _incomeContext.Discounts
            .OrderByDescending(d => d.DiscountPercentage)
            .FirstOrDefaultAsync();
    }
}