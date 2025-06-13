using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Domain.Entities;
using Energy.Helpers.Result;
using Npgsql;

namespace Energy.Contracts.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private NpgsqlConnection _connection;

    public UserRepository(NpgsqlConnection connection)
    {
        ArgumentNullException.ThrowIfNull(connection);

        _connection = connection;
    }

    public async Task<Result> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        using var cmd = new NpgsqlCommand(@"
            INSERT INTO users (id, name, last_name, dni)
            VALUES (@id, @name, @lastName, @dni);", _connection);

        cmd.Parameters.AddWithValue("id", user.Id);
        cmd.Parameters.AddWithValue("name", user.Name);
        cmd.Parameters.AddWithValue("lastName", user.LastName);
        cmd.Parameters.AddWithValue("dni", user.DNI);
        
        await cmd.ExecuteNonQueryAsync(cancellationToken);

        await _connection.CloseAsync();

        return Result.Success();
    }

    public async Task<Result<IList<User>>> GetUsersAsync(CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        IList<User> users = [];

        using var cmd = new NpgsqlCommand("SELECT id, name, last_name, dni FROM users", _connection);
        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        while (await reader.ReadAsync(cancellationToken))
        {
            var id = reader.GetGuid(0);
            var name = reader.GetString(1);
            var lastName = reader.GetString(2);
            var dni = reader.GetString(3);

            users.Add(User.Create(id, name, lastName, dni));
        }

        await _connection.CloseAsync();

        return Result.Success(users);
    }

    public async Task<Result<User>> GetUserAsync(Guid id, CancellationToken cancellationToken)
    {
        await _connection.OpenAsync(cancellationToken);

        using var cmd = new NpgsqlCommand("SELECT id, name, last_name, dni FROM users u where u.id = @id", _connection);
        cmd.Parameters.AddWithValue("id", id);

        using var reader = await cmd.ExecuteReaderAsync(cancellationToken);

        User? user = null;
        while (await reader.ReadAsync(cancellationToken))
        {
            var userId = reader.GetGuid(0);
            var name = reader.GetString(1);
            var lastName = reader.GetString(2);
            var dni = reader.GetString(3);

            user = User.Create(userId, name, lastName, dni);
        }

        await _connection.CloseAsync();


        return user != null ? Result.Success(user) : Result.Failure<User>("User not found");
    }
}