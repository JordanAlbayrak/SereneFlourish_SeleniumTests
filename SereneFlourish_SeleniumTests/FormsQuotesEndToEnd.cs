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
    public class FormsQuotesEndToEnd
    {
        private readonly EdgeDriver _driver = new();
        private readonly TimeSpan _time = TimeSpan.FromSeconds(5);
        private bool _status;

        //TC4-TSE01
        [Fact]
        public void TestFormsPageVisit()
        {

            var wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/forms";

            _driver.Quit();
        }
        //TC4-TSE01
        [Fact]
        public void TestQuotePageVisit()
        {

            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            _driver.Quit();
        }
        //TC2-TSE02
        [Fact]
        // Test the navigation buttons on the forms page, next, previous and last.
        public void TestNavigationButtonsOnFormsPage()
        {
            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/forms";

            Click(By.Name("btnNext"));

            IWebElement selectorValue = wait.Until(driver => driver.FindElement(By.Name("selectorOption")));

            Assert.Equal("1", selectorValue.Text);

            Click(By.Name("btnPrevious"));

            selectorValue = wait.Until(driver => driver.FindElement(By.Name("selectorOption")));

            Assert.Equal("1", selectorValue.Text);

            _driver.Quit();
        }
        
        //TC2-TSE02
        [Fact]
        // Test the edit features of the quote page with valid information
        public void TestEditQuoteWithValidInformation()
        {
            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            var search = _driver.FindElement(By.Name("priceBox"));
            search.Clear();
            search.SendKeys("50");
            search = _driver.FindElement(By.Name("materialsBox"));
            search.Clear();
            search.SendKeys("Wine Bottle");
            search = _driver.FindElement(By.Name("status"));
            var selectElement = new SelectElement(search);
            selectElement.SelectByValue("Approved");
            Click(By.Name("btnSubmit"));
            Thread.Sleep(5000);
            string QuoteAlertText = _driver.SwitchTo().Alert().Text;
            _driver.SwitchTo().Alert().Accept();

            if (QuoteAlertText.Equals("Quote updated"))
            {
                _status = true;
            }
            Assert.True(_status);

            _driver.Quit();
        }
        //TC2-TSE02
        [Fact]
        // Test the edit features of the quote page with Invalid information
        public void TestEditQuoteWithInvalidInformation()
        {
            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            var search = _driver.FindElement(By.Name("priceBox"));
            search.Clear();
            search.SendKeys("Test");
            string QuoteAlertText = _driver.SwitchTo().Alert().Text;
            _driver.SwitchTo().Alert().Dismiss();

            if (QuoteAlertText.Equals("Please enter a valid number") | QuoteAlertText.Equals("Error with quote, please verify inputted content"))
            {
                _status = true;
            }
            Assert.True(_status);

            _driver.Quit();
        }

        [Fact]
        // Test the edit features of the quote page with valid information to generate an email alert
        public void TestUpdateQuoteToApproved()
        {
            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/quote/1";

            var search = _driver.FindElement(By.Name("priceBox"));
            search.Clear();
            search.SendKeys("160");
            search = _driver.FindElement(By.Name("durationBox"));
            search.Clear();
            search.SendKeys("12");
            search = _driver.FindElement(By.Name("materialsBox"));
            search.Clear();
            search.SendKeys("Wine Bottle");
            search = _driver.FindElement(By.Name("status"));
            var selectElement = new SelectElement(search);
            selectElement.SelectByValue("Approved");
            Click(By.Name("btnSubmit"));
            Thread.Sleep(5000);
            _driver.SwitchTo().Alert().Accept();
            string quoteAlertText = _driver.SwitchTo().Alert().Text;

            if (quoteAlertText.Equals("A new contract has been made, check your email"))
            {
                _status = true;
            }
            Assert.True(_status);

            _driver.Quit();
        }

        [Fact]
        // Test the filtering by creation date feature on the forms page.
        public void TestFilterByCreationDate()
        {
            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/forms";

            IWebElement CreationDateInput = wait.Until(driver => driver.FindElement(By.Name("CreatedDateFilter")));

            CreationDateInput.Clear();
            CreationDateInput.SendKeys("02/04/2022");


            if (_driver.FindElement(By.XPath("//*[contains(text(), '2/4/2022')]")).Displayed)
            {
                _status = true;
            }
            Assert.True(_status);

            _driver.Quit();
        }
        [Fact]
        // Test the filtering by service type feature on the forms page.
        public void TestFilterByServiceType()
        {
            WebDriverWait wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            Login();

            _driver.Url = "http://localhost:3000/admin/dashboard/forms";

            IWebElement ServiceTypeInput = wait.Until(driver => driver.FindElement(By.XPath("//*[@id=\"react-select-2-input\"]")));

            ServiceTypeInput.Clear();
            ServiceTypeInput.SendKeys("Calligraphy");
            ServiceTypeInput.SendKeys(Keys.Enter);


            if (_driver.FindElement(By.TagName("td")).Text.Equals("Calligraphy"))
            {
                _status = true;
            }
            Assert.True(_status);

            _driver.Quit();
        }

        public bool Click(By by)
        {
            bool status = false;
            int i = 0;
            while (i == 0)
                try
                {
                    _driver.FindElement(by).Click();
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
            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("admin");
            _driver.FindElement(By.Id("password")).SendKeys("admin");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(3000);
        }
    }
}
