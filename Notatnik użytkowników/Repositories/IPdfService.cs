using Notatnik_użytkowników.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Notatnik_użytkowników.Repositories
{
    public interface IPdfService
    {
        void GeneratePdf(Stream stream, IEnumerable<UserModel> userModelList);
    }
}