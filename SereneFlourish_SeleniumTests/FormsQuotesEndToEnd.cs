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

            driver.Url = "http://localhost:3000/forms";

            driver.Quit();
        }
        //TC4-TSE01
        [Fact]
        public void TestQuotePageVisit()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Url = "http://localhost:3000/quote/1";

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

            driver.Url = "http://localhost:3000/admin/forms";

            Click(By.Name("btnNext"));

            IWebElement selectorValue = wait.Until(driver => driver.FindElement(By.Name("selectorOption")));

            Assert.Equal("2", selectorValue.Text);

            Click(By.Name("btnPrevious"));

            selectorValue = wait.Until(driver => driver.FindElement(By.Name("selectorOption")));

            Assert.Equal("1", selectorValue.Text);

            driver.Quit();
        }
        //TC2-TSE02
        [Fact]
        // Test the view quote button to view a quote associated ot the proper form.
        public void ViewQuoteForAssociatedForm()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/admin/forms";

            Click(By.CssSelector("[href *= '/admin/quote/1']"));

            IWebElement quoteButton = wait.Until(driver => driver.FindElement(By.XPath("//*[@id='root']/div/div/div/div/div[2]/form/table/tbody/tr/td[1]")));

            Assert.Equal("1", quoteButton.Text);

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

            driver.Url = "http://localhost:3000/admin/quote/1";

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

            driver.Url = "http://localhost:3000/admin/quote/1";

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
