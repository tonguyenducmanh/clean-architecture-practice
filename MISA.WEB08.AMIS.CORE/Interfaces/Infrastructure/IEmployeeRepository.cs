using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface dùng để triển khai các method được sử dụng để thêm sửa, xóa, đọc dữ liệu
    /// Từ ứng dụng tới Database
    /// </summary>
    /// Created by : TNMANH (20/08/2022)
    public interface IEmployeeRepository: IBaseRepository<Employee>
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
        PagingData Filter(string keyword, int limit, int offset);


        /// <summary>
        /// Lấy mã nhân viên lớn nhất trong bảng
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: TNMANH (20/09/2022)
        String GetMaxCode();

        /// <summary>
        /// Lấy thông tin chi tiết của 1 nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// Created by: TNMANH (20/09/2022)
        Employee GetByID(Guid employeeID);

        /// <summary>
        /// Check trùng mã nhân viên
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên</param>
        /// <returns></returns>
        /// Created by : TNMANH (22/09/2022)
        bool CheckDuplicateCode(string employeeCode);

        #endregion

        #region methodPOST

        /// <summary>
        /// Thêm mới 1 nhân viên vào bảng
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        int Insert(Employee employee);

        #endregion

        #region methodPUT
        /// <summary>
        /// Sửa thông tin 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <param name="employee">Thông tin sửa của nhân viên đó</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        int Update(Guid employeeID, Employee employee);

        #endregion

        #region methodDELETE

        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="employeeID">ID của nhân viên đó</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (20/09/2022)
        int Delete(Guid employeeID);

        #endregion

        #endregion
    }
}
