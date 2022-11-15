namespace TaxaHandler.Repository;

public class BookingRepository 
{
    public BookingRepository(){}
    private List<BookingDTO> bookings = new();

    public void AddBooking(BookingDTO newBooking) 
    {
        bookings.Add(newBooking);
    }    
    public List<BookingDTO> GetBookings() 
    {
        return bookings.OrderBy(x => x.TidStempel).ToList();
    }
}