# Heizungssteuerung-Client

## Allgemeine Informationen

Der Heizungssteuerung Client beinhaltet die **gesamte GUI** für unsere Heizungssteuerung. 
Darunter fallen sämtliche Objekte, die der Nutzer sehen und steuern kann. 

Der Ordner für den Client besteht wiederum aus einigen Unterordern, welche alle im Detail in dieser Dokumentation an den entsprechenden Stellen erklärt werden.

![Heizungssteuerung_Client_Overview.png](/resources/Heizungssteuerung_Client_Overview.png)

Die GUI wurde mithilfe des Tools **AvaloniaUI** aufgebaut.<br>
Die einzelnen Seiten, welche der User sehen kann, werden als Views bezeichnet.<br>
Die einzelnen Views sind im XML-Format geschrieben und sämtliche Anpassungen wurden während der Bearbeitungsphase in Echtzeit angezeigt.

Mithilfe unseres verwendeten Tools **AvaloniaUI** steht die Benutzeroberfläche neben einer Desktop-Anwendung auch als Webseite, als Android-App und IOS-App zur Verfügung:

![Heizungssteuerung_Client_OS.ong](/resources/Heizungssteuerung_Client_OS.png)

Die entsprechenden Objekte dazu erben sämtliche Änderungen vom Objekt "Heizungssteuerung_Client".
