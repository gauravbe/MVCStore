using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MVCStore.Data.Entities
{
    public class CustomerRole
    {
        [HiddenInput(DisplayValue = false)]
        public int RoleId { get; set; }

        [DataType(DataType.Text)]
        [StringLength(30, MinimumLength = 6)]
        public string RoleName { get; set; }
    }
}
