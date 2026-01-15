# DigitalProducts API (Portfolio-Projekt)

## Projektziel
Entwicklung einer sauberen und wartbaren Backend-API zur Verwaltung digitaler Produkte.  
Das Projekt demonstriert folgende Skills:

- Clean Architecture (Controllers, Services, DTOs) 
- Datenbankanbindung mit Entity Framework Core
- Datenvalidierung** mit FluentValidation
- JWT Authentication & Claims-basierte Autorisierung
- Object Mapping mit AutoMapper
- Globales Exception Handling

---

## Aktueller Stand
- CRUD-Endpunkte für **Products** und **Categories** implementiert  
- DTOs & Validatoren erstellt  
- AutoMapper-Mapping-Profiles erstellt und registriert  
- FluentValidation für Eingaben aktiv  
- Exception Handling Middleware implementiert  
- SQL Server Migration erstellt, Verbindung in `appsettings.json` vorbereitet  
- Swagger UI verfügbar (für Tests der Endpoints)

---

## Nächste Schritte
- JWT-Authentifizierung aktivieren und Claims prüfen  
- Erweiterte Business-Logik / LINQ Filter implementieren  
- Purchase Tabelle & User Identity implementieren 
- Frontend Anbindung Next.js

---

## Hinweise
- Das Projekt ist work in progress, daher noch nicht alle Features entwickelt
- Die Architektur zeigen die aktuelle Fähigkeit im Backend-Design und C#/.NET 10
