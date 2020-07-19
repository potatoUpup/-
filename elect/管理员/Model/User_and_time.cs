using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public partial class User_and_time
    {
        public User_and_time() { }
        #region time1
        private DateTime _d1;
        private DateTime _d2;
        private DateTime _d3;
        private DateTime _d4;
        public DateTime d1
        {
            set { _d1 = value; }
            get { return _d1; }
        }
        public DateTime d2
        {
            set { _d2 = value; }
            get { return _d2; }
        }
        public DateTime d3
        {
            set { _d3 = value; }
            get { return _d3; }
        }
        public DateTime d4
        {
            set { _d4 = value; }
            get { return _d4; }
        }
        #endregion time1
    }
}
