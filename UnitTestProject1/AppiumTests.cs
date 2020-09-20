using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        [TestMethod]
        public void TestMethod2()
        {
            //arrange
            bool expected = true;
            bool actual = false;
            //act
            //Make sure API is running somewhere
            var listBox1 = FindById("ListBox1");
            var listBoxItem1 = FindByClassName("ListBoxItem", listBox1).FirstOrDefault();
            var x = FindByClassName("TextBlock", listBoxItem1).ToList();
            actual = x.Exists(x => x.Text.Contains("Hello World"));
            //assert
            Assert.AreEqual(expected, actual);
        }

        public static WindowsElement FindById(string id) => FindByAny(() => session.FindElementByAccessibilityId(id));
        public static WindowsElement FindByName(string id) => FindByAny(() => session.FindElementByName(id));
        public static WindowsElement FindByClassName(string id) => FindByAny(() => session.FindElementByClassName(id));
        public static IEnumerable<AppiumWebElement> FindByClassName(string id, AppiumWebElement ele) => FindByAny(() => ele.FindElementsByClassName(id));

        public static T FindByAny<T>(Func<T> f)
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