using MISA.WEB08.AMIS.CORE.Entities.DTO;
using MISA.WEB08.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure
{
    /// <summary>
    /// Interface dùng để triển khai các method được sử dụng để thêm sửa, xóa, đọc dữ liệu
    /// Từ ứng dụng tới Database dùng chung cho các table khác nhau được
    /// </summary>
    /// Created by : TNMANH (23/08/2022)
    public interface IBaseRepository<MISAEntity>
    {
        #region method

        #region methodGET

        /// <summary>
        /// Lấy tất cả record
        /// </summary>
        /// <returns>Danh sách record</returns>
        /// Created by: TNMANH (23/09/2022)
        IEnumerable<MISAEntity> GetAll();


        #endregion

        #region methodPOST

        /// <summary>
        /// Thêm mới 1 record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>ID của record</returns>
        /// Created by: TNMANH (23/09/2022)
        int Insert(MISAEntity entity);

        #endregion

        #region methodPUT
        /// <summary>
        /// Sửa thông tin 1 record
        /// </summary>
        /// <param name="entityID">ID của record</param>
        /// <param name="entity">Thông tin sửa của record</param>
        /// <returns>ID của nhân viên</returns>
        /// Created by: TNMANH (23/09/2022)
        int Update(Guid entityID, MISAEntity entity);

        #endregion

        #region methodDELETE


        #endregion

        #endregion
    }
}
