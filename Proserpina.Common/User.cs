using System;
using System.ComponentModel.DataAnnotations;

namespace Proserpina.Common
{
    public class User
    {
        [Key]
        public int Id {set; get;}
        public string Username {set; get;}
        public string Password {set; get;}
        public string Firstname {set; get;}
        public string Lastname {set; get;}
    }
}
