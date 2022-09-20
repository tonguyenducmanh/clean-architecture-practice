using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB08.AMIS.CORE.Exceptions
{
    /// <summary>
    /// Expception tùy chỉnh
    /// </summary>
    /// Created by : TNMANH (20/09/2022)
    public class MISAValidateException: Exception
    {
        #region field

        /// <summary>
        /// đoạn text thông báo lỗi
        /// </summary>
        /// Created by : TNMANH (20/09/2022)
        string? MsgErrorValidate = null;

        #endregion

        #region properties

        /// <summary>
        /// Override lại Message trả về là chuỗi thay vì Throw exception do cái throw này nặng lắm
        /// </summary>
        /// Created by : TNMANH (20/09/2022)
        public override string Message
        {
            get
            {
                return MsgErrorValidate;
            }
        }

        #endregion

        #region contructor

        /// <summary>
        /// Hàm khởi tạo, truyền message từ bên ngoài vào
        /// </summary>
        /// <param name="msg">message</param>
        /// Created by : TNMANH (20/09/2022)
        public MISAValidateException(string msg)
        {
            this.MsgErrorValidate = msg;
        }

        #endregion

        #region method



        #endregion
    }
}
