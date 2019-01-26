using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MulitTenant.Sample.Data;
using MulitTenant.Sample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MulitTenant.Sample.Controllers
{
    public class HomeController : Controller
    {

        private readonly MuliTenantDbContext _muliTenantDbContext;

        public HomeController(MuliTenantDbContext muliTenantDbContext)
        {
            _muliTenantDbContext = muliTenantDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Bob()
        {          
            var userClaims = new List<Claim>();
            userClaims.Add(new Claim("TenantId", "1"));
            userClaims.Add(new Claim("UserId", "1"));
            userClaims.Add(new Claim("UserName", "Bob"));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction(nameof(MyPosts));
        }

        public async Task<IActionResult> Alice()
        {
          
            var userClaims = new List<Claim>();
            userClaims.Add(new Claim("TenantId", "2"));
            userClaims.Add(new Claim("UserId", "2"));
            userClaims.Add(new Claim("UserName", "Alice"));
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userClaims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction(nameof(MyPosts));
        }

        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> MyPosts()
        {
            return View(await _muliTenantDbContext.Posts.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> AddPost()
        {
            var ddd = DateTime.Now.ToString();
            _muliTenantDbContext.Posts.Add(new Post
            {

                Name = ddd,
                Content = ddd
            });
            await _muliTenantDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(MyPosts));           
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
