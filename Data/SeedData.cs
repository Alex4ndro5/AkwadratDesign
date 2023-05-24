using AkwadratDesign.Models.DbModels;

namespace AkwadratDesign.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (!context.Clients.Any())
            {
                var clients = new[]
                {
                new Client
                {
                    Name = "John",
                    Surname = "Doe",
                    Email = "johndoe@example.com",
                    Message = "Hello, I'm John Doe!"
                },
                new Client
                {
                    Name = "Jane",
                    Surname = "Smith",
                    Email = "janesmith@example.com",
                    Message = "Hi, I'm Jane Smith!"
                }
            };

                context.Clients.AddRange(clients);
                context.SaveChanges();
            }

            if (!context.Firms.Any())
            {
                var firms = new[]
                {
                new Firm
                {
                    FirmName = "ABC Company"
                },
                new Firm
                {
                    FirmName = "XYZ Corporation"
                }
            };

                context.Firms.AddRange(firms);
                context.SaveChanges();
            }

            // Add more seed data for other models (e.g., Project, ProjectFirm) if needed
        }
    }
}
