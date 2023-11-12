using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep_2023.ExtendModels
{
    public class UserLoginReponseViewModel
    {
        public UserLoginReponseViewModel()
        {
            this.ID_Account = 0;
            this.UserName = "";
            this.FullName = "";
            this.Email = "";
            this.PhoneNumber = "";
            this.IsAdmin = false;
            this.erCode = 0;

        }
        public int erCode { get; set; }
        public int ID_Account { get; set; }
        public bool IsAdmin { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
