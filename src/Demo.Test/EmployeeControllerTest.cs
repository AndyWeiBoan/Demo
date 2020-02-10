using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Models.Contract;
using Demo.Models.Poco;
using Demo.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace Demo.Test {

    [TestFixture]
    public class EmployeeControllerTest {

        private ILogger<EmployeeController> _logger;
        private IEmployeeService _service;
        private EmployeeController _target;

        [SetUp]
        public void Setup() {
            this._logger = Substitute.For<ILoggerStub<EmployeeController>>();
            this._service = Substitute.For<IEmployeeService>();
            this._target = new EmployeeController(_logger, _service);
        }

        [TestCase(TestName ="取得所有員工資料測試成功，應該要回傳 200 OK",Category =nameof(EmployeeControllerTest))]
        public async Task GetAllEmployeeTest() {
            // arrange
            this._service.GetAll().Returns(new List<Employee>());

            // act
            var resp = await this._target.Get() as ObjectResult;

            // assert
            Assert.AreEqual(200, resp.StatusCode);
        }

        [TestCaseSource(typeof(EmployeeContorllerTestcase), 
            nameof(EmployeeContorllerTestcase.Get_One_Employee_Testcase))]
        public async Task GetOneEmployeeTest(string guid, Employee res) {
            // arrange
            this._service.GetByGuid(guid).Returns(res);

            // act
            var resp = await this._target.Get(guid) as ObjectResult;

            // assert
            Assert.AreEqual(200, resp.StatusCode);
        }

        [TestCase(false,  TestName ="刪除全部員工失敗，應該要回傳200 OK")]
        [TestCase(true,  TestName ="刪除全部員工成功，應該要回傳200 OK")]
        public async Task DelAllEmployeeTest(bool Success) {
            // arrange
            this._service.DeleteEmployees().Returns(Success);

            // act 
            var resp = await this._target.Delete() as ObjectResult;

            // assert
            Assert.AreEqual(200, resp.StatusCode);
        }

        [TestCaseSource(typeof(EmployeeContorllerTestcase),
            nameof(EmployeeContorllerTestcase.Add_Employee_Testcase))]
        public async Task AddEmployeeTest(bool success, IEnumerable<Employee> list) {
            // arrange
            this._service.AddNewEmployee(list).Returns(success);

            // act
            var resp = await this._target.Post(list) as ObjectResult;

            // assert
            Assert.AreEqual(200, resp.StatusCode);
        }

        [TestCaseSource(typeof(EmployeeContorllerTestcase),
            nameof(EmployeeContorllerTestcase.Update_Employee_Testcase))]
        public async Task UpdateEmployeeInfoTest(bool success, Employee e) {
            // arrange
            this._service.UpdataEmployeeInfo(e).Returns(success);

            // act
            var resp = await this._target.Put(e) as ObjectResult;

            // assert
            Assert.AreEqual(200, resp.StatusCode);
        }
    }
}
