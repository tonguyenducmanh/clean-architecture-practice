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
    /// Từ ứng dụng tới Database
    /// </summary>
    /// Created by : TNMANH (23/08/2022)
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        #region method

        #region methodGET

        #endregion

        #endregion
    }
}
