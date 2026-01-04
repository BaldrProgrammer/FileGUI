namespace FileGUI.DTO.Auth;

public class UserGetDto
{
    public int id { get; set; }
    public string username { set; get; }
    public string hashed_password { set; get; }
    public bool is_admin { set; get; }
}