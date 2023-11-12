using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep_2023.ExtendModels
{
    public class ChangePasswordModel
    {
        public ChangePasswordModel()
        {
            this.UserName = "";
            this.PassWord = "";
            this.PassWord_New = "";
            this.Changer = "";
        }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Changer { get; set; }
        public string PassWord_New { get; set; }

    }
}
