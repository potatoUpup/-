using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    [Serializable]
    public partial class message_and_choose
    {
        public message_and_choose() {}
        //导师个人信息
        #region teamessage
        private string  _id;
        private string _name;
        private string _sex;
        private string _position;
        private string _groupnumber;
        private string _phone;
        private string _email;
        private string _academy;
        private string _research;
        private string _grade;
        private string _major;
        public string  id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        public string sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        public string position
        {
            set { _position = value; }
            get { return _position; }
        }
        public string groupnumber
        {
            set { _groupnumber = value; }
            get { return _groupnumber; }
        }
        public string phone
        {
            set { _phone = value; }
            get { return _phone; }
        }
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        public string academy
        {
            set { _academy = value; }
            get { return _academy; }
        }
        public string research
        {
            set { _research = value; }
            get { return _research; }
        }
        public string grade
        {
            set { _grade = value; }
            get { return _grade; }
        }
        public string major
        {
            set { _major = value; }
            get { return _major; }
        }
        #endregion teamessage

        //导师查看学生的团队信息
        #region groupmessage
        private string _Teamnumber;
        private string _topicname;
        private string _captainname;
        private string _memberonename;
        private string _membertwoname;
        private string _memberthreename;
        private string _topicintroduce;
        private string _captainid;
        private string _memberoneid;
        private string _membertwoid;
        private string _memberthreeid;
        private string _stuphone;
        public string Teamnumber
        {
            set { _Teamnumber = value; }
            get { return _Teamnumber; }
        }
        public string topicname
        {
            set { _topicname = value; }
            get { return _topicname; }
        }
        public string captainname
        {
            set { _captainname = value; }
            get { return _captainname; }
        }
        public string memberonename
        {
            set { _memberonename = value; }
            get { return _memberonename; }
        }
        public string membertwoname
        {
            set { _membertwoname = value; }
            get { return _membertwoname; }
        }
        public string memberthreename
        {
            set { _memberthreename = value; }
            get { return _memberthreename; }
        }
        public string topicintroduce
        {
            set { _topicintroduce = value; }
            get { return _topicintroduce; }
        }
        public string captainid
        {
            set { _captainid = value; }
            get { return _captainid; }
        }
        public string memberoneid
        {
            set { _memberoneid = value; }
            get { return _memberoneid; }
        }
        public string membertwoid
        {
            set { _membertwoid = value; }
            get { return _membertwoid; }
        }
        public string memberthreeid
        {
            set { _memberthreeid = value; }
            get { return _memberthreeid; }
        }
        public string stuphone
        {
            set { _stuphone = value; }
            get { return _stuphone; }
        }
        #endregion groupmess
        
        #region choose
        private string _stuid;
        private string _stuname;
        private string _subjectname;
        public string stuid
        {
            set { _stuid = value; }
            get { return _stuid; }
        }
        public string stuname
        {
            set { _stuname = value; }
            get { return _stuname; }
        }
        public string subjectname
        {
            set { _subjectname = value; }
            get { return _subjectname; }
        }
        #endregion choose

        #region time
        private DateTime _d1;
        private DateTime _d4;
        public DateTime d1
        {
            set { _d1 = value; }
            get { return _d1; }
        }
        public DateTime d4
        {
            set { _d4 = value; }
            get { return _d4; }
        }
        #endregion time
    }
}
