using System;

namespace PlayerSheetApp.Entity
{
    public sealed class Player
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public Nullable<DateTime> BirthDay { get; set; }
    }
}
