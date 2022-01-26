using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class ContractEndToEndTests
    {
        EdgeDriver driver = new EdgeDriver();
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;

        //TC2-TSE01
        [Fact]
        // Test to see if we can access the contracts page
        public void VisitContractsSummaryPageOk()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            IWebElement PageHeader = wait.Until(driver => driver.FindElement(By.TagName("h1")));

            Assert.Equal("Contracts Information Page", PageHeader.Text);

            driver.Quit();
        }

        //TC2-TSE02
        [Fact]
        // Test to see if we can clkc on a details button to find contract details
        public void ClickDetailsContractsButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            Click(By.Name("1DetailsBtn"));

            IWebElement PageHeader = wait.Until(driver => driver.FindElement(By.TagName("h1")));

            Assert.Equal("Contract Details for Contract 1", PageHeader.Text);

            driver.Quit();
        }

        //TC2-TSE03
        [Fact]
        // Test to see if we can update a contract
        public void UpdateContractOk()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            Click(By.Name("3DetailsBtn"));

            IWebElement FinalCostInput = wait.Until(driver => driver.FindElement(By.Name("FinalCost")));
            IWebElement DownPaymentInput = wait.Until(driver => driver.FindElement(By.Name("DownPayment")));
            IWebElement StartDateInput = wait.Until(driver => driver.FindElement(By.Name("DateCommissioned")));
            IWebElement EndDateInput = wait.Until(driver => driver.FindElement(By.Name("EndDate")));

            FinalCostInput.Clear();
            FinalCostInput.SendKeys("200");
            DownPaymentInput.Clear();
            DownPaymentInput.SendKeys("100");
            StartDateInput.Clear();
            StartDateInput.SendKeys("01/02/2022");
            EndDateInput.Clear();
            EndDateInput.SendKeys("01/12/2022");

            Click(By.Name("SubmitBtn"));

            Thread.Sleep(5000);

            string UpdateRequestAccept = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (UpdateRequestAccept.Equals("Success! The contract has been updated!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }

        //TC2-TSE04
        [Fact]
        // Test to see if we encounter a completed contract
        public void CheckDisabledSubmit()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            Click(By.Name("1DetailsBtn"));

            IWebElement SubmitButton = wait.Until(driver => driver.FindElement(By.Name("SubmitBtn")));

            bool enabled = SubmitButton.GetAttribute("class").Contains("disabled");

            Assert.False(enabled);

            driver.Quit();
        }

        //TC2-TSE05
        [Fact]
        // Test to see if we fail to put in values in fields
        public void ErrorMessageEmptyInputs()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            Click(By.Name("3DetailsBtn"));

            IWebElement FinalCostInput = wait.Until(driver => driver.FindElement(By.Name("FinalCost")));

            FinalCostInput.Clear();

            Click(By.Name("SubmitBtn"));

            string UpdateRequestDeny = wait.Until(driver => driver.SwitchTo().Alert().Text);
            driver.SwitchTo().Alert().Accept();

            if (UpdateRequestDeny.Equals("Failed, All Info is required"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }

        //TC2-TSE06
        [Fact]
        // Test to see if we try to make the start date into an earlier date
        public void ErrorMessageInvalidStartDate()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            Click(By.Name("3DetailsBtn"));

            IWebElement StartDateInput = wait.Until(driver => driver.FindElement(By.Name("DateCommissioned")));

            StartDateInput.Clear();
            StartDateInput.SendKeys("01/01/1950");

            Click(By.Name("SubmitBtn"));

            string UpdateRequestDeny = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (UpdateRequestDeny.Equals("Failed, you can't set the start date to an earlier date"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }

        [Fact]
        // Test to see if we can get to the montrhly warnings page
        public void CheckMonthlyEarningsPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/contracts";

            Click(By.Name("EarningsBtn"));

            IWebElement header = wait.Until(driver => driver.FindElement(By.Name("EarningsHeader")));

            Assert.Equal("Monthly Earnings Page", header.Text);

            driver.Quit();
        }

        public bool Click(By by)
        {
            bool status = false;
            int i = 0;
            while (i == 0)
                try
                {
                    driver.FindElement(by).Click();
                    status = true;
                    break;
                }
                catch (StaleElementReferenceException e)
                {
                }
            return status;
        }
    }
}
