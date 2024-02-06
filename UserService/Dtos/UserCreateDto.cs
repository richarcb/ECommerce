namespace UserService.Dtos
{
    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
