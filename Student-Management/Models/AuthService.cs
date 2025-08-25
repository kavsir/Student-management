using Microsoft.EntityFrameworkCore;

namespace Student_Management.Models;

public class AuthService
{
    private readonly QuanLyHocSinhContext _context;

    public AuthService(QuanLyHocSinhContext context)
    {
        _context = context;
    }

    public async Task<Taikhoan?> AuthenticateAsync(LoginViewModel model)
    {
        return await _context.Taikhoans
            .FirstOrDefaultAsync(x =>
                x.TenDangNhap == model.TenDangNhap &&
                x.MatKhau == model.MatKhau); // TODO: mã hóa sau
    }
}
