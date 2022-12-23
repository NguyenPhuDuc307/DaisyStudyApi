namespace Models;
public class ApiSuccessResult<T> : ApiResult<T>
{
    public ApiSuccessResult(T resultObj)
    {
        IsSuccess = true;
        ResultObj = resultObj;
        Message = "Success";
    }

    public ApiSuccessResult()
    {
        IsSuccess = true;
    }
}