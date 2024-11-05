using ClosedXML.Excel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Xml.Linq;

namespace TestProjectHRMT22
{
    [TestFixture]
    internal class ContactTest
    {
        public static string ExcelFilePath = @"C:\Users\Admin\Dropbox\PC\Desktop\KT.xlsx";
        private IWebDriver driver;
        public IDictionary<string, object> vars { get; private set; }
        private IJavaScriptExecutor js;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            js = (IJavaScriptExecutor)driver;
            vars = new Dictionary<string, object>();
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }
        public class TestCaseData
        {
            public string Email { get; set; }
            public string Name { get; set; }
            public string Subject { get; set; }
            public string YourMessage { get; set; }
            public string Expected { get; set; }
            public int CurrentRowId { get; set; }
        }
        //đọc file
            static TestCaseData[] CreateTestCase()
           {
            List<TestCaseData> testCases = new List<TestCaseData>();
            TestCaseData item = new TestCaseData();

            // Mở file Excel.
            using (var workbook = new XLWorkbook(ExcelFilePath))
            {
                // Chọn trang tính muốn cập nhật.
                var worksheet = workbook.Worksheet("Sheet1");

                if (worksheet == null)
                {
                    throw new Exception("Không tìm thấy trang tính");
                }

                // Duyệt qua các dòng, bắt đầu từ dòng thứ 2 nếu dòng đầu tiên là tiêu đề
                for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
                {
                    item = new TestCaseData();

                    item.Name = worksheet.Cell(row, 1).Value.ToString();
                    item.Email = worksheet.Cell(row, 2).Value.ToString();
                    item.Subject = worksheet.Cell(row, 3).Value.ToString();
                    item.YourMessage = worksheet.Cell(row, 4).Value.ToString();
                    item.Expected = worksheet.Cell(row, 5).Value.ToString();
                    item.CurrentRowId = row;

                    testCases.Add(item);
                }
            }

            return testCases.ToArray();
        }
        [TestCaseSource(nameof(CreateTestCase))]
        public void Test1Contact(TestCaseData testCaseData)
        {
            // Mở trang facebook
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://automationexercise.com/");
            Thread.Sleep(1000);
            // build
            driver.FindElement(By.XPath("//*[@id=\"header\"]/div/div/div/div[2]/div/ul/li[8]")).Click();


            //name
            driver.FindElement(By.Name("name")).SendKeys(testCaseData.Name.ToString());

            // email
            driver.FindElement(By.Name("email")).SendKeys(testCaseData.Email.ToString());

            //subject
            driver.FindElement(By.Name("subject")).SendKeys(testCaseData.Subject.ToString());

            //your mesage
            //subject
            driver.FindElement(By.Name("message")).SendKeys(testCaseData.YourMessage.ToString());

            //submit
            driver.FindElement(By.Name("submit")).Click();
            IAlert alert = driver.SwitchTo().Alert();
            alert.Accept();
            Thread.Sleep(2000);


            // actual
            var actualElement = driver.FindElement(By.XPath("//*[@id=\"contact-page\"]/div[2]/div[1]/div/div[2]"));

            string a = actualElement.Text;
            //// ghi file
            WriteFileExcel(testCaseData.CurrentRowId, testCaseData.Expected, a);

            Assert.That(testCaseData.Expected == a);
        }
        //    // ghi file
        public void WriteFileExcel(int currentRow, string expected, string actual)
        {
            // Mở file Excel.
            using (var workbook = new XLWorkbook(ExcelFilePath))
            {
                // Chọn trang tính muốn cập nhật.
                var worksheet = workbook.Worksheet("Sheet1");

                if (worksheet == null)
                {
                    throw new Exception("Không tìm thấy trang tính");
                }

                worksheet.Cell(currentRow, 6).Value = actual;
                worksheet.Cell(currentRow, 7).Value = expected == actual ? "passed" : "failed";
                workbook.Save();
            }
        }


        //public void WriteFileExcel(int currentRow, string expected, string actual)
        //{
        //    // Mở file Excel.
        //    using (var workbook = new XLWorkbook(ExcelFilePath))
        //    {
        //        // Chọn trang tính muốn cập nhật.
        //        var worksheet = workbook.Worksheet("Sheet1");

        //        if (worksheet == null)
        //        {
        //            throw new Exception("Không tìm thấy trang tính");
        //        }

