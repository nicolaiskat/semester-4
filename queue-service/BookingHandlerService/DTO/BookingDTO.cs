public class BookingDTO {
    public BookingDTO(string navn, string afhentningsAddresse, string destinationsAddresse, DateTime startTidspunkt)
    {
        ID = null;
        TidStempel = null;
        Navn = navn;
        AfhentningsAddresse = afhentningsAddresse;
        DestinationsAddresse = destinationsAddresse;
        StartTidspunkt = startTidspunkt;
    }
    
    public Int32? ID { get; set; }
    public DateTime? TidStempel { get; set; }
    public String Navn { get; set; }
    public String AfhentningsAddresse { get; set; }
    public String DestinationsAddresse { get; set; }
    public DateTime StartTidspunkt { get; set; }

    public override string ToString() {
        return "{ " + $"ID: {ID}, TidStempel: {TidStempel}, Navn: {Navn}, AfhentningsAddresse: {AfhentningsAddresse}, DestinationsAddresse: {DestinationsAddresse}, StartTidspunkt: {StartTidspunkt}" + " }"; 
    }
}