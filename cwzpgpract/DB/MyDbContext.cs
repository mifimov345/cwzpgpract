using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace cwzpgpract.DB
{
    public class MyDbContext : DbContext
    {
        private readonly DbContextOptions<MyDbContext> _options;
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) 
        {
            _options = options;
            Database.EnsureCreated();
        }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<Evented> Eventeds => Set<Evented>();
        public DbSet<PlayableChar> PlayableChars => Set<PlayableChar>();
        public DbSet<NPC> NPCS => Set<NPC>();

        public DbSet<Journal> Journals => Set<Journal>();

        private IDbContextTransaction _currentTransaction;

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                if (_currentTransaction == null)
                {
                    _currentTransaction = await Database.BeginTransactionAsync();
                }

                var result = await base.SaveChangesAsync(cancellationToken);

                _currentTransaction.Commit();

                return result;
            }
            catch (Exception ex)
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Rollback();
                }
                Console.WriteLine(ex.Message);
                return 0;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        public override int SaveChanges()
        {
            using (var transaction = Database.BeginTransaction())
            {
                try
                {
                    var result = base.SaveChanges();
                    transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine(ex.Message);
                    return 0;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=root;Host=localhost;Port=5432;Database=ZPGPR;ApplicationName=ZPGPR");
        }
    }
}
