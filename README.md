# Schedify-Backend

Schedify-Backend, etkinlik yönetimi için API sunan bir backend uygulamasıdır. Kullanıcılar, etkinliklerini oluşturabilir, düzenleyebilir, silebilir ve listeleyebilirler. 
Backend, etkinliklerin yönetimi için gerekli tüm iş mantığını ve veritabanı etkileşimini sağlar.

## Hedef

Schedify-Backend, kullanıcıların etkinliklerini planlayıp takip edebilecekleri basit bir API sağlayarak etkinlik yönetimini kolaylaştırmayı amaçlamaktadır. 
Uygulama, kullanıcı doğrulaması, veri validasyonu ve hata yönetimi gibi temel özellikleri de içerir.

## Temel Özellikler

- **CRUD işlemleri**: Etkinlik oluşturma, okuma, güncelleme ve silme işlemleri yapılabilir.
- **Validasyon**: Etkinlikler için gerekli tüm validasyonlar yapılır.
- **Hata Yönetimi**: Global hata yönetimi ile uygulama hataları standartlaştırılmış şekilde işlenir.
- **API**: RESTful API kullanarak frontend ile etkileşime girer.

## Teknolojiler

- **Backend Framework**: .NET (C#)
- **Veritabanı**: SQL Server (Entity Framework Core)
- **Diğer**: FluentValidation, AutoMapper, ASP.NET Core

## Kullanıcı İstekleri

- **Etkinlik oluşturma**: Kullanıcılar, başlık, açıklama ve tarih gibi bilgileri içeren etkinlikler oluşturabilirler.
- **Etkinlik düzenleme**: Mevcut etkinlikler düzenlenebilir.
- **Etkinlik silme**: Kullanıcılar kendi etkinliklerini silebilirler.
- **Etkinlik listeleme**: Tüm etkinlikler listelenebilir.

Schedify-Backend, etkinlik yönetim sistemlerini kolaylaştırmayı hedefler ve gelişmiş hata yönetimi, veri doğrulama gibi temel işlevleri sağlar.
