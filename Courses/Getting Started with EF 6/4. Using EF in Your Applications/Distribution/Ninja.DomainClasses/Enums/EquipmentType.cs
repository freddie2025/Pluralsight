using System.ComponentModel;

namespace NinjaDomain.Classes
{
  public enum EquipmentType
  {
    [Description("Tool")]
    Tool = 1,
    [Description("Weapon")]
    Weapon = 2,
    [Description("Outerwear")]
    Outerwear = 3
  }
}