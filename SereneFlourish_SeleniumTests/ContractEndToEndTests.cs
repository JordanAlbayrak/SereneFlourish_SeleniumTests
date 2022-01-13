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
