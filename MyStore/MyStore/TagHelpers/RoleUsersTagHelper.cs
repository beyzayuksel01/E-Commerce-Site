using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using MyStore.Models;
using System.Collections.Generic;


namespace MyStore.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "asp-for-users")]
    public class RoleUsersTagHelper : TagHelper
    {

        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public RoleUsersTagHelper(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HtmlAttributeName("asp-for-users")]
        public string RoleId { get; set; } = null!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var userNames = new List<string>();
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null && role.Name != null)
            {
                var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
                foreach (var user in usersInRole)
                {
                    userNames.Add(user.UserName ?? "");
                }
                output.Content.SetHtmlContent(userNames.Count == 0 ? "Kullanıcı Yok" : setHtml(userNames));
            }
        }


        //public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        //{
        //    var userNames = new List<string>();
        //    var role = await _roleManager.FindByIdAsync(RoleId);

        //    if (role != null && role.Name != null)
        //    {
        //        foreach (var user in _userManager.Users)
        //        {
        //            if (await _userManager.IsInRoleAsync(user, role.Name))
        //            {
        //                userNames.Add(user.UserName ?? "");
        //            }
        //        }
        //        output.Content.SetHtmlContent(userNames.Count == 0 ? "Kullanıcı Yok" : setHtml(userNames));
        //    }
        //}

        private string setHtml(List<string> userNames)
        {
            var html = "<ul>";

            foreach (var item in userNames)
            {
                html += "<li>" + item + "</li>";
            }
            html += "</ul>";
            return html;
        }
    }
}
