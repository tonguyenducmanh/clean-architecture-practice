using MISA.WEB08.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.CORE.Interfaces.Services
{
    /// <summary>
    /// Interface dùng để triển khai các services chuyên để xử lý các vấn đề nghiệp vụ
    /// như là validate dữ liệu trước khi được gửi tới database
    /// </summary>
    /// Created by : TNMANH (20/09/2022)
    public interface IEmployeeServices
    {
        #region method

        /// <summary>
        /// Services xử lý trước khi thêm vào database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        /// Created by : TNMANH (20/09/2022
        int InsertServices(Employee employee);

        /// <summary>
        /// Services xử lý trước khi update vào database
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        /// Created by : TNMANH (20/09/2022)
        int UpdateServices(Guid employeeID, Employee employee); 

        #endregion

    }
}
