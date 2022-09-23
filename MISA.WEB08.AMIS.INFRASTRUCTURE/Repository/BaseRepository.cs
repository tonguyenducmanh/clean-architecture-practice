using Dapper;
using MISA.WEB08.AMIS.CORE.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.INFRASTRUCTURE.Repository
{
    /// <summary>
    /// Khai báo base dùng chung cho các repository
    /// </summary>
    /// Created by : TNMANH (23/09/2022)
    public class BaseRepository<MISAEntity>
    {
        // chuỗi connection string giúp kết nối tới MySQL
        protected string _connectionString = "Server = localhost;Port = 5060;Database = misa.web08.gpbl.tnmanh;User Id = root;Password = 140300;";

        #region GetMethod
        
        /// <summary>
        /// Lấy toàn bộ record có trong database theo tên bảng
        /// </summary>
        /// <typeparam name="MISAEntity">Tên bảng muốn lấy</typeparam>
        /// <returns>Toàn bộ record của bảng đó</returns>
        /// Created by : TNMANH (23/09/2022)
        public IEnumerable<MISAEntity> GetAll()
        {
            var className = typeof(MISAEntity).Name.ToLower();

            using (var sqlConnection = new MySqlConnection(_connectionString))
            {
                // Khai báo store procedure
                string storeProcedureName = $"Proc_{className}_GetAll";

                // Thực hiện truy vấn trong database
                var entities = sqlConnection.Query<MISAEntity>(
                        storeProcedureName,
                        commandType: System.Data.CommandType.StoredProcedure
                    );

                // Return giá trị trả về

                return entities;
            }
        }

        #endregion

        #region PostMethod


        /// <summary>
        /// Thêm mới 1 record vào bảng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ID của record</returns>
        /// Created by: TNMANH (23/09/2022)
        public int Insert(MISAEntity entity)
        {
            var className = typeof(MISAEntity).Name;

            var sqlConnection = new MySqlConnection(_connectionString);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = $"Proc_{className.ToLower()}_PostOne";

            // Truyền tham số vào store procedure
            DynamicParameters parameters = new DynamicParameters();

            // Tạo ra entityID bằng guid
            Guid entityID = Guid.NewGuid();

            parameters.Add($"v_{className}ID", entityID);

            // Chèn các giá trị khác vào param cho store procedure
            var props = typeof(MISAEntity).GetProperties();

            foreach (var prop in props)
            {
                // lấy ra tên của properties
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                if (propName != $"{className}ID")
                {
                    parameters.Add($"v_{propName}", propValue);
                }
            }

            // Thực hiện chèn dữ liệu vào trong database
            var queryResult = sqlConnection.Execute(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

            // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
            sqlConnection.Dispose();

            // Trả về kết quả
            return queryResult;
        }


        #endregion

        #region PutMethod

        /// <summary>
        /// Sửa thông tin 1 record
        /// </summary>
        /// <param name="entityID">ID của record</param>
        /// <param name="entity">Thông tin sửa của record</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        public int Update(Guid entityID, MISAEntity entity)
        {
            var className = typeof(MISAEntity).Name;

            var sqlConnection = new MySqlConnection(_connectionString);

            // chuẩn bị câu lệnh MySQL
            string storeProcedureName = $"Proc_{className.ToLower()}_PutOne";

            // Truyền tham số vào store procedure
            DynamicParameters parameters = new DynamicParameters();

            // Chèn các giá trị khác vào param cho store procedure
            var props = typeof(MISAEntity).GetProperties();

            foreach (var prop in props)
            {
                // lấy ra tên của properties
                var propName = prop.Name;
                var propValue = prop.GetValue(entity);
                parameters.Add($"v_{propName}", propValue);
            }

            // Thực hiện chèn dữ liệu vào trong database
            var queryResult = sqlConnection.Execute(
                    storeProcedureName,
                    parameters,
                    commandType: System.Data.CommandType.StoredProcedure
                );

            // Xóa các giá trị đang nằm trong ram đi tránh bị đầy
            sqlConnection.Dispose();

            // Trả về kết quả
            return queryResult;
        }

        #endregion
    }
}
