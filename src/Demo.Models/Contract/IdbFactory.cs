using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Demo.Models.Contract {
    public interface IdbFactory {
        IDbConnection CreateConnection();
    }
}
