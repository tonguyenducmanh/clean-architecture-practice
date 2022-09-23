using MISA.WEB08.AMIS.CORE.Entities;
using MISA.WEB08.AMIS.CORE.Exceptions;
using MISA.WEB08.AMIS.CORE.Interfaces.Infrastructure;
using MISA.WEB08.AMIS.CORE.Interfaces.Services;

namespace MISA.WEB08.AMIS.CORE.Services
{
    /// <summary>
    /// Class base dùng để implement Interface IBaseServices
    /// </summary>
    /// Created by : TNMANH (23/09/2022)
    public class BaseServices<MISAEntity> : IBaseServices<MISAEntity>
    {
        // Khai báo sự phụ thuộc vào các Interface và tiến hành Dependences Injection
        // Để tuân thủ chữ D trong nguyên tắc SOLID
        #region Dependences Injection

        IBaseRepository<MISAEntity> _baseRepository;

        #endregion

        // tiến hành Injection ở đây
        #region contructor

        public BaseServices(IBaseRepository<MISAEntity> baseRepositoty)
        {
            _baseRepository = baseRepositoty;
        }

        #endregion

        #region method

        /// <summary>
        /// Thực hiện validate dữ liệu trước khi insert
        /// </summary>
        /// <param name="entity">Thông tin record</param>
        /// <returns></returns>
        /// Created by: TNMANH (23/09/2022)
        public int InsertServices(MISAEntity entity)
        {
            // Xử lý validate dữ liệu

            // Thêm mới vào database
            var res = _baseRepository.Insert(entity);
            return res;
        }

        /// <summary>
        /// Thực hiện validate dữ liệu trước khi update
        /// </summary>
        /// <param name="entity">Thông tin record</param>
        /// <param name="entityID">ID của record</param>
        /// <returns></returns>
        /// Created by: TNMANH (23/09/2022)
        public int UpdateServices(Guid entityID, MISAEntity entity)
        {
            // Xử lý validate dữ liệu

            // Sửa trong vào database
            var res = _baseRepository.Update(entityID, entity);
            return res;
        }

        #endregion
    }
}
