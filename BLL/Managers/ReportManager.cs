using BLL.Contract;
using BLL.DTO;
using DAL.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Managers
{
    public class ReportManager:IReportManager
    {
        private readonly IReportRepository _HomeRepo; //  = new ProductRepository();

        public ReportManager(IReportRepository HomeRepo)
        {
            _HomeRepo = HomeRepo;
        }

        public async Task<IEnumerable<TopNSoldBookModel>> GetTopNSellingBooksByDate(DateTime? startDate = null, DateTime? endDate = null)
        {
            if (!startDate.HasValue || !endDate.HasValue)
            {
                throw new ArgumentNullException("Start date and end date must be provided.");
            }

            return (IEnumerable<TopNSoldBookModel>)await _HomeRepo.GetTopNSellingBooksByDate(startDate.Value, endDate.Value);
        }

        
    
    }
}
