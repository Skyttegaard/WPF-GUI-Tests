# GUI-til-test-program
WPF Program til at læse scripts & tekstfiler

Programmet starter ud med at man kan tilføje sin filepath.

Herefter åbner så et vindue hvor man kan tilføje de computere man skal køre disse scripts igennem. 

Hver computer kan have 5 sæt som har hver deres timer.

Der kan kun køres 1 script af gangen per pc.


Programmet opretter en folder ved navn DATA. Denne indeholder 4 filer:

Clients.json:
Denne indeholder IP Adresser & PC Navne som (måske) bliver brugt senere.

CurrentJob.txt:
Denne tekstfil indeholder navnet på det nuværende job.
//Virker midlertidigt kun med den seneste valgte computers job.

SetNumber.txt:
Denne tekstfil indeholder tallet på det nuværende sæt efter man har startet en opgave.
//Virker midlertidigt kun med den seneste valgte computers job.

FilePath.txt:
Denne tekstfil indeholder filepath til folderen som opgaverne ligger i. File path skal være som følgende.
(MainFolder skal være den man vælger)

MainFolder--> Forløb --> Kategori --> Opgave --> Opgave filer

EKS: C:\MainFolder\
(Indeholder så resten af folders her)

Lav en genvej til GUI-TIL-TEST-PROGRAM.exe

Kør denne for at starte programmet.

