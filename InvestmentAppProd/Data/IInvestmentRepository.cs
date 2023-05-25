using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Data
{
    public interface IInvestmentRepository
    {

        public List<Investment> GetInvestments();
        public Investment GetInvestment();
        public bool InsertInvestment();
        public bool UpdateInvestment();
        public bool DeleteInvestment();

    }
}
