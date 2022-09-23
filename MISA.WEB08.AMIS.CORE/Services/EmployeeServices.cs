using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Exceptions;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MISA.WEB08.AMIS.CORE.Interfaces.Services;
using MISA.WEB08.AMIS.CORE.Resources;

namespace MISA.WEB08.AMIS.CORE.Services
{
    /// <summary>
    /// Class triển khai Interface EmployeeServices để thực hiện các thao tác
    /// ứng với nhân viên trước khi được gửi vào database
    /// </summary>
    /// Created by: TNMANH (20/09/2022)
    public class EmployeeServices : IEmployeeServices
    {

        // Khai báo sự phụ thuộc vào các Interface và tiến hành Dependences Injection
        // Để tuân thủ chữ D trong nguyên tắc SOLID
        #region Dependences Injection

        IEmployeeRepository _employeeRepository;

        #endregion

        // tiến hành Injection ở đây
        #region contructor

        public EmployeeServices(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        #endregion

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
                throw new MISAValidateException(ResourceVN.Error_Validate_Employee_EmployeeCode);
            }
            else if (string.IsNullOrEmpty(employee.FullName))
            {
                throw new MISAValidateException(ResourceVN.Error_Validate_Employee_FullName);
            }
            else if (employee.DepartmentID == null || employee.DepartmentID == Guid.Empty)
            {
                throw new MISAValidateException(ResourceVN.Error_Validate_Department_DepartmentID);
            }

            // Check mã nhân viên không được phép trùng với nhân viên khác
            //phần này do employeeRepository xử lý
            var isDuplicate = _employeeRepository.CheckDuplicateCode(employee.EmployeeCode);

            if (isDuplicate == true)
            {
                throw new MISAValidateException(ResourceVN.Error_Validate_Employee_Duplicate);
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
        public int UpdateServices(Guid employeeID, Employee employee)
        {
            // Mã nhân viên không được phép để trống
            if (string.IsNullOrEmpty(employee.EmployeeCode))
            {
                throw new MISAValidateException(ResourceVN.Error_Validate_Employee_EmployeeCode);
            }
            else if (string.IsNullOrEmpty(employee.FullName))
            {
                throw new MISAValidateException(ResourceVN.Error_Validate_Employee_FullName);
            }
            else if (employee.DepartmentID == null || employee.DepartmentID == Guid.Empty)
            {
                throw new MISAValidateException(ResourceVN.Error_Validate_Department_DepartmentID);
            }

            return 1;
        }

    #endregion
}
}
