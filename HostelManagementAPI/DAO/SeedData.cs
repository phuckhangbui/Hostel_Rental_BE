using BusinessObject.Models;
using DTOs.Account;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DAO;

public class SeedData
{
    public static async Task SeedAccount(DataContext context)
    {
        if (await context.Account.AnyAsync())
        {
            return;
        }

        var accountData = await File.ReadAllTextAsync("AccountSeedData.json");

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var accounts = JsonSerializer.Deserialize<List<Account>>(accountData);

        foreach (var account in accounts)
        {
            using var hmac = new HMACSHA512();

            account.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("123456"));
            account.PasswordSalt = hmac.Key;

            account.CreatedDate = DateTime.Now;
            account.IsLoginWithGmail = false;

            context.Account.Add(account);
        }

        await context.SaveChangesAsync();
    }

    public static async Task SeedHostel(DataContext context)
    {
        //if(await context.Hostel.AnyAsync())
        //{
        //    return;
        //}

        var hostelData = await File.ReadAllTextAsync("HostelSeedData.json");
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var hostels = JsonSerializer.Deserialize<List<Hostel>>(hostelData);

        foreach (var hostel in hostels)
        {
            context.Hostel.Add(hostel);
        }

        await context.SaveChangesAsync();
    }
}