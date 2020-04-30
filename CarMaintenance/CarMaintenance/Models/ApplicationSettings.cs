
namespace CarMaintenance.Models
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
        //in days
        public int Token_Expiration { get; set; }
    }
}
