using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using Demo.Models.Contract;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Serenity.Data;

namespace Demo.Models.DbFactory {
    public class dbFatory : IdbFactory {
        private readonly string _connectString;
        private readonly string _providerName;

        public dbFatory(IConfiguration config) {
            this._providerName = config.GetSection("ConnectionStrings:DefaultConnection").Value;
            this._connectString = config.GetSection("ConnectionStrings:ProviderName").Value;
        }

        private DbProviderFactory factory(string ProviderName) {
            DbProviderFactories.RegisterFactory("System.Data.SQLite", SQLiteFactory.Instance);
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            DbProviderFactories.RegisterFactory("MySql.Data.MySqlClient", MySqlClientFactory.Instance);
            return DbProviderFactories.GetFactory(this._providerName);
        }

        public IDbConnection CreateConnection() {
            IDbConnection conn = this.factory(this._providerName).CreateConnection();
            conn.ConnectionString = this._connectString;
            return conn;
        }
    }
}
