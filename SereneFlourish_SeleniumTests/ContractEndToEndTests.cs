using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class ContractEndToEndTests
    {
        EdgeDriver driver = new EdgeDriver();
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;

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

            FinalCostInput.SendKeys("200");
            DownPaymentInput.SendKeys("100");
            StartDateInput.SendKeys("06/13/2021");
            EndDateInput.SendKeys("07/02/2021");

            Click(By.Name("SubmitBtn"));

            string UpdateRequestAccept = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (UpdateRequestAccept.Equals("Success! The contract has been updated!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }

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
