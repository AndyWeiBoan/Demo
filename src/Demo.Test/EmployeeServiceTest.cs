using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Models.Contract;
using Demo.Models.Poco;
using Demo.Models.Service;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;

namespace Demo.Test {

    [TestFixture]
    public class EmployeeServiceTest {

        private EmployeeService service;
        private ILogger<EmployeeService> _logger;
        private IEmployeeRepo<Employee> _eRepo;

        [SetUp]
        public void SetUp() {
            this._logger = Substitute.For<ILoggerStub<EmployeeService>>();
            this._eRepo = Substitute.For<IEmployeeRepo<Employee>>();
            this.service = new EmployeeService(_eRepo, _logger);
        }

        [TestCaseSource(typeof(EmployeeServiceTestcase),nameof(EmployeeServiceTestcase.Add_One_Employee_Testcase))]
        public async Task Add_One_Employee_Should_return_expected(
            bool expected, int rowAffected, Employee e) {

            // arrange
            this._eRepo.Insert(Arg.Any<Employee>()).Returns(rowAffected);

            // act
            var actual = await this.service.AddNewEmployee(e);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(EmployeeServiceTestcase), nameof(EmployeeServiceTestcase.Add_More_Than_One_Employee_Testcase))]
        public async Task Add_More_Than_One_Employee_Should_return_expected(
            bool expected, int rowAffected, IEnumerable<Employee> e) {

            // arrange
            this._eRepo.Insert(Arg.Any<IEnumerable<Employee>>()).Returns(rowAffected);

            // act
            var actual = await this.service.AddNewEmployee(e);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(EmployeeServiceTestcase), nameof(EmployeeServiceTestcase.Del_One_Employee_Testcase))]
        public async Task Del_One_Employee_Should_Retrun_Expected(
            bool expected, int rowAffected, string g) {
            // arrange
            this._eRepo.DeleteByGuid(g).Returns(rowAffected);

            // act 
            var actual = await this.service.DeleteEmployee(g);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(EmployeeServiceTestcase), nameof(EmployeeServiceTestcase.Del_All_Employee_Testcase))]
        public async Task Del_All_Employee_Should_Retrun_Expected(
            bool expected, int rowAffected) {
            // arrange
            this._eRepo.DeleteAll().Returns(rowAffected);

            // act 
            var actual = await this.service.DeleteEmployees();

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(EmployeeServiceTestcase), nameof(EmployeeServiceTestcase.Get_All_Employee_Testcase))]
        public async Task Gel_All_Employee_Should_Retrun_Expected_Count(
            int expected_Cnt, IEnumerable<Employee> res) {
            // arrange
            this._eRepo.GetAll().Returns(res);

            // act 
            var item = await this.service.GetAll();
            var actual = item.ToList().Count;

            //assert
            Assert.AreEqual(expected_Cnt, actual);

        }

        [TestCase(0, TestName ="取得所有員工資料時，發生例外，應回傳0筆資料", Category = nameof(EmployeeServiceTest))]
        public async Task Gel_All_Employee_Failed_Should_NULL(int Cnt) {
            // arrange
            this._eRepo.GetAll().Throws(new Exception());

            // act 
            var item = await this.service.GetAll();
            var actual = item.ToList().Count;

            //assert
            Assert.AreEqual(Cnt, actual);

        }

        [TestCaseSource(typeof(EmployeeServiceTestcase), nameof(EmployeeServiceTestcase.Get_One_Employee_Testcase))]
        public async Task Get_One_Employee_Success(string g, Employee e) {
            // arrange
            this._eRepo.GetByGuid(Arg.Any<string>()).Returns(e);

            //act
            var actual = await this.service.GetByGuid(g);

            // assert
            Assert.IsNotNull(actual);
        }

        [TestCaseSource(typeof(EmployeeServiceTestcase), nameof(EmployeeServiceTestcase.Update_One_Employee_Testcase))]
        public async Task Update_One_Employee_Should_return_expected(
            bool expected, int rowAffected, Employee e) {

            // arrange
            this._eRepo.Insert(Arg.Any<Employee>()).Returns(rowAffected);

            // act
            var actual = await this.service.AddNewEmployee(e);

            // assert
            Assert.AreEqual(expected, actual);
        }
    }
}
