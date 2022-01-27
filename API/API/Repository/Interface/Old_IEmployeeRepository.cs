using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface Old_IEmployeeRepository
    {
        IEnumerable<Employee> Get();
        Employee Get(String NIK);
        int Insert(Employee employee);
        int Update(Employee employee);
        int Delete(string NIK);
    }
}
