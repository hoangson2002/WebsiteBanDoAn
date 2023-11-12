using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep_2023.ExtendModels
{
    public class UserLoginRequestViewModel
    {
        public UserLoginRequestViewModel()
        {
            this.UserName = "";
            this.PassWord = "";
        }
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
