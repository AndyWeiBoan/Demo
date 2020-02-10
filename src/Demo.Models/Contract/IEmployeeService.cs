using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo.Models.Poco;

namespace Demo.Models.Contract {
    public interface IEmployeeService {

        Task<bool> AddNewEmployee(Employee e);

        Task<bool> AddNewEmployee(IEnumerable<Employee> e);

        Task<IEnumerable<Employee>> GetAll();

        Task<Employee> GetByGuid(string guid);

        Task<bool> DeleteEmployee(string guid);

        Task<bool> DeleteEmployees();

        Task<bool> UpdataEmployeeInfo(Employee e);
    }
}
