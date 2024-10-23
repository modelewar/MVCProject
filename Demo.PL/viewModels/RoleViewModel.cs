using System;

namespace Demo.PL.viewModels
{
    public class RoleViewModel
    {
        public string id { get; set; }
        public string RoleName { get; set; }

        public RoleViewModel()
        {
                id = Guid.NewGuid().ToString();
        }
    }
}
