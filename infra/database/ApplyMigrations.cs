using Microsoft.EntityFrameworkCore;



namespace BankSystem.Extensions
{
    public static class MigrationExtensions
    {
        public static IHost ApplyDatabaseMigrations(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;


                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {

                    var dbContext = services.GetRequiredService<BankContext>();


                    dbContext.Database.Migrate();
                    logger.LogInformation("Migrações aplicadas com sucesso.");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Ocorreu um erro ao aplicar as migrações.");

                }
            }
            return host;
        }
    }
}