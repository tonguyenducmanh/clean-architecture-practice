using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.CORE.Enums;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MISA.WEB08.AMIS.CORE.Resources;

namespace MISA.WEB08.AMIS.API.Controllers
{
    /// <summary>
    /// Các API liên quan tới việc lấy dữ liệu của bảng đơn vị trong database
    /// </summary>
    /// Created by : TNMANH (23/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        // Thêm các Interface vào các class để không phụ thuộc trực tiếp vào nhau
        // Sau đó dùng DI để xử lý qua file Program.cs
        #region  Dependences Injection

        IDepartmentRepository _departmentRepository;

        #endregion

        // Hàm khởi tạo để truyền Dependences Injection vào
        #region Contructor

        public DepartmentsController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        #endregion

        #region method

        #region methodGet

        /// <summary>
        /// API lấy danh sách toàn bộ phòng ban
        /// </summary>
        /// <returns></returns>
        /// Created by : TNMANH (23/09/2022)
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            try
            {
                // Thực hiện lấy dữ liệu
                var departments = _departmentRepository.GetAll();

                // Trả về statud code và kết quả cho người dùng
                return StatusCode(StatusCodes.Status200OK, departments);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

                // Trả về status code và object báo lỗi cho người dùng
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    ex.Message,
                    ResourceVN.Error_Exception,
                    ResourceLink.e001,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        #endregion

        #endregion

    }
}
