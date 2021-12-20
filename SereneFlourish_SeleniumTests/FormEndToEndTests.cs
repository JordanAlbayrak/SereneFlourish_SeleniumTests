using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class FormEndToEndTests
    {
        IWebDriver driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;
        string projectRoot = Path.GetFullPath(@"..\..\..\");

        [Fact]
        public void TestHomePageVisit()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            driver.Quit();
        }

        [Fact]
        public void TestHomePageRequestServiceButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();
            driver.Quit();


        }
        [Fact]
        public void TestHomePageImageButtons()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";

            //Press calligraphy img then go back
            IWebElement search = driver.FindElement(By.Name("calligraphy-img"));
            search.Click();
            driver.Navigate().Back();

            //Press engraving img then go back
            search = driver.FindElement(By.Name("engraving-img"));
            search.Click();
            driver.Navigate().Back();

            //Press event img then go back and close the page
            search = driver.FindElement(By.Name("event-img"));
            search.Click();
            driver.Navigate().Back();
            driver.Quit();

        }
        [Fact]
        public void TestCalligraphyFormPost_FieldsFilled()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("email"));
            search.SendKeys("tristan.jeremy.jordan@gmail.com");

            search = driver.FindElement(By.Name("street"));
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();
            search = driver.FindElement(By.Name("calligraphy-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.SendKeys("Test Comment Calligraphy!");
            
            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            driver.SwitchTo().Alert().Accept();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        [Fact]
        public void TestCalligraphyFormPost_EmptyFields()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Failed, All Info is required"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        [Fact]
        public void TestCalligraphyFormPost_EngravingAndComment()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("email"));
            search.SendKeys("tristan.jeremy.jordan@gmail.com");

            search = driver.FindElement(By.Name("street"));
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();
            search = driver.FindElement(By.Name("engraving-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.SendKeys("Test Comment Engraving!");

            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            driver.SwitchTo().Alert().Accept();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        [Fact]
        public void TestCalligraphyFormPost_EventAndComment()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("email"));
            search.SendKeys("tristan.jeremy.jordan@gmail.com");

            search = driver.FindElement(By.Name("street"));
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();
            search = driver.FindElement(By.Name("event-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.SendKeys("Test Comment Event!");

            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            driver.SwitchTo().Alert().Accept();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        [Fact]
        public void TestCalligraphyFormPost_CalligraphyNoComment()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("email"));
            search.SendKeys("tristan.jeremy.jordan@gmail.com");

            search = driver.FindElement(By.Name("street"));
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();
            search = driver.FindElement(By.Name("calligraphy-select"));
            search.Click();
            
            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Failed, All Info is required"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }
        [Fact]
        public void TestCalligraphyFormPost_EngravingNoComment()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("email"));
            search.SendKeys("tristan.jeremy.jordan@gmail.com");

            search = driver.FindElement(By.Name("street"));
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();
            search = driver.FindElement(By.Name("engraving-select"));
            search.Click();

            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Failed, All Info is required"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }
        [Fact]
        public void TestCalligraphyFormPost_EventNoComment()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            IWebElement search = driver.FindElement(By.Name("nbForm"));
            search.Click();

            search = driver.FindElement(By.Name("firstName"));
            search.SendKeys("John");

            search = driver.FindElement(By.Name("lastName"));
            search.SendKeys("Doe");

            search = driver.FindElement(By.Name("email"));
            search.SendKeys("tristan.jeremy.jordan@gmail.com");

            search = driver.FindElement(By.Name("street"));
            search.SendKeys("SexyStreet");

            search = driver.FindElement(By.Name("postal"));
            search.SendKeys("J4Y1P1");

            search = driver.FindElement(By.Name("city"));
            search.SendKeys("CoolCity");

            search = driver.FindElement(By.Name("country"));
            search.SendKeys("CockCountry");

            search = driver.FindElement(By.Name("service"));
            search.Click();
            search = driver.FindElement(By.Name("calligraphy-select"));
            search.Click();

            search = driver.FindElement(By.Name("comments"));
            search.SendKeys("");

            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Failed, All Info is required"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        [Fact]
        public void SendEmailWithNoAttachmentsTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Url = "http://localhost:3000/form";
            IWebElement fNameFeild = driver.FindElement(By.Name("firstName"));
            IWebElement lNameFeild = driver.FindElement(By.Name("lastName"));
            IWebElement emailField = driver.FindElement(By.Name("email"));
            IWebElement streetField = driver.FindElement(By.Name("street"));
            IWebElement postalFeild = driver.FindElement(By.Name("postal"));
            IWebElement cityFeild = driver.FindElement(By.Name("city"));
            IWebElement countryFeild = driver.FindElement(By.Name("country"));
            IWebElement serviceFeild = driver.FindElement(By.Name("service"));
            IWebElement commentsFeild = driver.FindElement(By.Name("comments"));
            IWebElement attachmentsField = driver.FindElement(By.Name("attachments"));
            IWebElement submitBtn = driver.FindElement(By.Name("submit-btn"));

            fNameFeild.SendKeys("Tristan");
            lNameFeild.SendKeys("Lafleur");
            emailField.SendKeys("tristan.jeremy.jordan@gmail.com");
            streetField.SendKeys("32 rue René");
            postalFeild.SendKeys("J2X 5S8");
            cityFeild.SendKeys("Saint-Jean-sur-Richelieu");
            countryFeild.SendKeys("Canada");
            serviceFeild.Click();
            serviceFeild.FindElement(By.Name("calligraphy-select")).Click();
            commentsFeild.SendKeys("pls god, i'm begging through text write some fancy text");

            attachmentsField.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            submitBtn.Click();

            driver.SwitchTo().Alert().Accept();

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }
    }
}
