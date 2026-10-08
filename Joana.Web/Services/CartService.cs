using Joana.Web.Models;

namespace Joana.Web.Services;

public class CartService
{
    private readonly List<CartItemModel> _items = new();

    public IReadOnlyList<CartItemModel> Items => _items;
    public int ItemCount => _items.Sum(i => i.Kolicina);
    public decimal Total => _items.Sum(i => i.Ukupno);

    public event Action? OnChange;

    public void AddItem(int proizvodId, string naziv, decimal cena, int lager, int kolicina = 1)
    {
        if (lager <= 0) return;

        var existing = _items.FirstOrDefault(i => i.ProizvodId == proizvodId);
        if (existing is not null)
        {
            existing.Kolicina = Math.Clamp(existing.Kolicina + kolicina, 1, lager);
        }
        else
        {
            _items.Add(new CartItemModel
            {
                ProizvodId = proizvodId,
                Naziv = naziv,
                Cena = cena,
                Lager = lager,
                Kolicina = Math.Clamp(kolicina, 1, lager)
            });
        }

        OnChange?.Invoke();
    }

    public void UpdateKolicina(int proizvodId, int kolicina)
    {
        var item = _items.FirstOrDefault(i => i.ProizvodId == proizvodId);
        if (item is null) return;

        item.Kolicina = Math.Clamp(kolicina, 1, Math.Max(1, item.Lager));
        OnChange?.Invoke();
    }

    public void RemoveItem(int proizvodId)
    {
        _items.RemoveAll(i => i.ProizvodId == proizvodId);
        OnChange?.Invoke();
    }

    public void Clear()
    {
        _items.Clear();
        OnChange?.Invoke();
    }
}
