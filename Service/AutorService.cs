using Chayanne_P1_2_P4.Models;
using Chayanne_P1_2_P4.Sql;
using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.Sqlite;
using System.Data;

namespace Chayanne_P1_2_P4.Service;

public class AutorService(IConfiguration configuration)
{
    private SqliteConnection createConnection => new SqliteConnection(configuration.GetConnectionString("SqliteConnection"));

    public async Task<Autor> CreateAsync(Autor autorDto)
    {
        using var connection = createConnection;
        var resultado = await connection.ExecuteScalarAsync<int>(AutorSql.Insert, autorDto);

        if (resultado == null || resultado < 1)
        {
            throw new Exception("Fallo la insercion del autor en la base de datos.");
        };

        return autorDto with { IdAutor = resultado};
    }

    public async Task<IEnumerable<Autor>> GetAsync()
    {
        using var connection = createConnection;
        var resultado = await connection.QueryAsync<Autor>(AutorSql.GetTable);

        return resultado;
    }

    public async Task<Autor> GetIdAsync(int IdAutor)
    {
        using var connection = createConnection;
        Autor autor = await connection.QueryFirstOrDefaultAsync<Autor>(AutorSql.GetRow, new { IdAutor });

        if (autor == null)
        {
            throw new Exception($"Fallo la optencion del autor ID:{IdAutor} en la base de datos.");
        };

        return autor;
    }

    public async Task<bool> UpdateAsync(Autor autorDto)
    {
        using var connection = createConnection;
        var resultado = await connection.ExecuteAsync(AutorSql.Update, autorDto);

        if (resultado < 1)
        {
            throw new Exception($"Fallo la Actualizacion del autor ID:{autorDto.IdAutor} en la base de datos");
        }

        return true;
    }
    public async Task<bool> DeleteAsync(int IdAutor)
    {
        using var connection = createConnection;
        var resultado = await connection.ExecuteAsync(AutorSql.Delete, new { IdAutor });

        if (resultado < 1)
        {
            throw new Exception($"Fallo la eliminacion del Autor ID:{IdAutor} en la base de datos");
        };

        return true;
    }
}
