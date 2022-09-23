using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.INFRASTRUCTURE.Repository;
using MISA.WEB08.AMIS.CORE.Enums;
using MISA.WEB08.AMIS.CORE.Interfaces.Services;
using MISA.WEB08.AMIS.CORE.Services;
using MISA.WEB08.AMIS.CORE.Exceptions;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MISA.WEB08.AMIS.CORE.Resources;

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
        // Thêm các Interface vào để các class không phụ thuộc trực tiếp vào nhau mà phụ thuộc thông qua Interface
        // Sau đó dùng DI để xử lý qua file Program.cs
        #region Dependences Injection

        IEmployeeRepository _employeeRepository;
        IEmployeeServices   _employeeServices;

        #endregion

        #region Contructor
        // Hàm khởi tạo để truyền Dependences Injection vào
        public EmployeesController(IEmployeeRepository employeeRepository, IEmployeeServices employeeServices)
        {
            _employeeRepository = employeeRepository;
            _employeeServices = employeeServices;
        } 
        #endregion

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
            try
            {
                // Thực hiện lấy dữ liệu
                var employees = _employeeRepository.GetAll();

                // Trả về status code và kết quả cho người dùng
                return StatusCode(StatusCodes.Status200OK, employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                ));
            }
        }

        /// <summary>
        /// API lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by : TNMANH (17/09/2022)
        [HttpGet("max-code")]
        public IActionResult GetMaxEmployeeCode()
        {
            try
            {
                // Thực hiện lấy dữ liệu
                var maxEmployeeCode = _employeeRepository.GetMaxCode();

                //Trả về status code và kết quả cho người dùng
                return StatusCode(StatusCodes.Status200OK, maxEmployeeCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                ));
            }
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
            try
            {
                // Thực hiện lấy dữ liệu
                var employee = _employeeRepository.GetByID(employeeID);
                
                // Trả về status code và dữ liệu người dùng
                return StatusCode(StatusCodes.Status200OK, employee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                ));
            }
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
            try
            {
                // Thực hiện lấy dữ liệu
                EmployeeRepository employeeRepository = new EmployeeRepository();
                var result = employeeRepository.Filter(keyword, limit, offset);
                
                // Trả về status code và dữ liệu truy vấn
                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    "Có lỗi xảy ra, vui lòng liên hệ với MISA.",
                    "https://openapi.misa.com.vn/errorcode/e001",
                     HttpContext.TraceIdentifier
                ));
            }
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
            try
            {
                // Validate dữ liệu
                var validateResult = _employeeServices.InsertServices(employee);

                // Thực hiện thêm mới dữ liệu
                var result = _employeeRepository.Insert(employee);

                // Trả về status code và kết quả
                return StatusCode(StatusCodes.Status201Created, result);
            }
            //Validate mà trả về misa exception thì vào cái dưới
            catch (MISAValidateException ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                (
                    ErrorCode.EmptyCode,
                    ex.Message,
                    ResourceVN.Error_Validate_Common,
                    ResourceLink.e002,
                    HttpContext.TraceIdentifier
                ));
            }
            //Exception chung thì vào đây
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion

        // Danh sách các API liên quan tới việc sửa nhân viên đã có
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
            try
            {
                // Validate dữ liệu
                var validateResult = _employeeServices.UpdateServices(employeeID, employee);

                // Thực hiện truy vấn tới databse
                var result = _employeeRepository.Update(employeeID, employee);

                // Trả về status code và kết quả truy vấn
                return StatusCode(StatusCodes.Status200OK, result);
            }
            //Validate mà trả về misa exception thì vào cái dưới
            catch (MISAValidateException ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                (
                    ErrorCode.EmptyCode,
                    ex.Message,
                    ResourceVN.Error_Validate_Common,
                    ResourceLink.e002,
                    HttpContext.TraceIdentifier
                ));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion

        // Danh sách các API liên quan tới việc xóa nhân viên
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
            try
            {
                // Thực hiện xóa 1 nhân viên
                var result = _employeeRepository.Delete(employeeID);
                
                // Trả về status code và kết quả truy vấn
                return StatusCode(StatusCodes.Status200OK, employeeID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                (
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                ));
            }
        }

        #endregion
    }
}
