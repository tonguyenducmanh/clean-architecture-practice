using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.INFRASTRUCTURE.Repository;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Collections.Generic;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Danh sách các API liên quan tới dữ liệu nhân viên của bảng employee trong database
    /// </summary>
    /// Created by : TNMANH (17/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Danh sách các API liên quan tới việc lấy thông tin của nhân viên
        #region GetMethod

        /// <summary>
        /// API lấy danh sách toàn bộ nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("")]
        public IActionResult GetAllEmployees()
        {
            // Thực hiện lấy dữ liệu
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var employees = employeeRepository.GetAll();
           
            return StatusCode(StatusCodes.Status200OK, employees);
        }

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("max-code")]
        public IActionResult GetMaxEmployeeCode()
        {
            return StatusCode(StatusCodes.Status200OK, "NV99999");
        }

        /// <summary>
        /// API lấy thông tin chi tiết của 1 nhân viên theo ID đầu vào
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Thông tin của nhân viên theo ID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {
            // Thực hiện lấy dữ liệu
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var employee = employeeRepository.GetByID(employeeID);
            return StatusCode(StatusCodes.Status200OK, employee);
        }

        /// <summary>
        /// API lọc danh sách nhân viên theo các điều kiện cho trước
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm (mã, tên, số điện thoại của nhân viên)</param>
        /// <param name="limit">Số lượng kết quả trả về của 1 bảng</param>
        /// <param name="offset">Start Index của bảng</param>
        /// <returns>Tổng số bản ghi, tổng số trang, số trang hiện tại, danh sách kết quả</returns>
        [HttpGet("filter")]
        public IActionResult FilterEmployee(
            [FromQuery] string? keyword,
            [FromQuery] int limit,
            [FromQuery] int offset
            )
        {
            // Thực hiện lấy dữ liệu
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var result = employeeRepository.Filter(keyword, limit, offset);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        #endregion

        // Danh sách các API liên quan tới việc tạo mới nhân viên
        #region PostMethod

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Thông tin nhân viên mới</param>
        /// <returns>Status 201 created, employeeID</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            // Thực hiện thêm mới dữ liệu
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var result = employeeRepository.Insert(employee);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        #endregion

        #region PutMethod

        /// <summary>
        /// API sửa thông tin của 1 nhân viên dựa vào employeeID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên định sửa</param>
        /// <param name="employee">Giá trị sửa</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpPut("{employeeID}")]        
        public IActionResult UpdateEmployee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }

        #endregion

        #region DeleteMethod

        /// <summary>
        /// API xóa 1 nhân viên dựa vào ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên</param>
        /// <returns>Status 200 OK, employeeID / Status 400 badrequest</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            // Thực hiện xóa 1 nhân viên
            EmployeeRepository employeeRepository = new EmployeeRepository();
            var result = employeeRepository.Delete(employeeID);
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }

        #endregion
    }
}
