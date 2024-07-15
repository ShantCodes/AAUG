using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AAUG.DomainModels.Enums;

public class AaugRoles
{
    public const short King = 1;
    public const short Varich = 2;
    public const short Hanxnakhumb = 3;
    public const short Antam = 4;

    public static string Mapper(short id)
    {
        return id switch
        {
            King => "King",
            Varich => "Varich",
            Hanxnakhumb => "Hanxnakhumb",
            Antam => "Antam",
            _ => "0"
        };
    }

    public class RoleDto
    {
        public string Name  { get; set; }
        public short Id { get; set; }
    }

    public static IEnumerable<string> GetAllRoles()
    {
        return new List<string> { Mapper(King), Mapper(Varich), Mapper(Hanxnakhumb), Mapper(Antam) };
    }

    public static IEnumerable<(short Id, string Name)> GetAllRolesWithIds()
    {
        return new List<(short, string)>
        {
            (King, Mapper(King)),
            (Varich, Mapper(Varich)),
            (Hanxnakhumb, Mapper(Hanxnakhumb)),
            (Antam, Mapper(Antam))
        };
    }

}

