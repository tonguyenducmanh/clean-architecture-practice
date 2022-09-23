using MISA.WEB08.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.CORE.Interfaces.Services
{
    /// <summary>
    /// Interface dùng chung, để triển khai các services chung
    /// </summary>
    /// Created by : TNMANH (23/09/2022)
    public interface IBaseServices<MISAEntity>
    {
        #region method

        /// <summary>
        /// Services xử lý trước khi thêm vào database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by : TNMANH (23/09/2022
        int InsertServices(MISAEntity entity);

        /// <summary>
        /// Services xử lý trước khi update vào database
        /// </summary>
        /// <param name="entityID"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by : TNMANH (23/09/2022)
        int UpdateServices(Guid entityID, MISAEntity entity);

        #endregion
    }
}
