namespace AdminApi.DTOs
{
    public class AdminDTO
    {
        public required int NIC { get; set; }
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public required string Email { get; set; }
    }
}
