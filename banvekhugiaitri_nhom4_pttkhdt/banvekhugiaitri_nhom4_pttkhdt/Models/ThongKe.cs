namespace banvekhugiaitri_nhom4_pttkhdt.Models
{
    public class ThongKe
    {
        public int ticketId;

        public ThongKe(int ticketId)
        {
            this.ticketId = ticketId;
        }

        public String ticketName;
        public String ticketImg;

        public int soLuong;

        public ThongKe(int ticketId, String ticketName, string ticketImg)
        {
            this.ticketId = ticketId;
            this.ticketName = ticketName;
            this.ticketImg = ticketImg;
        }
    }
}
