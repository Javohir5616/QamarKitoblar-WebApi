﻿using Dapper;
using QamarKitoblar.DataAccess.Interfaces.Books;
using QamarKitoblar.DataAccess.Utils;
using QamarKitoblar.DataAccess.ViewModels.BooksVM;
using QamarKitoblar.Domain.Entities.Books;

namespace QamarKitoblar.DataAccess.Repositories.Books;

public class BookRepository : BaseRepository, IBookRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = "Select Count(*) From books";
            var result=await _connection.QuerySingleAsync<long>(query);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(Book entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.books(name, author, description, image_path, unit_price, is_have_electron, gener_id, publisher_id, created_at, updated_at) " +
                "VALUES (@Name, @Author, @Description, @ImagePath, @UnitPrice, @IsHaveElectron, @GenerId, @PublisherId, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "DELETE FROM books WHERE id=@Id;";
            var result = await _connection.ExecuteAsync(query, new { Id = id });
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<IList<BookViewModel>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<BookViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<(long ItemsCount, IList<BookViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Book entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.books SET " +
                "id=@Id, name=@Name, author=@Author, description=@Description, image_path=@ImagePath, " +
                "unit_price=@UnitPrice, is_have_electron=@IsHaveElectron, gener_id=@GenerId, publisher_id=@PublisherId, " +
                "created_at=@CreatedAt, updated_at=@UpdatedAt " +
                $"WHERE id={id};";
            var result = await _connection.ExecuteAsync(query, entity);
            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
