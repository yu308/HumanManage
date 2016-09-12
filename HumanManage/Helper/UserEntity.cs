
using System;
using System.Text;

namespace HumanManage.Helpers
{
    public class UserEntity
    {
        public UserEntity()
        { }
        // Ticket.
        private string _ticket;

        // UserName.
        private string _username;

        // TrueName.
        private string _truename;

        // Role.
        private string _roleid;

        // Last refresh time of page.
        private DateTime _refreshtime;

        // Last active time of user.
        private DateTime _activetime;

        // Ip address of user.
        private string _clientip;    

        public string Ticket
        {
            get
            {
                return _ticket;
            }
            set
            {
                _ticket = value;
            }
        }
        public string UserName
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }
        public string TrueName
        {
            get
            {
                return _truename;
            }
            set
            {
                _truename = value;
            }
        }
        public string RoleID
        {
            get
            {
                return _roleid;
            }
            set
            {
                _roleid = value;
            }
        }
        public DateTime RefreshTime
        {
            get
            {
                return _refreshtime;
            }
            set
            {
                _refreshtime = value;
            }
        }
        public DateTime ActiveTime
        {
            get
            {
                return _activetime;
            }
            set
            {
                _activetime = value;
            }
        }
        public string ClientIP
        {
            get
            {
                return _clientip;
            }
            set
            {
                _clientip = value;
            }
        }
    }
}