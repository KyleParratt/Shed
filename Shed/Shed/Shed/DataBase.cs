using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace Shed
{
    class DataBase
    {

    }
    public class UsersDB : RealmObject
    {
        public string Emp_ID { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string assigned { get; set; }
        public bool Approved { get; set; }
    }

    public class UserJobs : RealmObject
    {
        public string Job_Num { get; set; }
        public string Job_Title { get; set; }
        public string Job_Descr { get; set; }
        public string Job_Status { get; set; }
        public string Job_AssignedTo { get; set; }
        public string Job_Start { get; set; }
        public string Job_End { get; set; }
    }

    }
