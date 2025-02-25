Testprotokolle
=====

### Durchgeführter Test: T01  
**Datum:** 25.02.2025  
**Tester:** Manon Ludt  
**Beobachtetes Resultat:**  
Der Benutzername 'Test' und das Passwort '1234' wurden in der Datenbank erfolgreich angelegt und ein Anmelden war dadurch möglich.  
**Testergebnis:**   
Das beobachtete Resultat hat nur teilweise mit dem erwarteten Resultat übereinstimmt, da ein Benutzer auch dann angelegt 
wurde, wenn keine Daten angegeben wurden. 
Dieser Fehler wurde jedoch behoben, indem nun in jedem Feld eine Eingabe getätigt werden muss, um eine Regestrierung des
Benutzers zuzulassen.


### Durchgeführter Test: T02
**Datum:** 25.02.2025
**Tester:** Manon Ludt
**Beobachtetes Resultat:** 
Die Reservierung wurde erfolgreich angelegt, indem eine gültige ReservierungsID, KundenID und TicketID eingegeben wurde.
Zudem ist es nun möglich diese zu löschen oder anzupassen.
**Testergebnis:** 
Das beobachtete Resultat hat nur teilweise mit dem erwarteten Resultat übereinstimmt, da Fehler noch vorhanden waren.
Es war möglich das Programm durch unerwartete eingaben in Form eines Strings zum Absturz zu bringen.
Dieser Fehler wurde jedoch behoben, indem String-Eingaben verweigert werden.





