namespace PastryFlow.Domain.Enums;

public enum TrackingType
{
    Production = 0,   // Üretim ürünleri — talep ile gelir, kalan sayılır
    Purchased = 1,    // Satın alınan — satın alım ile stoğa girer, kalan sayılır
    Counter = 2       // Sayaç/jeton — stok tutulmaz, satılan adet girilir
}
