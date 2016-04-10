using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EWSD.Domain
{
    public class Staff
    {
        public int staffId { get; set; }
        public string username { get; set; }
        public string pw { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string securityQuestion { get; set; }
        public string securityAnswer { get; set; }
        public DateTime creationDate { get; set; }
        public DateTime lastLoginDate { get; set; }
        public byte userRole { get; set; }

        public Staff()
        {

        }

        public Staff(int staffId, string username, string pw, string firstName, string lastName, string email, string securityQuestion, string securityAnswer, DateTime creationDate, DateTime lastLoginDate, byte userRole)
        {
            this.staffId = staffId;
            this.username = username;
            this.pw = pw;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.securityQuestion = securityQuestion;
            this.securityAnswer = securityAnswer;
            this.creationDate = creationDate;
            this.lastLoginDate = lastLoginDate;
            this.userRole = userRole;
        }
    }
}