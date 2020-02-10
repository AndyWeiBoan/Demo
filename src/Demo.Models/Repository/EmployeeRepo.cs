using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Models.Contract;
using Demo.Models.Poco;
using Microsoft.Extensions.Logging;
using Dapper;
namespace Demo.Models.Repository {
    public class EmployeeRepo : IEmployeeRepo<Employee> {
         
        internal struct SQLStatement {
            internal const string DELETE_ALL_EMPLOYEE = "DELETE FROM Employee";
            internal const string DELETE_EMPLOYEE = "DELETE FROM Employee WHERE [Guid] = @guid";
            internal const string INSERT_EMPLOYEE =
                @"INSERT INTO Employee (NEWID(), LastName, FirtName, Email, Country, Title, CreateAt)
                  VALUES(@guid, @LastName, @FirstName, @Email, @Country, @Title, @CreateAt)";
            internal const string SELECT_ALL_EMPLOYEE = 
                @"SELECT Guid, LastName, FirstName, Email, Country, Title, CreateAt FROM Employee";
            internal const string SELECT_EMPLOYEE =
                @"SELECT Guid, LastName, FirstName, Email, Country, Title, CreateAt 
                  FROM Employee WHERE Guid = @guid";
            internal const string UPDATE_EMPLOYEE = 
                @"UPDATE Employee SET LastName = @LastName, FirstName=@FirstName, Country =@Country, Title=@Title
                  WHERE Guid=@guid";
        }
        private readonly IdbFactory _dbFactory;
        private readonly ILogger<EmployeeRepo> _logger;
        public EmployeeRepo(
            IdbFactory dbFactory, 
            ILogger<EmployeeRepo> logger) {
            this._dbFactory = dbFactory;
            this._logger = logger;
        }

        public Task<IEnumerable<Employee>> GetAll() {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.QueryAsync<Employee>(SQLStatement.SELECT_ALL_EMPLOYEE);
            }
        }

        public Task<Employee> GetByGuid(string guid) {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.QueryFirstAsync<Employee>(SQLStatement.SELECT_EMPLOYEE, new { guid = guid});
            }
        }

        public Task<int> Insert(Employee Employee) {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.ExecuteAsync(SQLStatement.INSERT_EMPLOYEE, Employee);
            }
        }

        public Task<int> Insert(IEnumerable<Employee> Employees) {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.ExecuteAsync(SQLStatement.INSERT_EMPLOYEE, Employees);
            }
        }

        public Task<int> Update(Employee Employee) {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.ExecuteAsync(SQLStatement.UPDATE_EMPLOYEE, Employee);
            }
        }

        public Task<int> DeleteByGuid(string guid) {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.ExecuteAsync(SQLStatement.DELETE_EMPLOYEE, new { guid = guid});
            }
        }

        public Task<int> DeleteAll() {
            using (var cn = this._dbFactory.CreateConnection()) {
                return cn.ExecuteAsync(SQLStatement.DELETE_ALL_EMPLOYEE);
            }
        }
    }
}
