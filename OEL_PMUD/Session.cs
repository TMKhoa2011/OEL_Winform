using OEL_PMUD.Models;

namespace OEL_PMUD
{
    public static class Session
    {
        // Lưu trữ thông tin User đang đăng nhập hiện tại
        public static User CurrentUser { get; set; }
    }
}