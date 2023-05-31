using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace InvestmentAppProd.Models
{
	public class Investment
	{
		[Required]
		[Key]
		public string Name { get; set; }

		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public string InterestType { get; set; }
		[Required]
		public double InterestRate { get; set; }
		[Required]
		public double PrincipalAmount { get; set; }
		private double CurrentValue { get; set; } = 0;

		public Investment()
		{
		}

		public Investment(string name, DateTime startDate, string interestType, double rate, double principal)
		{
			Name = name;
			StartDate = startDate;
			InterestType = interestType;
			InterestRate = rate;
			PrincipalAmount = principal;
		}

		public bool Validate()
        {
			// TODO: Can interest rate be zero?
			// TODO: Can start date be in future?

			if (this.PrincipalAmount <= 0)
            {
				throw new InvestmentBaseException("Principal amount cannot be zero or negative", System.Net.HttpStatusCode.BadRequest);
            }
			if (this.InterestType != "Simple" && this.InterestType != "Compound")
            {
				throw new InvestmentBaseException("Interest type can either be Simple or Compound", System.Net.HttpStatusCode.BadRequest);
			}
			return true;
        }

		public void CalculateValue(DateTime endDate)
		{
			double interestRateNormalized;
			double t;
			double n;
			double simpleInterestFinalAmount;
			double compoundInterestFinalAmount;
			double monthsDiff;

			// Interest rate is divided by 100.
			interestRateNormalized = this.InterestRate / 100;
			// Add half month to the date to round to the nearest month.
			DateTime endDateModified = endDate.AddDays(16);
			// Time t is calculated to the nearest month.
			monthsDiff = Math.Abs(12 * (this.StartDate.Year - endDateModified.Year) + (endDateModified.Month - this.StartDate.Month));
			double timeInYears = monthsDiff / 12;
			// SIMPLE INTEREST.
			simpleInterestFinalAmount = this.PrincipalAmount * (1 + (interestRateNormalized * timeInYears));

			t = monthsDiff / 12;
			// COMPOUND INTEREST.
			// Compounding period is set to monthly (i.e. n = 12).
			n = 12;
			compoundInterestFinalAmount = this.PrincipalAmount * Math.Pow((1 + (interestRateNormalized / n)), (n * t));

			if (this.InterestType == "Simple")
				this.CurrentValue = Math.Round(simpleInterestFinalAmount, 2);
			else
				this.CurrentValue = Math.Round(compoundInterestFinalAmount, 2);
		}
		public double GetCurrentValue()
        {
			return Math.Round(this.CurrentValue, 2);
        }
	}
}
