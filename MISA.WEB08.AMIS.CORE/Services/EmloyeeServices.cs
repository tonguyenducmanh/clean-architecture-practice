using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.CORE.Services
{
    /// <summary>
    /// Class triển khai Interface EmployeeServices để thực hiện các thao tác
    /// ứng với nhân viên trước khi được gửi vào database
    /// </summary>
    /// Created by: TNMANH (20/09/2022)
    public class EmloyeeServices : IEmployeeServices
    {

        #region method

        /// <summary>
        /// Thực hiện validate dữ liệu trước khi insert
        /// </summary>
        /// <param name="employee">Thông tin nhân viên</param>
        /// <returns></returns>
        /// Created by: TNMANH (20/09/2022)
        public int InsertServices(Employee employee)
        {
            // Mã nhân viên không được phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                var respone = new
                {
                    devMsg = "Mã nhân viên không được phép để trống",
                    userMsg = "Mã nhân viên không được phép để trống",
                    data = employee
                };
                // Check mã nhân viên không được phép trùng với nhân viên khác
                //phần này do employeeRepository xử lý
            }
            return 1;
        }

    /// <summary>
    /// Thực hiện validate dữ liệu trước khi update
    /// </summary>
    /// <param name="employee">Thông tin nhân viên</param>
    /// <param name="employeeID">ID của nhân viên</param>
    /// <returns></returns>
    /// Created by: TNMANH (20/09/2022)
    public int UpdateServices(Employee employee, Guid employeeID)
    {
        return 1;
    }

    #endregion
}
}
