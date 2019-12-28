using Microsoft.EntityFrameworkCore;
using PlayerSheetApp.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerSheetApp.Data
{
    public class PlayerItem
    {
        /// <summary>
        /// 識別碼
        /// </summary>
        public int Id { get; set; }

        [Required(ErrorMessage = "請輸入背號")]
        /// <summary>
        /// 背號
        /// </summary>
        public Nullable<int> Number { get; set; }

        [Required(ErrorMessage = "請輸入名字")]
        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        [Required(ErrorMessage = "請輸入出生日期")]
        /// <summary>
        /// 出生日期
        /// </summary>
        public Nullable<DateTime> BirthDay { get; set; }
    }

    public class PlayerService
    {
        private readonly PlayerSheetContext _context;

        public PlayerService(PlayerSheetContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<PlayerItem>> GetPlayersAsync()
        {
            if(this._context.Players.Count() == 0)
            {
                this._context.Players.Add(new Player() { Id = 1, Name = "Messi", Number = 10, BirthDay = new DateTime(1987, 6, 24) });
                this._context.Players.Add(new Player() { Id = 2, Name = "Cristiano Ronaldo", Number = 7, BirthDay = new DateTime(1985, 2, 5) });
                await this._context.SaveChangesAsync();
            }


            var _players = await this._context.Players
                                                .Select(x => new PlayerItem { 
                                                    Id = x.Id,
                                                    Name = x.Name,
                                                    Number = x.Number,
                                                    BirthDay = x.BirthDay
                                                })
                                                .ToListAsync();

            return _players;
        }

        public async Task<PlayerItem> GetPlayerAsync(int Id)
        {
            var _player = await this._context.Players
                                                .Where(x => x.Id == Id)
                                                .FirstOrDefaultAsync();

            PlayerItem _item = new PlayerItem();
            _item.Id = _player.Id;
            _item.Number = _player.Number;
            _item.Name = _player.Name;
            _item.BirthDay = _player.BirthDay;

            return _item;
        }

        public async Task<int> CreateAsync(PlayerItem player)
        {
            var _player = new Player();
            _player.Number = player.Number ?? 0;
            _player.Name = player.Name;
            _player.BirthDay = player.BirthDay;

            await this._context.Players.AddAsync(_player);
            await this._context.SaveChangesAsync();

            return _player.Id;
        }

        public async Task<bool> UpdateAsync(PlayerItem player)
        {
            var _player = new Player();
            _player.Id = player.Id;
            _player.Number = player.Number ?? 0;
            _player.Name = player.Name;
            _player.BirthDay = player.BirthDay;

            this._context.Entry(_player).State = EntityState.Modified;

            await this._context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var _player = new Player();
            _player.Id = Id;

            this._context.Remove(_player);

            await this._context.SaveChangesAsync();

            return true;
        }
    }
}
