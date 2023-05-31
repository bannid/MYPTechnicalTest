using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using InvestmentAppProd.Models;
using InvestmentAppProd.Controllers;
using InvestmentAppProd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentAppProd.Tests
{
    [TestFixture]
    public class TestInvestment
    {
        [Test]
        public void InvestmentValid_ValidShouldReturnTrue()
        {
            var investment = new Investment
            {
                Name="Investment1",
                StartDate = DateTime.Parse("2022-05-01"),
                InterestType = "Simple",
                InterestRate = 5,
                PrincipalAmount = 10000
            };
            Assert.IsTrue(investment.Validate());
        }

        [Test]
        public void InvestmentValidation_ValidateShouldThrowExceptionWhenPrincipalAmountIsNegative()
        {
            var investment = new Investment {
                Name = "Investment1",
                StartDate = DateTime.Parse("2022-05-01"),
                InterestType = "Simple",
                InterestRate = 5,
                PrincipalAmount = -10000
            };
            Assert.Throws<InvestmentBaseException>(() => investment.Validate());
        }
        [Test]
        public void InvestmentValidation_ValidateShouldThrowExceptionWhenPrincipalAmountIsZero()
        {
            var investment = new Investment
            {
                Name = "Investment1",
                StartDate = DateTime.Parse("2022-05-01"),
                InterestType = "Simple",
                InterestRate = 5,
                PrincipalAmount = 0
            };
            Assert.Throws<InvestmentBaseException>(() => investment.Validate());
        }
        [Test]
        public void InvestmentCalculate_CalculateReturnsRightValueOnSimpleInterest()
        {
            var investment = new Investment
            {
                Name = "Investment1",
                StartDate = DateTime.Parse("2022-05-01"),
                InterestType = "Simple",
                InterestRate = 3.875,
                PrincipalAmount = 10000
            };
            investment.CalculateValue(DateTime.Parse("2023-05-01"));
            double value = investment.GetCurrentValue();
            //I + P in one year should be equal to 10387.50 with simple interest rate of 3.875 and principal amount of 10000
            //See https://www.calculatorsoup.com/calculators/financial/simple-interest-plus-principal-calculator.php
            Assert.AreEqual(value, 10387.50);
        }
        [Test]
        public void InvestmentCalculate_CalculateReturnsRightValueOnCompoundInterest()
        {
            var investment = new Investment
            {
                Name = "Investment1",
                StartDate = DateTime.Parse("2022-05-01"),
                InterestType = "Compound",
                InterestRate = 3.875,
                PrincipalAmount = 10000
            };
            investment.CalculateValue(DateTime.Parse("2023-05-01"));
            double value = investment.GetCurrentValue();
            //I + P in one year should be equal to 10394.46 with compound interest rate of 3.875 and principal amount of 10000
            //See https://www.calculatorsoup.com/calculators/financial/compound-interest-calculator.php
            Assert.AreEqual(value, 10394.46);
        }
    }
}
