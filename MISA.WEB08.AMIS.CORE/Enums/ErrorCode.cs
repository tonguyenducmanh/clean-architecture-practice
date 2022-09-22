
namespace MISA.WEB08.AMIS.CORE.Enums
{
    /// <summary>
    /// enum chứa các mã lỗi tự tạo
    /// </summary>
    /// Created by : TNMANH (22/09/2022)
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi do exception
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        Exception = 1,

        /// <summary>
        /// Lỗi do trùng mã ID nhân viên
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi do mã để trống
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        EmptyCode = 3,

        /// <summary>
        /// Gọi vào DB để insert thất bại
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        InsertFailed = 4,

        /// <summary>
        /// Lỗi thuộc về database phòng ban
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        DepartmentError = 3,

        /// <summary>
        /// Lỗi thuộc về database chức vụ
        /// </summary>
        /// Created by : TNMANH (21/09/2022)
        PositionError = 4,
    }
}
