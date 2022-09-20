using Dapper;
using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.INFRASTRUCTURE.Repository
{
    /// <summary>
    /// Class triển khai Interface repository để thực hiện các method thêm sửa, xóa, đọc
    /// tới database
    /// </summary>
    /// Created by : TNMANH (20/08/2022)
    public class EmployeeRepository : IEmployeeRepository

    {
        #region method

        #region methodGET

        /// <summary>
        /// Lấy danh sách nhân viên theo điều kiện
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm</param>
        /// <param name="limit">Số kết quả trên 1 trang</param>
        /// <param name="offset">Số bắt đầu</param>
        /// <returns>Danh sách nhân viên theo điều kiện</returns>
        /// Created by: TNMANH (20/09/2022)
        public PagingData Filter(string keyword, int limit, int offset)
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.QueryMultiple($"Call Proc_employee_GetPaging({offset}, {limit}, \"{keyword}\")"); ;
            return new PagingData()
            {
                PageSize = limit,
                PageNumber = offset / limit + 1,
                Data = result.Read<Employee>().ToList(),
                TotalCount = unchecked((int)result.ReadSingle().TotalCount),
            };

        }

        /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        public IEnumerable<Employee> GetAll()
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.Query<Employee>("Call Proc_employee_GetPaging(0, 10, NULL)");
            return result;

        }

        /// <summary>
        /// Lấy thông tin chi tiết của 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee GetByID(Guid employeeID)
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.QueryFirstOrDefault<Employee>($"Call Proc_employee_GetOne(\"{employeeID}\")");
            return result;
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong bảng
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: TNMANH (20/09/2022)
        public string GetMaxCode()
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.QueryFirstOrDefault<string>("Call Proc_employee_GetMaxCode()");
            return result;

        }

        #endregion

        #region methodPOST

        /// <summary>
        /// Thêm mới 1 nhân viên vào bảng
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        public string Insert(Employee employee)
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.QueryFirstOrDefault<string>("Call Proc_employee_GetPaging(0, 10, NULL)");
            return result;

        }

        #endregion

        #region methodPUT

        /// <summary>
        /// Sửa thông tin 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <param name="employee">Thông tin sửa của nhân viên đó</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        public string Update(Guid employeeID, Employee employee)
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.QueryFirstOrDefault<string>("Call Proc_employee_GetPaging(0, 10, NULL)");
            return result;

        }

        #endregion

        #region methodDELETE

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên đó</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        public string Delete(Guid employeeID)
        {
            // Tạo connection string
            string connectionString = "" +
                "Server = localhost;" +
                "Port = 5060;" +
                "Database = misa.web08.gpbl.tnmanh;" +
                "User Id = root;" +
                "Password = 140300;";

            // khởi tạo kết nối
            var sqlConnection = new MySqlConnection(connectionString);

            //thực hiện xóa 1 records trong database
            var result = sqlConnection.QueryFirstOrDefault<string>($"CALL Proc_employee_DeleteOne(\"{employeeID}\")");
            return result;

        }

        #endregion


        #endregion
    }
}
