# Zadanie Testowe

Prosta symulacja agentów na planszy wykonana w ramach realizacji zadania testowego dla firmy Dream Parable.

# Założenia do realizacji zadania

Część I:
- Stworzyć fragment gry ("symulacji")
- Co parę sekund pojawia się nowy agent (losowe z zakresu od 2 do 10 sekund, kontrolowane z inspektora) 
- Maksymalnie powinno być nie więcej niż 30 agentów (ustawienie również kontrolowane z inspektora)
- Agenci poruszają się losowo na ograniczonej planszy (np. 10x10), ruch może być po linii prostej
- Jeżeli 2 agentów zetknie się ze sobą tracą po 1 punkcie życia (ze startowych 3)
- Jako gracze, możemy kliknąć na agenta (zaznaczyć go):
* na UI pojawia się jego nazwa i ilość HP - proste UI typu overlay, które powinno skalować się do różnych rozdzielczości
* możemy również odznaczyć agenta
* dobrze aby sam agent też miał jakąś formę zaznaczenia na sobie
- Grafika powinna być jedynie symboliczna (na przykład na figurach geometrycznych)
- Proszę założyć lokalne repozytorium GIT i commitować częściowe rozwiązania (min. 10 commitów aby można było podejrzeć proces tworzenia rozwiązania)

Część II:
- Dodatkowo po naciśnięciu przycisku na UI, w okienku tekstowym proszę wypisać:
    * Kolejno liczby od 1 do 100
    * Ale:
- jeżeli liczba jest podzielna przez trzy wypisać "Marko"
- jeżeli jest podzielna przez pięć wypisać "Polo"
- jednak jeżeli równocześnie podzielna przez trzy i pięć wypisać "MarkoPolo"

Proszę użyć Unity 2021.3.x
