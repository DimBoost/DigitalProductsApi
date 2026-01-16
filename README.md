# DigitalProducts API (Portfolio-Projekt)

## Projektziel
Entwicklung einer Backend-API zur Verwaltung digitaler Produkte.  
Das Projekt demonstriert Fähigkeiten in:

- ASP.NET Core Web API mit C# / .NET 10 
- Identity & JWT Authentication mit Claims-basiertem Zugriff
- Entity Framework Core für Datenbankzugriff (SQL Server)
- DTOs, AutoMapper & FluentValidation für saubere Input-/Output-Strukturen
- Global Exception Handling für konsistente Fehlerbehandlung

---

## Aktueller Stand
- CRUD-Endpunkte für Products und Categories implementiert  
- DTOs und FluentValidation für Eingaben eingerichtet 
- AutoMapper Mapping Profiles erstellt und registriert  
- Exception Handling Middleware implementiert  
- SQL Server Migration erstellt, Verbindung in `appsettings.json` vorbereitet  
- Swagger UI verfügbar für Tests der Endpoints
- Use Case „Purchase eines Produkts“ implementiert (inkl. Preis zum Kaufzeitpunkt, Prüfung auf Verfügbarkeit und einmaligen Kauf pro User)

---

## Nächste Schritte
- Vollständige JWT-Authentifizierung und Policies prüfen
- Weitere Business-Logik / LINQ-Filter bei Bedarf  
- Backend Konfigurieren und vorbereiten für das Frontend (CORS)
- Frontend Anbindung Next.js

---

## Hinweise
- Das Projekt ist work in progress, daher sind noch nicht alle Features entwickelt
- Die Anwendung zeigt die aktuelle Fähigkeit in der Backend Entwicklung mit C#/.NET 10, ASP.NET Core
