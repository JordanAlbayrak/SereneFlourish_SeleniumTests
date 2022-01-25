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
    public class AboutEndToEndTests
    {
        EdgeDriver driver = new EdgeDriver();
        TimeSpan time = TimeSpan.FromSeconds(5);
        bool status = false;

        //TC4-TSE01
        [Fact]
        public void TestAboutPageVisit()
        {
            WebDriverWait wait = new WebDriverWait(driver, time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            driver.Manage().Window.Maximize();

            driver.Url = "http://localhost:3000/about";

            IWebElement PageHeader = wait.Until(driver => driver.FindElement(By.TagName("h5")));

            Assert.Equal("Serena Tam", PageHeader.Text);

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

            driver.Url = "http://localhost:3000/about";

            Click(By.Name("btnEdit"));

            //About Tab
            IWebElement NameInput = wait.Until(driver => driver.FindElement(By.Name("inputName")));
            IWebElement EmailInput = wait.Until(driver => driver.FindElement(By.Name("inputEmail")));
            IWebElement PhoneInput = wait.Until(driver => driver.FindElement(By.Name("inputPhone")));
            IWebElement ProfessionInput = wait.Until(driver => driver.FindElement(By.Name("inputProfession")));
            IWebElement DescriptionInput = wait.Until(driver => driver.FindElement(By.Name("inputDescription")));

            NameInput.Clear();
            NameInput.SendKeys("Serena Tam");
            EmailInput.Clear();
            EmailInput.SendKeys("SereneFlourish@gmail.com");
            PhoneInput.Clear();
            PhoneInput.SendKeys("(123)-456-7890");
            ProfessionInput.Clear();
            ProfessionInput.SendKeys("Calligrapher and Engraver");
            DescriptionInput.Clear();
            DescriptionInput.SendKeys("I am very very good at what I do m8");

            //Experience Tab
            Click(By.Id("react-tabs-2"));

            IWebElement LanguageInput = wait.Until(driver => driver.FindElement(By.Name("inputLanguage")));
            IWebElement CountryInput = wait.Until(driver => driver.FindElement(By.Name("inputCountry")));
            IWebElement ExperienceInput = wait.Until(driver => driver.FindElement(By.Name("inputExperience")));

            LanguageInput.Clear();
            LanguageInput.SendKeys("All Languages");
            CountryInput.Clear();
            CountryInput.SendKeys("Canada");
            ExperienceInput.Clear();
            ExperienceInput.SendKeys("Phd in Everything");

            //Goal Tab
            Click(By.Id("react-tabs-4"));
            IWebElement MissionInput = wait.Until(driver => driver.FindElement(By.Name("inputMission")));

            MissionInput.Clear();
            MissionInput.SendKeys("Make that moneeeeeyy ya know");


            Click(By.Name("btnSave"));

            Thread.Sleep(5000);

            string UpdateRequestAccept = driver.SwitchTo().Alert().Text;
            driver.SwitchTo().Alert().Accept();

            if (UpdateRequestAccept.Equals("About Info updated"))
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
