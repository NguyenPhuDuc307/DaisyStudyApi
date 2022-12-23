using Data.EF;
using Microsoft.EntityFrameworkCore;
using Models;
using Utils;
using Utils.Helpers;

namespace Application.Contacts;

public class ContactService : IContactService
{
    private readonly DaisyStudyDbContext _context;

    public ContactService(DaisyStudyDbContext context)
    {
        _context = context;
    }

    public async Task<ApiResult<int>> Create(ContactRequest request)
    {
        if (string.IsNullOrEmpty(request.Message) || string.IsNullOrWhiteSpace(request.Message)) return new ApiErrorResult<int>("Vui lòng nhập nội dung");

        Data.Entities.Contact contact = new Data.Entities.Contact()
        {
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Message = request.Message
        };
        await _context.Contacts.AddAsync(contact);
        var result = await _context.SaveChangesAsync();

        if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrWhiteSpace(request.Email))
        {
            SendMail.SendEmail(request.Email, "Daisy Study",
                $"<!DOCTYPE html>\n" +
                $"<html lang=\"en\">\n" +
                $"<head>\n" +
                $"<meta charset=\"utf-8\" />\n" +
                $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\n" +
                $"<link\n" +
                $"href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\"\n" +
                $"rel=\"stylesheet\"\n      " +
                $"integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\"\n" +
                $"crossorigin=\"anonymous\"\n/>\n" +
                $"</head>\n" +
                $"<body class=\"container\">\n\n" +
                $"<h1>Tư vấn - hỏi đáp</h1>\n" +
                $"<p>Cảm ơn bạn đã gửi yêu cầu.</p>\n" +
                $"<p>Chúng tôi sẽ phản hồi trong thời gian sớm nhất.</p>\n" +
                $"</body>\n" +
                $"</html>\n", "");
        }

        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<int>> Delete(int Id)
    {
        var contact = await _context.Contacts.FindAsync(Id);
        if (contact == null) return new ApiErrorResult<int>("Liên hệ không tồn tại");

        _context.Contacts.Remove(contact);
        var result = await _context.SaveChangesAsync();
        return new ApiSuccessResult<int>(result);
    }

    public async Task<ApiResult<IEnumerable<ContactViewModel>>> GetAll()
    {
        var contactViewModels = await _context.Contacts.Select(x => new ContactViewModel()
        {
            ContactId = x.ContactId,
            Name = x.Name,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Message = x.Message
        }).ToListAsync();
        return new ApiSuccessResult<IEnumerable<ContactViewModel>>(contactViewModels);
    }

    public async Task<ApiResult<ContactViewModel>> GetById(int Id)
    {
        var contact = await _context.Contacts.FindAsync(Id);
        if (contact == null) return new ApiErrorResult<ContactViewModel>("Liên hệ không tồn tại");
        var contactViewModel = new ContactViewModel()
        {
            ContactId = contact.ContactId,
            Name = contact.Name,
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            Message = contact.Message
        };
        return new ApiSuccessResult<ContactViewModel>(contactViewModel);
    }

    public async Task<ApiResult<int>> Reply(int Id, string ReplyMessage)
    {
        if (string.IsNullOrEmpty(ReplyMessage) || string.IsNullOrWhiteSpace(ReplyMessage)) return new ApiErrorResult<int>("Vui lòng nhập nội dung");
        var contact = await _context.Contacts.FindAsync(Id);
        if (contact == null) return new ApiErrorResult<int>("Liên hệ không tồn tại");
        contact.Reply = ReplyMessage;
        var result = await _context.SaveChangesAsync();
        if (!string.IsNullOrEmpty(contact.Email) && !string.IsNullOrWhiteSpace(contact.Email))
        {
            SendMail.SendEmail(contact.Email, "Daisy Study",
                $"<!DOCTYPE html>\n" +
                $"<html lang=\"en\">\n" +
                $"<head>\n" +
                $"<meta charset=\"utf-8\" />\n" +
                $"<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" />\n" +
                $"<link\n" +
                $"href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css\"\n" +
                $"rel=\"stylesheet\"\n      " +
                $"integrity=\"sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65\"\n" +
                $"crossorigin=\"anonymous\"\n/>\n" +
                $"</head>\n" +
                $"<body class=\"container\">\n\n" +
                $"<h1>Tư vấn - hỏi đáp</h1>\n" +
                $"<p>Phản hồi lại yêu cầu từ bạn {contact.Message}.</p>\n" +
                $"<p>Chúng tôi xin trả lời về vấn đề này:</p>\n" +
                $"{ReplyMessage}\n" +
                $"</body>\n" +
                $"</html>\n", "");
        }
        return new ApiSuccessResult<int>(result);
    }
}