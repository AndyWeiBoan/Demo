using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Demo.Models.Contract;
using Demo.Models.Poco;
using Microsoft.Extensions.Logging;

namespace Demo.Models.Service {
    public class EmployeeService : IEmployeeService {

        private readonly IEmployeeRepo<Employee> _eRepo;
        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(
            IEmployeeRepo<Employee> eRepo,
            ILogger<EmployeeService> logger) {
            this._eRepo = eRepo;
            this._logger = logger;
        }

        private bool IsValid(Employee e) {
            if (e == null
                   || string.IsNullOrEmpty(e.LastName)
                   || string.IsNullOrEmpty(e.FirstName)
                   || string.IsNullOrEmpty(e.Country))
                return false;
            return true;
        }

        public async Task<bool> AddNewEmployee(Employee e) {
            try {
                if (!this.IsValid(e)) {
                    this._logger.LogError("Add employee failed since invalid.");
                    return false;
                }

                var res = await this._eRepo.Insert(e);
                if (res > 0) {
                    this._logger.LogInformation($"Add new employee success.(LastName:{e.LastName}, FirstName:{e.FirstName},Guid:{e.guid})");
                    return true;
                } else {
                    this._logger.LogError($"Add new employee failed.(LastName:{e.LastName}, FirstName:{e.FirstName},Guid:{e.guid})");
                }
            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
            }
            return false;
        }

        public async Task<bool> AddNewEmployee(IEnumerable<Employee> e) {
            try {
                var res = await this._eRepo.Insert(e);
                if (res <= 0) {
                    this._logger.LogWarning($"{res} row affected when employees added.");
                    return false;
                }
                this._logger.LogInformation($"Add employee successed.({res} row affected)");
                return true;
            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEmployee(string guid) {
            try {
                var res = await this._eRepo.DeleteByGuid(guid);
                if (res <= 0) {
                    this._logger.LogWarning(
                        $"This employee not exist since 0 row affected.({guid.ToString()})");
                    return false;
                }
                this._logger.LogInformation($"Delete employee successed.({res} row affected)");
                return true;

            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteEmployees() {
            try {
                var res = await this._eRepo.DeleteAll();
                if (res <= 0) {
                    this._logger.LogError(
                        $"The employee info was empty when all employee deleted.)");
                    return false;
                }
                this._logger.LogInformation($"Delete all employee successed.({res} row affected)");
                return true;

            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
                return false;
            }
        }

        public async Task<IEnumerable<Employee>> GetAll() {
            try {
                return await this._eRepo.GetAll();
            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
                return new List<Employee>();
            }
        }

        public async Task<Employee> GetByGuid(string guid) {
            try {
                return await this._eRepo.GetByGuid(guid);
            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdataEmployeeInfo(Employee e) {
            try {
                if (!this.IsValid(e)) {
                    this._logger.LogError("Update employee failed since invalid.");
                    return false;
                }
                var res = await this._eRepo.Update(e);
                if (res <= 0) {
                    this._logger.LogError(
                        $"Update employee info failed since not exist.(LastName{e.LastName}, FistName:{e.FirstName})");
                    return false;
                }
                this._logger.LogInformation(
                    $"Employee updated success.(LastName:{e.LastName}, FirstName:{e.FirstName}, Guid:{e.guid.ToString()})");

            } catch (Exception ex) {
                this._logger.LogError(ex, ex.Message);
                return false;
            }
            return true;
        }
    }
}
