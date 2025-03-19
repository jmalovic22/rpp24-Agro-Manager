[![Open in Codespaces](https://classroom.github.com/assets/launch-codespace-2972f46106e565e64193e422d61a12cf1da4916b45550586e14ef0a7c637dd04.svg)](https://classroom.github.com/open-in-codespaces?assignment_repo_id=16601795)

# AgroManager

## Projektni tim

Ime i prezime | E-mail adresa (FOI) | JMBAG | Github korisničko ime
------------  | ------------------- | ----- | ---------------------
Filip Markić | fmarkic22@foi.hr | 0016157981 | filipmarkic
Antonio Martinaga | amartinag22@foi.hr | 0016160739 | amartinag22
Jakov Malović | jmalovic22@foi.hr | 0035238861 | jmalovic22
David Šimičević | dsimicevi21@foi.hr | 0016153711 | dsimicevi21
## Opis domene
AgroManager je sveobuhvatna aplikacija za upravljanje poljoprivrednim gospodarstvima, osmišljena kako bi omogućila vlasnicima i menadžerima većih farmi i ratarstava potpunu kontrolu nad svim aspektima poslovanja. Aplikacija omogućuje jednostavno upravljanje poljima, farmama, silosima, vozilima, opremom te zaposlenicima, čime se povećava učinkovitost i olakšava svakodnevno poslovanje. Uz AgroManager, korisnici mogu pratiti stanje svih svojih resursa u stvarnom vremenu, donositi informirane odluke i brzo dodjeljivati posla zaposlenicima. Sve to mogu učiniti bez potrebe za fizičkim sastancima ili obilascima, što omogućuje bolje korištenje vremena i resursa. Aplikacija također nudi mogućnost praćenja izvršenja radnih naloga, povijesti uzgoja na poljima, osiguravajući cjelovit pregled poslovanja na jednom mjestu. Ovime donosimo digitalizaciju u svakodnevno upravljanje poljoprivredom, omogućujući optimizaciju radnih procesa, smanjenje troškova te povećanje profitabilnosti.

## Specifikacija projekta

Oznaka | Naziv | Kratki opis | Odgovorni član tima
------ | ----- | ----------- | -------------------
F01 | Prijava korisnika i početna stranica | Omogućava prijavu korisnika u sustav, što je ključno da bi korisnik pristupio ostatak funkcionalnosti aplikacije. Kako bi se korisnik prijavio mora unijeti OTP koji dobije skeniranjem QR koda. Nakon uspješne prijave, aplikacija korisnika prebacuje na početnu stranicu. Početna stranica će se razlikovati ovisno o tome koji se korisnik prijavio. Na početnoj stranici će biti prikazani podaci koji su najrelevantniji za prijavljenog korisnika. | Jakov Malović
F02 | Upravljanje silosima | Omogućava administraciju silosa unutar sustava. Administratori i vlasnik mogu dodavati nove silose u bazu podataka unosom ključnih informacija, pregledavati detalje o svakom silosu, uređivati podatke poput kapaciteta, lokacije ili statusa, te po potrebi brisati silose iz sustava. | Filip Markić
F03 | Praćenje kapaciteta silosa |Funkcionalnost praćenja kapaciteta silosa osigurava korisnicima uvid u trenutnu razinu popunjenosti svakog silosa i preostali raspoloživi kapacitet te uređivanje istog. Čime se optimizira iskorištenje prostora i smanjuje rizik od preopterećenja ili neiskorištenosti silosa. | David Šimičević
F04 | Upravljanje farmama | Omogućava administraciju farmi unutar sustava. Administratori i vlasnik mogu dodavati nove farme, uređivati postojeće informacije o farmama te brisati farme koje više nisu u funkciji. | Filip Markić
F05 | Praćenje kapaciteta farme | Ova funkcionalnost omogućava praćenje kapaciteta farmi u pogledu broja životinja, vrsti životinja, broja zaposlenih. Korisnici mogu vidjeti trenutno stanje i kapacitete farme, pretraživati farme, dobiti detaljan pregled podataka o svakoj od farmi. | Filip Markić
F06 | Upravljanje zaposlenicima | Obuhvaća dodavanje i brisanje zaposlenika u sustav, kao i pregled i uređivanje njihovih osnovnih podataka poput kontakt informacija. | Antonio Martinaga
F07 | Upravljanje poslovima | Pokriva dodavanje novih poslova, uređivanje i brisanje postojećih te pregled i pretraživanje poslova. Ovime omogućujemo lako dodjeljivanje poslova za sekciju radnih naloga. | Antonio Martinaga
F08 | Upravljanje radnim nalozima | Ova funkcionalnost obuhvaća dodavanje, brisanje, uređivanje i pregled radnih naloga. Ovime možemo dodijeliti radne naloge pomoću poslova koji su zapisani u bazi te jasno definiraju što zaposlenici trebaju raditi.| Antonio Martinaga
F09 | Upravljanje vozilima i priključcima | Aplikacija će omogućiti praćenje vozila i priključaka unutar sustava. Administratori i vlasnik mogu dodavati nova vozila i priključke u bazu podataka unosom ključnih informacija, pregledavati detalje o svakom vozilu i priključke, ažurirati podatke o vozilima i priključcima, te po potrebi brisati vozila i priključke iz baze podataka. Cilj je omogućiti efikasno upravljanje vozilima i priključcima u stvarnom vremenu. | Jakov Malović
F10 | Zauzimanje vozila i priključaka | Zaposlenik može odabrati jedno od dostupnih vozila. Ako je vozilo dostupno aplikacija će da prebaciti na formu sa priključcima gdje će moći spojiti i odspojiti odabrani priključak sa vozila kojeg je zauzeo | Jakov Malović
F11 | Upravljanje poljima | Funkcionalnost upravljanja poljima omogućuje administratorima i vlasnicima detaljno upravljanje svim poljima na farmi. Kroz ovu funkcionalnost, administratori i vlasnik mogu pretraživati postojeća polja, dodavati nova, te unositi ključne informacije kao što su veličina polja, geografska lokacija, tip tla, te specifične poljoprivredne karakteristike. Ova funkcionalnost također uključuje mogućnost brisanja polja koja nisu u upotrebi, a interaktivna karta (biti će dodana kasnije) omogućuje vizualnu identifikaciju svih polja, što olakšava njihovu koordinaciju i praćenje stanja na terenu. | David Šimičević
F12 | Praćenje povijesti polja | Funkcionalnost praćenja povijesti polja omogućuje korisnicima detaljan pregled svih prethodnih aktivnosti i kultura na svakom polju. Ova funkcionalnost omogućava bilježenje i pregled važnih informacije kao što su datumi sadnje i žetve, korištene tehnike obrade tla itd. Praćenje tih podataka pomaže u analizi učinka različitih kultura na kvalitetu tla i ukupnu rodnost. Ovi podaci također omogućuju bolje planiranje rotacije kultura, smanjujući rizik od iscrpljivanja tla i razvoja specifičnih bolesti. Na temelju tih informacija, administratori i vlasnici mogu donositi informirane odluke za optimizaciju rodnosti i održivosti poljoprivrednih operacija, čime se poboljšava dugoročna produktivnost farmi. | David Šimičević

## Tehnologije i oprema
Projekt se razvija korištenjem programskog jezika C# i temelji se na .Net Framework razvojnom okviru. Implementacija programa odvija se putem WPF aplikacije, što omogućuje kreiranje modernijeg korisničkog sučelja u odnosu na WindowsForms. Za razvoj softvera upotrebljavamo alate poput GitHub-a i gita za verzioniranje koda. Tehnička dokumentacija i projektna dokumentacije pišu se unutar GitHub Wiki-a, dok za upravljanje zadacima i projektima koristimo GitHub Projects. Svaki član tima koristi vlastita računala i odgovarajuću periferiju za izradu projekta.



