# Car Maintenance
Proiectul are ca scop principal mentinerea unei baze de date cu masinile personale si intervalele de revizie.
Exista doua tipuri de utilizatori: Admin si Customer.
Admin-ul poate sa stearga utilizatorii noi care s-au logat si sa vada masinile pe care acestia le detin.
Customerii(clientii) pot sa adauge si sa stearga masini si sa modifice datele acestora.

## Noțiuni de bază

Aceste instrucțiuni vă vor oferi o copie a proiectului care poate fi pus în funcțiune pe mașina locală în scop de dezvoltare și testare. 

### Cerințe preliminare

Ce lucruri aveți nevoie pentru a instala software-ul și cum să le instalați

Instalati urmatoarele programe: 
```
Visual Studio 2019
Visual Studio Code
Microsoft SQL Server 2018
```

### Instalarea

O serie de exemple pas cu pas care îți spun cum să obții un build:

#### Backend(server-side)
1.Downloadati proiectul de pe github si dezarhivati-l
2.In Visual Studio, deschideti solutia care se afla la urmatoarea cale:
```
CarMaintenance-development\CarMaintenance-development\CarMaintenance\CarMaintenance.sln
```
3.Din Visual Studio dati click dreapta pe solutie -> Restore NuGet Packages.
4.Din Visual Studio dati click dreapta pe solutie -> Build Solutin.
5.Porniti SQL Server Management Studio. Logativa cu credidentialele default( sqlexpress, Windows Authentication ).
* Daca v-ati logat cu alt server, connection string-ul trebuie modificat ( din appsettings.json ) :
Connection string-ul default:
```"IdentityConnection": "Server=localhost\\sqlexpress; Database=UserDB; Trusted_Connection=True; MultipleActiveResultSets=True;"```

Connection string-ul nou:
```"IdentityConnection": "Server=SERVERUL_NOU; Database=UserDB; Trusted_Connection=True; MultipleActiveResultSets=True;"```

6.Deschideti Package Manager Console ( View -> Other Windows -> Package Manager Console) si rulati urmatoarele update-uri pentru baza de date:
```update-database -context AuthenticationContext
update-database -context CarContext```

7.Adaugati backup-ul din GIT pentru baza de date: backup-cm-2020.bak
*Click dreapta pe UserDB -> Task -> Restore -> Database
8.Daca s-a adaugat backup-ul, acesta are deja 2 useri creati, credidentialele pentru admin sunt:
```User: admin
Parola: adminn```
Si pentru customer:
```User: customer
Parola: customer```
9. Daca nu s-a dorit adaugarea backup-ului, pentru a crea un admin trebuie modificata linia 59 din metoda ```PostApplicationUser``` care se afla in controllerul ```ApplicationUserController```

Initial userii creati sunt customeri. Daca modificam linia cu urmatoare se creeaza admini.
```applicationUserModel.Role = "Admin";```

Apoi trebuie adaugate 2 linii in tabela dbo.AspNetUserRoles ca in imaginea de mai jos: 
![alt text](https://i.imgur.com/ZFPClBH.png)
10. Rulati solutia in debug( e default setat ).
Daca s-a deschis o pagina cu url-ul http://localhost:52672/api/values si continutul "["value1","value2"]", atunci solutia a rulat cu succes.

#### Frontend(client-side)
1.Deschideti cu Visual Studio Code folderul Angular7-CarMaintenance
2.Deschideti un terminal(Terminal -> New Terminal) si rulati urmatoarele comenzi:
```npm install
ng serve --o```

Aplicatia se deschide si va puteti loga ca si customer sau admin daca ati adaugat backup-ul.


## Rularea testelor

Deschideti solutia, click dreapta pe proiectul CarMaintenanceUnitTests apoi Run Tests.

### Descriere pagini si functionalitati
Din pagina de start aveti doua optiuni: logarea si inregistrarea.
Daca nu se modifica din cod linia despre care am vorbit la pasul 9 de la Backend(server-side), se pot crea doar useri cu rolul Customer.
Adminii au urmatoarele functionalitati: Pot sterge si vizualiza detalii despre useri.
Customerii au urmatoarele functionalitati:
- pot adauga o masina noua 
- pot modifica datele despre masina
- pot modifica datele la care trebuie facute inspectiile
- pot sterge masina si datele aferentepot
- pot vedea intervalele(km/zile) la care trebuie facute urmatoarele interventii.

Datele disponibile despre fiecare masina care sunt calculate la Calendarul de service sunt:
- rovinieta
- ITP
- asigurare
- revizie (km+zile)



