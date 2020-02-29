using NinjaDomain.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NinjaDomain.DataModel
{
  public class DataHelpers
  {
    public static void NewDbWithSeed() {
     
      Database.SetInitializer(new DropCreateDatabaseAlways<NinjaContext>());
      using (var context = new NinjaContext()) {
        if (context.Ninjas.Any()) {
          return;
        }
        var vtClan = context.Clans.Add(new Clan { ClanName = "Vermont Clan" });
        var turtleClan = context.Clans.Add(new Clan { ClanName = "Turtles" });
        var amClan = context.Clans.Add(new Clan { ClanName = "American Ninja Warriors" });

        var j = new Ninja
        {
          Name = "JulieSan",
          ServedInOniwaban = false,
          DateOfBirth = new DateTime(1980, 1, 1),
          Clan = vtClan

        };
        var s = new Ninja
        {
          Name = "SampsonSan",
          ServedInOniwaban = false,
          DateOfBirth = new DateTime(2008, 1, 28),
          ClanId = 1

        };
        var l = new Ninja
        {
          Name = "Leonardo",
          ServedInOniwaban = false,
          DateOfBirth = new DateTime(1984, 1, 1),
          Clan = turtleClan
        };
        var r = new Ninja
        {
          Name = "Raphael",
          ServedInOniwaban = false,
          DateOfBirth = new DateTime(1985, 1, 1),
          Clan = turtleClan
        };
        context.Ninjas.AddRange(new List<Ninja> { j, s, l, r });
        var ninjaKC = new Ninja
        {
          Name = "Kacy Catanzaro",
          ServedInOniwaban = false,
          DateOfBirth = new DateTime(1990, 1, 14),
          Clan = amClan
        };
        var muscles = new NinjaEquipment
        {
          Name = "Muscles",
          Type = EquipmentType.Tool,

        };
        var spunk = new NinjaEquipment
        {
          Name = "Spunk",
          Type = EquipmentType.Weapon
        };

        ninjaKC.EquipmentOwned.Add(muscles);
        ninjaKC.EquipmentOwned.Add(spunk);
        context.Ninjas.Add(ninjaKC);

        context.SaveChanges();
        context.Database.ExecuteSqlCommand(
          @"CREATE PROCEDURE GetOldNinjas
                    AS  SELECT * FROM Ninjas WHERE DateOfBirth<='1/1/1980'");

        context.Database.ExecuteSqlCommand(
           @"CREATE PROCEDURE DeleteNinjaViaId
                     @Id int
                     AS
                     DELETE from Ninjas Where Id = @id
                     RETURN @@rowcount");
      }
    }
  }
}

