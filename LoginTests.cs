using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using System; 

namespace LoginAutomationTests
{
    class Program
    {
        static void Main(string[] args)
        {
        
            using (IWebDriver driver = new ChromeDriver())
            {
                
                driver.Navigate().GoToUrl("https://crusader.bransys.com/#/");
                Thread.Sleep(2000);

            
                CheckPresenceOfLoginFields(driver);
                ValidateInputFields(driver);
                CheckErrorMessages(driver);

            
                driver.Quit();
            }
        }

        static void CheckPresenceOfLoginFields(IWebDriver driver)
        {
            try
            {
                var usernameField = driver.FindElement(By.Name("username")); 
                var passwordField = driver.FindElement(By.Id("input-207")); 
                Console.WriteLine("Login fields are present.");
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Login fields are not present!");
            }
        }

        static void ValidateInputFields(IWebDriver driver)
        {
            var usernameField = driver.FindElement(By.Name("username"));
            var passwordField = driver.FindElement(By.Id("input-207"));

            
            usernameField.SendKeys("testuser@bransys.com");
            Thread.Sleep(2000);
            passwordField.SendKeys("testpassword");
            Thread.Sleep(2000);
            Console.WriteLine("Data can be entered in input fields.");
        }


        static void CheckErrorMessages(IWebDriver driver)
        {
            var usernameField = driver.FindElement(By.Name("username"));
            var passwordField = driver.FindElement(By.Id("input-207"));
            var loginButton = driver.FindElement(By.XPath("//button[@type='submit']"));

            
            usernameField.Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].value = '';", usernameField);
            usernameField.SendKeys("wronguser");
            Thread.Sleep(1000);
            passwordField.Click();
            js.ExecuteScript("arguments[0].value = '';", passwordField); 
            passwordField.SendKeys("wrongpassword");
            Thread.Sleep(1000);
            loginButton.Click();
            Thread.Sleep(1000);

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));

            

            try
    {
        
        var errorMessage = wait.Until(drv => drv.FindElement(By.ClassName("red--text"))); 
        Console.WriteLine("Correct error message is displayed.");
    }
    catch (WebDriverTimeoutException)
    {
        Console.WriteLine("Error message not displayed!");
    }
            

        }
    }
}
