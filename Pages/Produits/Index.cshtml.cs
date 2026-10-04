using GestionProduits.Data;
using GestionProduits.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GestionProduits.Pages.Produits;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;

    public IndexModel(AppDbContext context)
    {
        _context = context;
    }

    public List<Produit> Produits { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Recherche { get; set; }

    public int TotalStock => Produits.Sum(p => p.Stock);
    public decimal PrixMoyen => Produits.Count == 0 ? 0 : Produits.Average(p => p.Prix);

    public async Task OnGetAsync()
    {
        var requete = _context.Produits
            .Include(p => p.Categorie)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(Recherche))
        {
            requete = requete.Where(p => p.Nom.Contains(Recherche));
        }

        Produits = await requete
            .OrderBy(p => p.Nom)
            .ToListAsync();
    }
}