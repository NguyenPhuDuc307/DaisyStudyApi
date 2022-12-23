using Models;

namespace Application.Contacts;

public interface IContactService
{
    Task<ApiResult<int>> Create(ContactRequest request);
    Task<ApiResult<int>> Delete(int Id);
    Task<ApiResult<IEnumerable<ContactViewModel>>> GetAll();
    Task<ApiResult<ContactViewModel>> GetById(int Id);
    Task<ApiResult<int>> Reply(int Id, string ReplyMessage);
}