Testfälle
=====

### ID: 	T01
Beschreibung: 	Benutzer anlegen
Vorbedingung: 	Das Programm ist im Registrierungsdialog
Test-Schritte: 	
1. Im Feld “Name” wird der neue Name des Benutzers eingegeben.
2. Im Feld “Passwort” wird ein passendes Passwort eingegeben.
3. Es wird auf den Button "anlegen" geklickt.
4. Die Regestrierung wird durchgeführt.
   
#### Erwartetes Resultat: 	
Das Benutzerkonto wir in der Datenbank anlegt und der Benutzer kann sich anmelden oder zum Beispiel seine Nutzerdaten ändern.
Dies wird im Menü “Passwort vergessen?” überprüft.


### ID: 	T02
Beschreibung: 	Reservierung anlegen
Vorbedingung: 	Das Programm ist im Dashboard bei dem Punkt Reservierung
Test-Schritte: 	
1. Im Feld “KundenID” wird die ID des entsprechenden Kunden eingegeben.
2. Im Feld “TicketID” wird nun das zu reservierende Ticket eingegeben.
3. Es wird auf den Button "anlegen" geklickt.
4. Die Reservierung wird durchgeführt.
     
#### Erwartetes Resultat: 	
Die Reservierung wird in der Datenbank hinzugefügt und kann nun geändert oder gelöscht werden.
Zudem reduziert sich die Anzahl des reservierten Tickets um 1 und die Anzahl der abwesenden Kunden erhöht sich um 1.


