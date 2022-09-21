using Dapper;
using Microsoft.VisualBasic;
using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.AccessControl;
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

            // build chuỗi câu sql thực hiện thêm mới dữ liệu :

            var className = typeof(Employee).Name;
            var sqlColumnValue = new StringBuilder();
            // lấy ra tất cả properties của đối tượng
            // lấy ra giá trị của properties
            // thực hiện build câu lệnh sql

            var props = typeof(Employee).GetProperties();
            var delimiter = "";
            foreach (var prop in props)
            {
                // lấy ra tên của properties
                var propName = prop.Name;
                var propValue = prop.GetValue(employee);
                var propType = prop.PropertyType.Name;
                // kiểm tra xem có phải là kiểu date không, nếu là kiểu date thì tiến hành format value
                // thành định dạng value của datetime trong MySQL
                if (propType == "DateTime" && propValue != null)
                {
                    propValue =  null;
                    sqlColumnValue.Append($"{delimiter}null");
                    delimiter = ",";
                }
                // kiểm tra xem nó có phải enum không, nếu là enum thì phải lấy value theo enum
                else if (propType == propName)
                {
                    propValue = (int)Enum.Parse(prop.PropertyType, propValue.ToString());
                    sqlColumnValue.Append($"{delimiter}\"{propValue}\"");
                    delimiter = ",";
                }
                else
                // check xem có phải là trường EmployeeID không,
                // nếu không phải thì mới thêm vào câu truy vấn vì
                // thêm mới thì tự động sinh ra dữ liệu employeeID rồi
                if( propName != "EmployeeID")
                {
                    sqlColumnValue.Append($"{delimiter}\"{propValue}\"");
                    delimiter = ",";
                }
            }
            var sqlCommand = $"Call Proc_employee_PostOne({sqlColumnValue})";

            //thực hiện truy vấn dữ liệu trong database
            var result = sqlConnection.QueryFirstOrDefault<string>(sqlCommand);
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
