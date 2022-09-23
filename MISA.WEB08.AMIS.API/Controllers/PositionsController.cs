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
    /// Các api liên quan tới việc lấy dữ liệu chức vụ từ bảng positions trong database
    /// </summary>
    /// Created by : TNMANH (23/09/2022)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        // Thêm các Interface vào để các class không phụ thuộc trực tiếp vào nhau
        // Sau đó dùng DI để cử lý qua file Program.cs
        #region Dependences Injection

        IPositionRepository _positionRepository;

        #endregion

        // Hàm khởi tạo để truyền Dependences Injection vào
        #region Contructor

        public PositionsController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }

        #endregion

        // Danh sách các API liên quan tới việc lấy thông tin chức vụ
        #region method

        #region methodGet

        /// <summary>
        /// API lấy toàn bộ thông tin chức vụ
        /// </summary>
        /// <returns>Danh sách chức vụ</returns>
        /// Created by : TNMANH (23/09/2022)
        [HttpGet]
        public IActionResult GetAllPosition()
        {
            try
            {
                // Thực hiện lấy dữ liệu
                var position = _positionRepository.GetAll();

                // Trả về status code và kết quả cho người dùng
                return StatusCode(StatusCodes.Status200OK, position);
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
