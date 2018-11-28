# MD
MD application. User can create personal account, login with Identity server (OAuth),
and create notes with images.
Consist of multiple projects and layers:

Data layer:
- MD.Data (base entities, repository interface, base db context)
- MD.Identity (MD.Data layer implementations, application entities)
- MD.Helpers (contains app constants)

Identity layer:
- MD.IdentityServer (Identity server connected to MS SQL database, implements OAuth)

Services:
- MD.CoreApi (provide access to Note CRUD functionality, require authorization grant from Identity Server)
- MD.RegistrationApi (provide functionality to register new user)

Clients:
web: 
- Angular 6 client (separated project)
 
mobile:
- Cross-platform mobile application on Xamarin Forms
 
Solution contains CoreMVC project with Unit tests
- This project develop is isolated from servers for now
