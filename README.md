# ISNemocnice
WPF aplikace využívající WCF DataService. Školní projekt ze střední.

![Preview](http://i.imgur.com/OI2CMY2.png)

Funkce:
 - přihlašení/odhlášení/registrace účtu
 - role/práva účtů
 - prohlížení a editace pacientů v "ordinaci"
 - konzole v pozadí
 - splashscreen
 - a další

 Zde najdete starší verzi projektu, poslední jsem nenašel. Ta je skoro stejná ale ještě jsem dodělal:


 - větší bezpečnost, zejména:
  - validace vstupu na sql serveru pomocí update trigger
  - omezení oprávnění číst/zapisovat v databázi
  - ověření přihlašování pomocí skriptu na sql serveru
 - odstranění testovacích stránek a tabulek
 - "demo" mód - pouze prohlížení lékařů, "cenzura" pacientů
 - dodělání validace vstupu v "klientu"
 - volitelný logging do souboru, s omezení maximální velikosti, automatickým zálohováním logů, a prohlížečem logů který barevně zvýrazňuje "bezpečnostní upozornění" (vlastně jenom nepodařené pokusy o přihlášení)
 - prevence ukončení aplikace v globálně nastavené "pracovní době"
 - (ne úplně funkční) graf počtu dotazů na server

 jo a ještě hezčí splash screen ve stylu microsoft office 2k13 :)

 A dvě načaté a nedodělané věci:
 - textový mód přes ssh, podařilo se mi udělat druhý projekt kde byl "blbý terminál" a tomu jsem posílal text, a on zase uměl vracet scancode zmáčklé klávesy. ukázal tabulku účtů a brzy po tom jsem to zanechal
 - udělal jsem hezký mockup gui ve photoshopu, když jsem byl spokojený tak jsem udělal jsem projekt v microsoft blend a pak jsem zjistil že přenést to do starého projektu nejde jen tak a vzdal to
