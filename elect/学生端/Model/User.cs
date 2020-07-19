using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public partial class User
    {
        public User()
        { }
        #region Model
        private string _username;
        private string _password;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        #endregion Model
    }
}
