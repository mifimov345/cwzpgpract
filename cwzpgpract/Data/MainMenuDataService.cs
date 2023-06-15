using cwzpgpract.DB;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace cwzpgpract.Data
{
    public class MainMenuDataService
    {
        public List<Journal>? LastFourEntities { get; set; }
        [Required]
        [StringLength(14, ErrorMessage = "Слишком длинное значение.")]
        public string? Particle { get; set; }

        private static readonly string[] _Verbs = new[]
        {
            "Пойди", "Ограбь", "Сломай", "Заработай"
        };
        private static readonly string[] _Prefixes = new[] 
        {
            "в", "на", "его", " "
        };
        public Task<MainMenuData[]> GetCombosAsync()
        {
            return Task.FromResult(Enumerable.Range(0, _Verbs.Length).Select(index => new MainMenuData
            {
                Acting = _Verbs[index],
                Pretexing = _Prefixes[index]
            }).ToArray());
        }
        public Task<List<Journal>> GetFourAsync()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyDbContext>();
            optionsBuilder.UseNpgsql("Postgres");
            var dbContext = new MyDbContext(optionsBuilder.Options);
            LastFourEntities = dbContext.Journals.OrderByDescending(e => e.Id).Take(4).ToList();
            return Task.FromResult(LastFourEntities);
        }
    }
}
