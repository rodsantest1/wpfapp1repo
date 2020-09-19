using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;

namespace UnitTestProject1
{
    [TestClass]
    public class AppiumTests : WpfAppSession
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void TestMethod1()
        {
            //arrange
            string expected = "4";
            string actual = "";
            //act
            //Make sure API is running somewhere
            var input1 = FindById("Input1");
            input1.SendKeys("3");
            var input2 = FindById("Input2");
            input2.SendKeys("1");
            actual = FindById("Sum").Text;
            //assert
            Assert.AreEqual(expected, actual);
        }

        public WindowsElement FindById(string id) => FindByAny(() => session.FindElementByAccessibilityId(id));

        public T FindByAny<T>(Func<T> f)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            do
            {
                try
                {
                    var x = f();

                    return x;
                }
                catch (Exception)
                {
                    System.Threading.Tasks.Task.Delay(50);

                    if (sw.Elapsed.TotalSeconds > 10)
                        throw new Exception("Rodney Santiago - Element not found error.");
                }
            } while (true);
        }
    }
}