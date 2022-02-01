using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class FormsQuotesEndToEnd
    {
        EdgeDriver driver = new EdgeDriver();
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;
        string projectRoot = Path.GetFullPath(@"..\..\..\");

        //TC4-TSE01
        [Fact]
        public void TestFormsPageVisit()
        {

            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            Login();

            driver.Url = "http://localhost:3000/admin/dashboard/forms";

            driver.Quit();
        }
        //TC4-TSE01
        [Fact]
        public void TestQuotePageVisit()
        {

            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            Login();

            driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            driver.Quit();
        }
        //TC2-TSE02
        [Fact]
        // Test the navigation buttons on the forms page, next, previous and last.
        public void TestNavigationButtonsOnFormsPage()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            Login();

            driver.Url = "http://localhost:3000/admin/dashboard/forms";

            Click(By.Name("btnNext"));

            IWebElement selectorValue = wait.Until(driver => driver.FindElement(By.Name("selectorOption")));

            Assert.Equal("1", selectorValue.Text);

            Click(By.Name("btnPrevious"));

            selectorValue = wait.Until(driver => driver.FindElement(By.Name("selectorOption")));

            Assert.Equal("1", selectorValue.Text);

            driver.Quit();
        }
        
        //TC2-TSE02
        [Fact]
        // Test the edit features of the quote page with valid information
        public void TestEditQuoteWithValidInformation()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            Login();

            driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            var search = driver.FindElement(By.Name("priceBox"));
            search.Clear();
            search.SendKeys("50");
            search = driver.FindElement(By.Name("materialsBox"));
            search.Clear();
            search.SendKeys("Wine Bottle");
            search = driver.FindElement(By.Name("status"));
            var selectElement = new SelectElement(search);
            selectElement.SelectByValue("Approved");
            Click(By.Name("btnSubmit"));
            Thread.Sleep(5000);
            string QuoteAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (QuoteAlertText.Equals("Quote updated"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }
        //TC2-TSE02
        [Fact]
        // Test the edit features of the quote page with Invalid information
        public void TestEditQuoteWithInvalidInformation()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            Login();

            driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            var search = driver.FindElement(By.Name("priceBox"));
            search.Clear();
            search.SendKeys("Test");
            string QuoteAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Dismiss();

            if (QuoteAlertText.Equals("Please enter a valid number") | QuoteAlertText.Equals("Error with quote, please verify inputted content"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }

        [Fact]
        // Test the edit features of the quote page with valid information to generate an email alert
        public void TestUpdateQuoteToApproved()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            Login();

            driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            var search = driver.FindElement(By.Name("priceBox"));
            search.Clear();
            search.SendKeys("160");
            search = driver.FindElement(By.Name("durationBox"));
            search.Clear();
            search.SendKeys("12");
            search = driver.FindElement(By.Name("materialsBox"));
            search.Clear();
            search.SendKeys("Wine Bottle");
            search = driver.FindElement(By.Name("status"));
            var selectElement = new SelectElement(search);
            selectElement.SelectByValue("Approved");
            Click(By.Name("btnSubmit"));
            Thread.Sleep(5000);
            driver.SwitchTo().Alert().Accept();
            string QuoteAlertText = driver.SwitchTo().Alert().Text;

            if (QuoteAlertText.Equals("A new contract has been made, check your email"))
            {
                status = true;
            }
            Assert.True(status);

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

        internal void Login()
        {
            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            driver.FindElement(By.Id("username")).SendKeys("admin");
            driver.FindElement(By.Id("password")).SendKeys("admin");

            // click login button
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(3000);
        }
    }
}
