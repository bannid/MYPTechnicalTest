﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvestmentAppProd.Models;

namespace InvestmentAppProd.Data
{

    public class InvestmentDBContext : DbContext, IInvestmentRepository
    {
        public DbSet<Investment> Investments { get; set; }

        public InvestmentDBContext(DbContextOptions<InvestmentDBContext> options) : base(options)
        {
        }

        public List<Investment> GetInvestments()
        {
            return this.Investments.ToList();
        }

        public Investment GetInvestment(string name)
        {
            return this.Investments.Find(name);
        }

        public void InsertInvestment(Investment investment)
        {
            if (this.Investments.Contains(investment))
            {
                throw new InvestmentBaseException(String.Format("Investment with name {0} already exists", investment.Name), System.Net.HttpStatusCode.BadRequest);
            }
            investment.Validate();
            investment.CalculateValue(DateTime.Now);
            this.ChangeTracker.Clear();
            this.Investments.Add(investment);
            this.SaveChanges();
        }

        public void UpdateInvestment(Investment investment)
        {
            var existingInvestment = this.GetInvestment(investment.Name);
            if (existingInvestment == null)
            {
                throw new InvestmentBaseException(String.Format("Investment with name {0} is not found", investment.Name), System.Net.HttpStatusCode.NotFound);
            }
            investment.Validate();
            investment.CalculateValue(DateTime.Now);
            this.ChangeTracker.Clear();
            this.Entry(investment).State = EntityState.Modified;
            this.SaveChanges();
        }

        public void DeleteInvestment(string name)
        {
            var investment = this.Investments.Find(name);
            if (investment == null)
            {
                throw new InvestmentBaseException(String.Format("Investment with name {0} not found", name), System.Net.HttpStatusCode.NotFound);
            }
            this.ChangeTracker.Clear();
            this.Investments.Remove(investment);
            this.SaveChanges();
        }
    }
}
