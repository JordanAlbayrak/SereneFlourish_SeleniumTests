using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.IO;
using System.Threading;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class FormEndToEndTests
    {
        EdgeDriver driver = new EdgeDriver();
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;
        string projectRoot = Path.GetFullPath(@"..\..\..\");

        private int ZoomValue = 100;
        private int ZoomIncrement = 20;

        //TC4-TSE01
        [Fact]
        public void TestHomePageVisit()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/";
            driver.Quit();
        }

        //TC4-TSE02
        [Fact]
        public void TestHomePageRequestServiceButton()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";
            IWebElement search = driver.FindElement(By.XPath("/html/body/div/body/nav[1]/div/a[3]"));
            search.Click();
            driver.Quit();


        }
        
        //TC4-TSE03
        [Fact]
        public void TestHomePageImageButtons()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";

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

        //TC4-TSE04
        [Fact]
        public void TestCalligraphyFormPost_FieldsFilled()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";
            IWebElement search = driver.FindElement(By.XPath("/html/body/div/body/nav[1]/div/a[3]"));
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

            Click(driver, By.XPath("/html/body/div[1]/body/div/div/div/div[2]/div[2]/form/div[1]/div[3]/div[1]/select"));
            Click(driver, By.XPath("/html/body/div[1]/body/div/div/div/div[2]/div[2]/form/div[1]/div[3]/div[1]/select/option[3]"));

            search = driver.FindElement(By.Name("comments"));
            search.SendKeys("Test Comment Calligraphy!");

            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.recaptcha-checkbox-border"))).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(2000);

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());

            driver.SwitchTo().Alert().Accept();

            wait.Until(ExpectedConditions.AlertIsPresent());

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        //TC4-TSE05
        [Fact]
        public void TestCalligraphyFormPost_EmptyFields()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";
            IWebElement search = driver.FindElement(By.XPath("/html/body/div/body/nav[1]/div/a[3]"));
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

        //TC4-TSE06
        [Fact]
        public void TestCalligraphyFormPost_EngravingAndComment()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";
            IWebElement search = driver.FindElement(By.XPath("/html/body/div/body/nav[1]/div/a[3]"));
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

            Click(driver, By.Name("service"));
            Click(driver, By.Name("Engraving-select"));

            search = driver.FindElement(By.Name("comments"));
            search.SendKeys("Test Comment Engraving!");

            search = driver.FindElement(By.Name("attachments"));
            search.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.recaptcha-checkbox-border"))).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(2000);

            search = driver.FindElement(By.Name("submit-btn"));
            search.Click();

            wait.Until(ExpectedConditions.AlertIsPresent());

            driver.SwitchTo().Alert().Accept();

            wait.Until(ExpectedConditions.AlertIsPresent());

            string emailRequestSentAlertText = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (emailRequestSentAlertText.Equals("Thank you for your request, an email has been sent your way!"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();

        }

        //TC4-TSE07
        [Fact]
        public void TestCalligraphyFormPost_CalligraphyNoComment()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";
            IWebElement search = driver.FindElement(By.XPath("/html/body/div/body/nav[1]/div/a[3]"));
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

            Click(driver, By.Name("service"));
            Click(driver, By.Name("Calligraphy-select"));

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

        //TC4-TSE08
        [Fact]
        public void TestCalligraphyFormPost_EngravingNoComment()
        {
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, time);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            driver.Url = "http://localhost:3000/home";
            IWebElement search = driver.FindElement(By.XPath("/html/body/div/body/nav[1]/div/a[3]"));
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

            Click(driver, By.Name("service"));
            Click(driver, By.Name("Engraving-select"));

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

        //TC4-TSE09
        [Fact]
        public void SendEmailWithNoAttachmentsTest()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Url = "http://localhost:3000/form";

            driver.Manage().Window.Maximize();

            IWebElement fNameFeild = wait.Until(driver => driver.FindElement(By.Name("firstName")));
            IWebElement lNameFeild = wait.Until(driver => driver.FindElement(By.Name("lastName")));
            IWebElement emailField = wait.Until(driver => driver.FindElement(By.Name("email")));
            IWebElement streetField = wait.Until(driver => driver.FindElement(By.Name("street")));
            IWebElement postalFeild = wait.Until(driver => driver.FindElement(By.Name("postal")));
            IWebElement cityFeild = wait.Until(driver => driver.FindElement(By.Name("city")));
            IWebElement countryFeild = wait.Until(driver => driver.FindElement(By.Name("country")));
            IWebElement serviceFeild = wait.Until(driver => driver.FindElement(By.Name("service")));
            IWebElement commentsFeild = wait.Until(driver => driver.FindElement(By.Name("comments")));
            IWebElement attachmentsField = wait.Until(driver => driver.FindElement(By.Name("attachments")));
            string submitBtn = "/html/body/div[1]/body/div/div/div/div[2]/div[2]/form/div[1]/button[1]";

            fNameFeild.SendKeys("Tristan");
            lNameFeild.SendKeys("Lafleur");
            emailField.SendKeys("tristan.jeremy.jordan@gmail.com");
            streetField.SendKeys("32 rue Ren�");
            postalFeild.SendKeys("J2X 5S8");
            cityFeild.SendKeys("Saint-Jean-sur-Richelieu");
            countryFeild.SendKeys("Canada");
            Click(driver, By.Name("service"));
            Click(driver, By.Name("Calligraphy-select"));
            commentsFeild.SendKeys("pls god, i'm begging through text write some fancy text");

            attachmentsField.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt(By.XPath("//iframe[starts-with(@name, 'a-') and starts-with(@src, 'https://www.google.com/recaptcha')]")));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("div.recaptcha-checkbox-border"))).Click();
            driver.SwitchTo().DefaultContent();

            Thread.Sleep(2000);

            Click(driver, By.XPath(submitBtn));

            wait.Until(ExpectedConditions.AlertIsPresent());

            driver.SwitchTo().Alert().Accept();

            wait.Until(ExpectedConditions.AlertIsPresent());

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
        public void TestRecaptchaErrorMessage()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Url = "http://localhost:3000/form";

            driver.Manage().Window.Maximize();

            IWebElement fNameFeild = wait.Until(driver => driver.FindElement(By.Name("firstName")));
            IWebElement lNameFeild = wait.Until(driver => driver.FindElement(By.Name("lastName")));
            IWebElement emailField = wait.Until(driver => driver.FindElement(By.Name("email")));
            IWebElement streetField = wait.Until(driver => driver.FindElement(By.Name("street")));
            IWebElement postalFeild = wait.Until(driver => driver.FindElement(By.Name("postal")));
            IWebElement cityFeild = wait.Until(driver => driver.FindElement(By.Name("city")));
            IWebElement countryFeild = wait.Until(driver => driver.FindElement(By.Name("country")));
            IWebElement serviceFeild = wait.Until(driver => driver.FindElement(By.Name("service")));
            IWebElement commentsFeild = wait.Until(driver => driver.FindElement(By.Name("comments")));
            IWebElement attachmentsField = wait.Until(driver => driver.FindElement(By.Name("attachments")));
            string submitBtn = "/html/body/div[1]/body/div/div[2]/div[2]/form/div/div[4]/div[2]/button";

            fNameFeild.SendKeys("Tristan");
            lNameFeild.SendKeys("Lafleur");
            emailField.SendKeys("tristan.jeremy.jordan@gmail.com");
            streetField.SendKeys("32 rue Ren�");
            postalFeild.SendKeys("J2X 5S8");
            cityFeild.SendKeys("Saint-Jean-sur-Richelieu");
            countryFeild.SendKeys("Canada");
            Click(driver, By.Name("service"));
            Click(driver, By.Name("Calligraphy-select"));
            commentsFeild.SendKeys("pls god, i'm begging through text write some fancy text");

            attachmentsField.SendKeys(projectRoot + @"Images\Calligraphy.jpg");

            Click(driver, By.XPath(submitBtn));

            wait.Until(ExpectedConditions.AlertIsPresent());

            string recaptchaErrorMessage = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (recaptchaErrorMessage.Equals("Failed, you need to validate reCAPTCHA first"))
            {
                status = true;
            }
            Assert.True(status);

            driver.Quit();
        }

        private void ZoomOut()
        {
            ZoomValue -= ZoomIncrement;
            Zoom(ZoomValue);
        }
        private void Zoom(int level)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript(string.Format("document.body.style.zoom='{0}%'", level));
        }

        public bool Click(EdgeDriver driver, By by)
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
