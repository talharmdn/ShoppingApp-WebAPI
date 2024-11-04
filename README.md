# ShoppingApp

ShoppingApp, satıcılar ve alıcılar için alışveriş işlevselliği sunan bir web uygulamasıdır. Bu proje, C#, ASP.NET Core ve Entity Framework kullanılarak geliştirilmiştir.
Proje katmanlı mimari ile tasarlanmış olup, kullanıcı kimlik doğrulaması, rol yönetimi, sepet ve sipariş işlevselliklerini içerir.

## İçindekiler
- [Özellikler](#özellikler)
- [Kullanılan Teknolojiler](#kullanılan-teknolojiler)
- [Kurulum](#kurulum)
- [Proje Yapısı](#proje-yapısı)
- [Kullanım](#kullanım)

## Özellikler
- **Kullanıcı Kimlik Doğrulama ve Rol Yönetimi:** Kullanıcılar "Customer" (Alıcı) veya "Seller" (Satıcı) rolleriyle kayıt olabilir.
- **Ürün Yönetimi:** Satıcılar yeni ürünler oluşturabilir, güncelleyebilir ve silebilir.
- **Sepet ve Sipariş Yönetimi:** Alıcılar ürünleri sepete ekleyebilir ve sipariş oluşturabilir.
- **Entity Framework Core ile Veritabanı Yönetimi:** Veritabanı işlemleri için EF Core kullanılmıştır.
- **Dependency Injection (DI) ve Katmanlı Mimari:** Kod modüler ve ölçeklenebilir olacak şekilde tasarlanmıştır.

## Kullanılan Teknolojiler
- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [ASP.NET Core Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [AutoMapper](https://automapper.org/) (DTO eşlemeleri için)
- [Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) (Kullanıcı kimlik doğrulama)

## Kurulum

1. **Projeyi Kopyalayın:**
   ```bash
   git clone https://github.com/kullaniciadi/ShoppingApp.git
   cd ShoppingApp
