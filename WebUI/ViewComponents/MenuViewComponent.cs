using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebUI.Models;

namespace WebUI.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private IUserService _userService;

        public MenuViewComponent(IUserService userService)
        {
            _userService=userService;
        }

        public IViewComponentResult Invoke()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            MenuModel menuModel = new MenuModel();
            if (claim != null)
            {
                int userId = int.Parse(claim.Value);
                var result = _userService.GetUserInformation(userId);
                menuModel = new MenuModel()
                {
                    UserInformation = result.Success ? result.Data : null
                };
            }
            return View(menuModel);
        }
    }
}
