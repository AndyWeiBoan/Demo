using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Models.Contract {
    public interface IEmployeeRepo<T> {

        Task<IEnumerable<T>> GetAll();

        Task<T> GetByGuid(string guid);

        Task<int> Insert(T Employee);

        Task<int> Insert(IEnumerable<T> Employees);

        Task<int> Update(T Employee);

        Task<int> DeleteByGuid(string guid);

        Task<int> DeleteAll();
    }
}
