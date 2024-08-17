using System;
using System.Collections.Generic;

namespace G_APIs.Models
{
    public class Menu
    {
        public List<ParentMenuVM> ParentMenus { get; set; }
        public List<SubMenuVm> SubMenus { get; set; }
        public UserInfoVM UserInfo { get; set; }
        public UserRoleVM UserRole { get; set; }
    }

    public class UserInfoVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDay { get; set; }
        public string FatherName { get; set; }
        public DateTime RegDate { get; set; }
        public string SedadInfo { get; set; }
        public int Status { get; set; }
        public int Gender { get; set; }
        public string NationalCardImage { get; set; }
    }

    public class UserRoleVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
    }

    public class ParentMenuVM
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuTitle { get; set; }
        public string MenuIcon { get; set; }
        public long RoleId { get; set; }
    }

    public class SubMenuVm
    {
        public int ActionId { get; set; }
        public string ActionName { get; set; }
        public string ActionPath { get; set; }
        public string ActionTitle { get; set; }
        public int? ParentMenuId { get; set; }
        public string ActionIcon { get; set; }
        public long RoleId { get; set; }
    }
}
