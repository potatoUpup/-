using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    [Serializable]
    public partial class stu
    {
        private string _id;
        private string _password;
        private string _name;
        private string _sex;
        private string _grade;
        private string _major;
        private string _class;
        private string _phone;
        private string _email;
        private string _introduce;
        private string _academy;
        private string _groupID;
        public string id
        {
            set { _id = value; }
            get { return _id; }
        }
        public string password
        {
            set { _password = value; }
            get { return _password; }
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
        public string Class
        {
            set { _class = value; }
            get { return _class; }
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
        public string introduce
        {
            set { _introduce = value; }
            get { return _introduce; }
        }
        public string academy
        {
            set { _academy = value; }
            get { return _academy; }
        }
        public string groupID
        {
            set { _groupID = value; }
            get { return _groupID; }
        }
    }
}
