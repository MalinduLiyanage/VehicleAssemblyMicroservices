namespace AdminApi.DTOs.Requests.EmailRequests
{
    public class AdminAccountEmailRequestDTO
    {
        public int? NIC { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