        //        worksheet.Cell(currentRow, 6).Value = actual;
        //        worksheet.Cell(currentRow, 7).Value = expected == actual ? "passed" : "failed";
        //        workbook.Save();
        //    }
        //}
    }

    //[TestFixture]
    //internal class CalculateUnitTest
    //{
    //    public static string ExcelFilePath = @"C:\Users\Admin\Dropbox\PC\Desktop\KiemTraBDCL.xlsx";
    //    private IWebDriver driver;
    //    public IDictionary<string, object> vars { get; private set; }
    //    private IJavaScriptExecutor js;

    //    [SetUp]
    //    public void SetUp()
    //    {
    //        driver = new ChromeDriver();
    //        js = (IJavaScriptExecutor)driver;
    //        vars = new Dictionary<string, object>();
    //    }

    //    [TearDown]
    //    protected void TearDown()
    //    {
    //        driver.Quit();
    //    }

    //    public class TestCaseData
    //    {
    //        public string Email { get; set; }
    //        public string Name { get; set; }
    //        public string Subject { get; set; }
    //        public string Operation { get; set; }
    //        public string YourMessage { get; set; }
    //        public double Expected { get; set; }
    //        public int CurrentRowId { get; set; }
    //    }

    //    //đọc file
    //    static TestCaseData[] CreateTestCase()
    //    {
    //        List<TestCaseData> testCases = new List<TestCaseData>();
    //        TestCaseData item = new TestCaseData();

    //        // Mở file Excel.
    //        using (var workbook = new XLWorkbook(ExcelFilePath))
    //        {
    //            // Chọn trang tính muốn cập nhật.
    //            var worksheet = workbook.Worksheet("Sheet1");

    //            if (worksheet == null)
    //            {
    //                throw new Exception("Không tìm thấy trang tính");
    //            }

    //            // Duyệt qua các dòng, bắt đầu từ dòng thứ 2 nếu dòng đầu tiên là tiêu đề
    //            for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
    //            {
    //                item = new TestCaseData();

    //                item.Build = short.Parse(worksheet.Cell(row, 1).Value.ToString());
    //                item.FirstNumber = double.Parse(worksheet.Cell(row, 2).Value.ToString());
    //                item.SecondNumber = double.Parse(worksheet.Cell(row, 3).Value.ToString());
    //                item.Operation = worksheet.Cell(row, 4).Value.ToString();
    //                item.IntegersOnly = bool.Parse(worksheet.Cell(row, 5).Value.ToString());
    //                item.Expected = double.Parse(worksheet.Cell(row, 6).Value.ToString());
    //                item.CurrentRowId = row;

    //                testCases.Add(item);
    //            }
    //        }

    //        return testCases.ToArray();
    //    }

    //    [TestCaseSource(nameof(CreateTestCase))]
    //    public void Operation(TestCaseData testCaseData)
    //    {
    //        // Mở trang facebook
    //        driver.Manage().Window.Maximize();
    //        driver.Navigate().GoToUrl("https://testsheepnz.github.io/BasicCalculator.html");

    //        // build
    //        driver.FindElement(By.Id("selectBuild")).Click();
    //        {
    //            var dropdown = driver.FindElement(By.Id("selectBuild"));
    //            dropdown.FindElement(By.XPath($"//option[. = '{testCaseData.Build}']")).Click();
    //        }

    //        // first number
    //        driver.FindElement(By.Id("number1Field")).SendKeys(testCaseData.FirstNumber.ToString());

    //        // second number
    //        driver.FindElement(By.Id("number2Field")).SendKeys(testCaseData.SecondNumber.ToString());

    //        // Operation
    //        driver.FindElement(By.Id("selectOperationDropdown")).Click();
    //        {
    //            var dropdown = driver.FindElement(By.Id("selectOperationDropdown"));
    //            dropdown.FindElement(By.XPath(testCaseData.Operation)).Click();
    //        }

    //        // calculator
    //        driver.FindElement(By.Id("calculateButton")).Click();

    //        // actual
    //        var actual = driver.FindElement(By.Id("numberAnswerField"));

    //        // ghi file
    //        WriteFileExcel(testCaseData.CurrentRowId, testCaseData.Expected, double.Parse(actual.GetAttribute("value")));

    //        Assert.That(testCaseData.Expected == double.Parse(actual.GetAttribute("value")));
    //    }



    //    // ghi file
    //    public void WriteFileExcel(int currentRow, double expected, double actual)
    //    {
    //        // Mở file Excel.
    //        using (var workbook = new XLWorkbook(ExcelFilePath))
    //        {
    //            // Chọn trang tính muốn cập nhật.
    //            var worksheet = workbook.Worksheet("Sheet1");

    //            if (worksheet == null)
    //            {
    //                throw new Exception("Không tìm thấy trang tính");
    //            }

    //            worksheet.Cell(currentRow, 7).Value = actual;
    //            worksheet.Cell(currentRow, 8).Value = expected == actual ? "passed" : "failed";
    //            workbook.Save();
    //        }
    //    }
    //}
}