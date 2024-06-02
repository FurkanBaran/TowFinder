using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TowFinder.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

[Authorize(Roles = "Admin")]
public class AdministratorController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    public AdministratorController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var towOperators = await _context.TowOperators.ToListAsync();
        return View(towOperators);
    }

    public async Task<IActionResult> Approve(int id)
    {
        var towOperator = await _context.TowOperators.FindAsync(id);
        if (towOperator != null)
        {
            towOperator.ApprovalStatus = true;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }



    public async Task<IActionResult> Delete(int id)
    {
        var towOperator = await _context.TowOperators.FindAsync(id);
        if (towOperator != null)
        {
            await _userManager.DeleteAsync(await _userManager.FindByNameAsync(towOperator.Username));
            _context.TowOperators.Remove(towOperator);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    // logout

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}
