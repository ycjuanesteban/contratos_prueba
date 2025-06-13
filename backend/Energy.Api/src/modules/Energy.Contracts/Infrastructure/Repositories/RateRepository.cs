using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using Npgsql;

namespace Energy.Contracts.Infrastructure.Repositories;

public class RateRepository : IRateRepository
{
    private NpgsqlConnection _connection;

    public RateRepository(NpgsqlConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _connection = connection;
    }

    public async Task<Result> CreateRateAsync(Rate rate, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        using var cmd = new NpgsqlCommand(@"
            INSERT INTO rates (id, name)
            VALUES (@id, @name);", _connection);

        cmd.Parameters.AddWithValue("id", rate.Id);
        cmd.Parameters.AddWithValue("name", rate.Name);

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        await _connection.CloseAsync();

        return Result.Success();
    }

    public async Task<Result<Rate>> GetRateAsync(Guid id, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        using var cmd = new NpgsqlCommand("SELECT id, name FROM rates r where r.id = @id", _connection);
        cmd.Parameters.AddWithValue("id", id);

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        Rate? rate = null;

        while (await reader.ReadAsync(cancellationToken))
        {
            var userId = reader.GetGuid(0);
            var name = reader.GetString(1);

            rate = Rate.Create(userId, name);
        }

        await _connection.CloseAsync();

        return rate == null ? Result.Failure<Rate>("Rate not found") : Result.Success(rate);
    }

    public async Task<Result<IList<Rate>>> GetRatesAsync(CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        IList<Rate> rates = [];

        using var cmd = new NpgsqlCommand("SELECT id, name FROM rates", _connection);
        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var id = reader.GetGuid(0);
            var name = reader.GetString(1);

            rates.Add(Rate.Create(id, name));
        }

        await _connection.CloseAsync();

        return Result.Success(rates);
    }
}