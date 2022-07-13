using Dapper;
using System.Data.SqlClient;

namespace EmployeeManagement.Models
{
    public class DapperORM
    {
        /*      private static string connectionstring = "Data Source=0806-3427;" +
        "Initial Catalog=Employeemanagement;" +
        "User id=sa;" +
        "Password=aloha@123;";

              public static void ExecuteWithoutReturn(String procedure,DynamicParameters param)
              {
                  using (SqlConnection con = new SqlConnection(connectionstring))
                  { 
                  con.Open();
                  con.Execute(procedure,param,commandType:System.Data.CommandType.StoredProcedure);
                  }

              }

              public static T ExecuteReturnScalar<T>(String procedure, DynamicParameters param)
              {
                  using (SqlConnection con = new SqlConnection(connectionstring))
                  {
                      con.Open();
                      return (T) Convert.ChangeType(con.ExecuteScalar(procedure, param, commandType: System.Data.CommandType.StoredProcedure),typeof(T));
                  }

              }
              public static IEnumerable<T> ReturnList<T>(String procedure, DynamicParameters param)
              {
                  using (SqlConnection con = new SqlConnection(connectionstring))
                  { 
                  con.Open();
                  return con.Query<T>(procedure, param, commandType:System.Data.CommandType.StoredProcedure);
                  }

      }
        */
        private IConfiguration configuration;
        private string connectionString = "";
       
        public DapperORM()
        {
            connectionString = this.configuration.GetConnectionString("DefaultConnection");
        }

        public System.Data.IDbConnection Connection
        {
            get { 

                return new SqlConnection(connectionString);
            }
        }
        public void Create(Employee emp)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = @"INSERT INTO Employee1(Firstname,Lastname,Email) Values(@FirstName,@LastName,@Email)";
                dbConnection.Open();
                dbConnection.Execute(sQuery,emp);
            }
        }

        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            try
            {
                using (System.Data.IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    return await dbConnection.QueryAsync<Employee>("Select * from Employee1");
                }
            }
            catch(Exception) {
                throw;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                
                string sQuery = @"Select * from Employee1 where Employyeeid=@EmployeeId";
                dbConnection.Open();
                return dbConnection.Query<Employee>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public void UpdateEmployee(Employee obemp)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = @"Update Employee1 set Firstname=@FirstName ,Lastname=@LastName,Email=@Email where EmployyeeId=@EmployeeId";
                dbConnection.Open();
                dbConnection.Execute(sQuery,obemp);
            }
        }

        public void DeleteEmployee(int id)
        {
            using (System.Data.IDbConnection dbConnection = Connection)
            {
                string sQuery = @"Delete from Employee1 where EmployyeeId=@EmployeeId";
                dbConnection.Open();
                dbConnection.Execute(sQuery,new { id=id} );
            }
        }
    }
}
