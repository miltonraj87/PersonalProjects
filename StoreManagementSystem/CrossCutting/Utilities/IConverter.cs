using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossCutting.Utilities
{
    public interface IConverter<Source>
        where Source : class
    {
        SqlParameter[] ConvertToSqlParameter(Source source);
    }

    public interface IConverter<Source, Target>
        where Source : class
        where Target : class, new()
    {
        Target Convert(Source input);
        SqlParameter[] ConvertToSqlParameter(Source source);
    }
}
