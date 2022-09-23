using Dapper;
using Microsoft.AspNetCore.Http;
using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MySqlConnector;
using System.Text;

namespace MISA.WEB08.AMIS.INFRASTRUCTURE.Repository
{
    /// <summary>
    /// Class triển khai Interface repository để thực hiện các method thêm sửa, xóa, đọc
    /// tới database
    /// </summary>
    /// Created by : TNMANH (20/08/2022)
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository

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
            var sqlConnection = new MySqlConnection(_connectionString);

            // Chuẩn bị câu lệnh MySQL
            string storeProcedureName = "Proc_employee_GetPaging";

            // Chèn parameter cho procedure
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("v_Offset", offset);
            parameters.Add("v_Limit", limit);
            parameters.Add("v_Search", keyword);

            // Thực hiện gọi vào trong Database
            var employeesFiltered = sqlConnection.QueryMultiple(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

            // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
            sqlConnection.Dispose();

            // Trả về status code kèm theo object kết quả
            return new PagingData()
            {
                PageSize = limit,
                PageNumber = offset / limit + 1,
                Data = employeesFiltered.Read<Employee>().ToList(),
                TotalCount = unchecked((int)employeesFiltered.ReadSingle().TotalCount),
            };

        }

        /// <summary>
        /// Lấy tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        /// method bên dưới thằng cha có rồi nên chả cần viết nữa
        
        //public IEnumerable<Employee> GetAll()
        //{
        //    // thực hiện gọi vào DB, phải dùng base . nếu không sẽ ko gọi được
        //    // tới  method của class cha
        //    return base.GetAll();
        //}

        /// <summary>
        /// Lấy thông tin chi tiết của 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public Employee GetByID(Guid employeeID)
        {
            var sqlConnection = new MySqlConnection(_connectionString);

            // Khai báo procedure name
            string storeProcedureName = "Proc_employee_GetOne";

            // Khởi tạo các parameter để chèn vào trong storeprocedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_id", employeeID);

            // Thực hiện kết nối tới Database
            var employee = sqlConnection.QueryFirstOrDefault<Employee>(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );

            // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
            sqlConnection.Dispose();

            // Trả về status code và kết quả trả về
            return employee;
        }

        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong bảng
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: TNMANH (20/09/2022)
        public string GetMaxCode()
        {
            var sqlConnection = new MySqlConnection(_connectionString);

            // Chuẩn bị câu lệnh Query
            string storeProcedureName = "Proc_employee_GetMaxCode";

            // Thực hiện gọi vào Database
            var maxCode = sqlConnection.QueryFirstOrDefault<String>(
                storeProcedureName,
                commandType: System.Data.CommandType.StoredProcedure
                );

            // Trả về Status code và kết quả
            return maxCode;
        }

        /// <summary>
        /// Check xem mã nhân viên có bị trùng hay không
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// Created by: TNMANH (20/09/2022)
        public bool CheckDuplicateCode(string employeeCode)
        {
            /// Khởi tạo kết nối tới Database
            var sqlConnection = new MySqlConnection(_connectionString);

            /// Khởi tạo storeProcedure
            var storeProcedureName = "Proc_employee_GetOneCode";

            /// Khởi tạo param
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_EmployeeCode", employeeCode);

            /// Thực hiện truy vấn kiểm tra tên
            var result = sqlConnection.Query(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

            // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
            sqlConnection.Dispose();

            // Nếu số lượng nhân viên có mã đó lớn hơn 0 thì return true
            if (result.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        #endregion

        #region methodPOST

        /// <summary>
        /// Thêm mới 1 nhân viên vào bảng
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        /// Phần này được kế thừa từ bên BaseRepository
        //public int Insert(Employee employee)
        //{
        //    var sqlConnection = new MySqlConnection(_connectionString);

        //    // chuẩn bị câu lệnh MySQL
        //    string storeProcedureName = "Proc_employee_PostOne";

        //    // Truyền tham số vào store procedure
        //    DynamicParameters parameters = new DynamicParameters();

        //    // Tạo ra employeeID bằng guid
        //    Guid employeeID = Guid.NewGuid();

        //    parameters.Add("v_EmployeeID", employeeID);

        //    // Chèn các giá trị khác vào param cho store procedure
        //    var props = typeof(Employee).GetProperties();

        //    foreach (var prop in props)
        //    {
        //        // lấy ra tên của properties
        //        var propName = prop.Name;
        //        var propValue = prop.GetValue(employee);
        //        if (propName != "EmployeeID")
        //        {
        //            parameters.Add($"v_{propName}", propValue);
        //        }
        //    }

        //    // Thực hiện chèn dữ liệu vào trong database
        //    var queryResult = sqlConnection.Execute(
        //            storeProcedureName,
        //            parameters,
        //            commandType: System.Data.CommandType.StoredProcedure
        //        );

        //    // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
        //    sqlConnection.Dispose();

        //    // Trả về kết quả
        //    return queryResult;
        //}

        #endregion

        #region methodPUT

        /// <summary>
        /// Sửa thông tin 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <param name="employee">Thông tin sửa của nhân viên đó</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        /// Phần này kế thừa từ bên Base Employee rồi
        //public int Update(Guid employeeID, Employee employee)
        //{
        //    var sqlConnection = new MySqlConnection(_connectionString);

        //    // chuẩn bị câu lệnh MySQL
        //    string storeProcedureName = "Proc_employee_PutOne";

        //    // Truyền tham số vào store procedure
        //    DynamicParameters parameters = new DynamicParameters();

        //    // Chèn các giá trị khác vào param cho store procedure
        //    var props = typeof(Employee).GetProperties();

        //    foreach (var prop in props)
        //    {
        //        // lấy ra tên của properties
        //        var propName = prop.Name;
        //        var propValue = prop.GetValue(employee);
        //        parameters.Add($"v_{propName}", propValue);
        //    }

        //    // Thực hiện chèn dữ liệu vào trong database
        //    var queryResult = sqlConnection.Execute(
        //            storeProcedureName,
        //            parameters,
        //            commandType: System.Data.CommandType.StoredProcedure
        //        );

        //    // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
        //    sqlConnection.Dispose();

        //    // Trả về kết quả
        //    return queryResult;
        //}

        #endregion

        #region methodDELETE

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên đó</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        public int Delete(Guid employeeID)
        {
            var sqlConnection = new MySqlConnection(_connectionString);

            // khởi tạo store procedure
            string storeProcedureName = "Proc_employee_DeleteOne";

            // khởi tạo các parameter truyền vào trong store procedure
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("v_id", employeeID);

            // thực hiện truy vấn tới database
            var deleteOne = sqlConnection.Execute(
                storeProcedureName,
                parameters,
                commandType: System.Data.CommandType.StoredProcedure
                );

            // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
            sqlConnection.Dispose();

            // trả về status code và kết quả
            return deleteOne;
        }

        #endregion


        #endregion
    }
}
