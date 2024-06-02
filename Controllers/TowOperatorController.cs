using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TowFinder.Data;
using TowFinder.ViewModels;
using TowFinder.Models;


public class TowOperatorController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;

    public TowOperatorController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public IActionResult Register()
    {
        _signInManager.SignOutAsync(); // E�er giri� yap�lm��sa ��k�� yap
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(TowOperatorRegisterViewModel towOperatorUser)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _userManager.FindByNameAsync(towOperatorUser.Username);

            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Kullan�c� ad� zaten al�nm��.");
                return View(towOperatorUser);
            }



            if (towOperatorUser.Username.ToLower() == "admin")
            {
                ModelState.AddModelError("Username", "'admin' kullan�c� ad� kullan�lamaz.");
                return View(towOperatorUser);
            }

            var user = new IdentityUser
            {
                UserName = towOperatorUser.Username,
                PhoneNumber = towOperatorUser.Phone
            };

            var result = await _userManager.CreateAsync(user, towOperatorUser.Password);


            if (result.Succeeded)
            {
                var towOperator = new TowOperator
                {
                    Name = towOperatorUser.Name,
                    Phone = towOperatorUser.Phone,
                    City = towOperatorUser.City,
                    District = towOperatorUser.District,
                    Username = towOperatorUser.Username,
                    TowOperatorID = user.GetHashCode(),
                    ApprovalStatus = false,
                };
                await _userManager.AddToRoleAsync(user, "TowOperator");
                _context.TowOperators.Add(towOperator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Success");
            }else
           


            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Ge�ersiz giri�.");
        }
        return View();
    }



    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded)
                {
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Administrator");
                    }
                    else if (await _userManager.IsInRoleAsync(user, "TowOperator"))
                    {
                        var towOperator = await _context.TowOperators.FirstOrDefaultAsync(t => t.Username == model.Username);
                        if (towOperator != null)
                        {
                            return RedirectToAction("Profile", new { id = towOperator.TowOperatorID });
                        }
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Ge�ersiz kullan�c� ad� veya �ifre.");
        }
        return View(model);
    }

    // logout
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }


    public IActionResult Success()
    {
        return View();
    }



    [HttpGet("TowOperator/Profile/{id}")]
    public async Task<IActionResult> Profile(int id) // verilen id'ye sahip TowOperator'�n profilini g�ster (sadece kendi profilini g�rebilir)
    {
        var towOperator = await _context.TowOperators.FindAsync(id);
        if (towOperator == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);
        if (user == null || towOperator.Username != user.UserName)
        {
            return Forbid(); // Yetkisiz eri�im
        }

        var model = new EditProfileViewModel
        {
            Name = towOperator.Name,
            Phone = towOperator.Phone,
            City = towOperator.City,
            District = towOperator.District,
            ApprovalStatus = towOperator.ApprovalStatus ? "Onayland�" : "Onay Bekliyor"
        };

        return View(model);
    }




    // Profil g�r�nt�leme
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var towOperator = await _context.TowOperators
            .FirstOrDefaultAsync(t => t.Username == user.UserName);

        if (towOperator == null)
        {
            return NotFound();
        }
        var model = new EditProfileViewModel
        {
            Name = towOperator.Name,
            Phone = towOperator.Phone,
            City = towOperator.City,
            District = towOperator.District,
            ApprovalStatus = towOperator.ApprovalStatus ? "Onayland�" : "Onay Bekliyor"
        };
        return View(model);
    }


    // Profil d�zenleme sayfas�n� g�ster
    [HttpGet]
    public async Task<IActionResult> EditProfile()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return NotFound();
        }

        var towOperator = await _context.TowOperators
            .FirstOrDefaultAsync(t => t.Username == user.UserName);

        if (towOperator == null)
        {
            return NotFound();
        }

        var model = new EditProfileViewModel
        {
            Name = towOperator.Name,
            Phone = towOperator.Phone,
            City = towOperator.City,
            District = towOperator.District,
            ApprovalStatus = towOperator.ApprovalStatus?"Onayland�" : "Onay Bekliyor"
        };

        return View(model);
    }

    // Profil d�zenleme i�lemi
    [HttpPost]
    public async Task<IActionResult> EditProfile(EditProfileViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var towOperator = await _context.TowOperators
                .FirstOrDefaultAsync(t => t.Username == user.UserName);

            if (towOperator == null)
            {
                return NotFound();
            }

            towOperator.Name = model.Name;
            towOperator.Phone = model.Phone;
            towOperator.City = model.City;
            towOperator.District = model.District;
            towOperator.ApprovalStatus = false;

            await _context.SaveChangesAsync();

            return RedirectToAction("Profile");
        }

        return View(model);
    }



}
