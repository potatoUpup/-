using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BLL
{
    public class message_and_choose
    {
        SQLDAL.message_and_choose dal = new SQLDAL.message_and_choose();
        public Model.message_and_choose GetModel(string id)
        {
            return dal.GetModel(id);
        }
        public Model.message_and_choose GetgroupModel(string xxid)
        {
            return dal.GetGroupModel(xxid);
        }
        public Model.message_and_choose time()
        {
            return dal.time();
        }
        public DataTable GetSureList(string teaid)
        {
            return dal.GetSureList(teaid);
        }
        public DataTable GetNoChooseList(string teaid, string term1, string term2)
        {
            return dal.GetNoChooseList(teaid,term1,term2);
        }
        public void Chooseing(string teaid,string xxid)
        {
            dal.Chooseing(teaid,xxid);
        }
        public void WithdrawalTeam(string id,string teaid)
        {
            dal.WithdrawalTeam(id, teaid);
        }
        public DataTable SelectionList(string teaid)
        {
            return dal.SelectionList(teaid);
        }
    }
}
