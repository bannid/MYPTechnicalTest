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
        public Investment GetInvestment(string name);
        public void InsertInvestment(Investment investment);
        public void UpdateInvestment(Investment investment);
        public void DeleteInvestment(string name);

    }
}
