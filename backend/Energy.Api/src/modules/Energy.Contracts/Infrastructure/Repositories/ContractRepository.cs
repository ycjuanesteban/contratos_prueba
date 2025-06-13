using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using Npgsql;
using System.Diagnostics.Contracts;
using Contract = Energy.Contracts.Domain.Entities.Contract;

namespace Energy.Contracts.Infrastructure.Repositories;

public class ContractRepository : IContractRepository
{
    private NpgsqlConnection _connection;

    public ContractRepository(NpgsqlConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _connection = connection;
    }

    public async Task<Result> CreateAsync(Contract contract, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        using var cmd = new NpgsqlCommand(@"
            INSERT INTO contracts (id, user_id, rate_id, hiring_date)
            VALUES (@id, @userId, @rateId, @hiringDate);", _connection);

        cmd.Parameters.AddWithValue("id", contract.Id);
        cmd.Parameters.AddWithValue("userId", contract.User.Id);
        cmd.Parameters.AddWithValue("rateId", contract.Rate.Id);
        cmd.Parameters.AddWithValue("hiringDate", contract.HiringDate);

        await cmd.ExecuteNonQueryAsync(cancellationToken);

        await _connection.CloseAsync();

        return Result.Success();
    }

    public async Task<Result<IList<Contract>>> GetAllAsync(CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        IList<Contract> contracts = [];

        using var cmd = new NpgsqlCommand(@"
            SELECT
                c.id, c.hiring_date,
                u.id, u.name, u.last_name, u.dni,
                r.id, r.name
            FROM contracts c
            JOIN users u ON c.user_id = u.id
            JOIN rates r ON c.rate_id = r.id", _connection);

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var user = User.Create(reader.GetGuid(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
            var rate = Rate.Create(reader.GetGuid(6), reader.GetString(7));

            var contract = Contract.Create(reader.GetGuid(0), user, rate, reader.GetDateTime(1));
            contracts.Add(contract);
        }

        await _connection.CloseAsync();

        return Result.Success(contracts);
    }

    public async Task<Result<Contract>> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        using var cmd = new NpgsqlCommand(@"
            SELECT
                c.id, c.hiring_date,
                u.id, u.name, u.last_name, u.dni,
                r.id, r.name
            FROM contracts c
            JOIN users u ON c.user_id = u.id
            JOIN rates r ON c.rate_id = r.id
            WHERE c.id = @id", _connection);

        cmd.Parameters.AddWithValue("id", id);

        Contract? contract = null;

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);
        while (await reader.ReadAsync(cancellationToken))
        {
            var user = User.Create(reader.GetGuid(2), reader.GetString(3), reader.GetString(4), reader.GetString(5));
            var rate = Rate.Create(reader.GetGuid(6), reader.GetString(7));

            contract = Contract.Create(reader.GetGuid(0), user, rate, reader.GetDateTime(1));
        }

        await _connection.CloseAsync();

        return contract == null ? 
            Result.Failure<Contract>("Contract not found") : 
            Result.Success(contract);
    }

    public async Task<Result> UpdateAsync(Contract contract, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync();

        using var cmd = new NpgsqlCommand(@"
        UPDATE contracts
        SET user_id = @userId,
            rate_id = @rateId,
            hiring_date = @hiringDate
        WHERE id = @id", _connection);

        cmd.Parameters.AddWithValue("id", contract.Id);
        cmd.Parameters.AddWithValue("userId", contract.User.Id);
        cmd.Parameters.AddWithValue("rateId", contract.Rate.Id);
        cmd.Parameters.AddWithValue("hiringDate", contract.HiringDate);

        int filasAfectadas = cmd.ExecuteNonQuery();

        await _connection.CloseAsync();

        return contract == null ?
            Result.Failure<Contract>("Contract not found") :
            Result.Success(contract);
    }
}