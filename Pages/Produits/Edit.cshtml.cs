using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

public class EditModel : PageModel
{
    private readonly AppDbContext _context;

    public EditModel(AppDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Produit Produit { get; set; } = default!;

    public SelectList Categories { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Produit = await _context.Produits.FindAsync(id);

        if (Produit == null)
        {
            return NotFound();
        }

        await ChargerCategories();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        ModelState.Remove("Produit.Categorie");

        if (!ModelState.IsValid)
        {
            await ChargerCategories();

            return Page();
        }

        _context.Attach(Produit).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Produits.AnyAsync(p => p.Id == Produit.Id))
            {
                return NotFound();
            }

            throw;
        }

        return RedirectToPage("./Index");
    }

    private async Task ChargerCategories()
    {
        Categories = new SelectList(
            await _context.Categories.OrderBy(c => c.Nom).ToListAsync(),
            "Id",
            "Nom",
            Produit?.CategorieId);
    }
}