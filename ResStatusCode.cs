
namespace pringApp
{
    enum ResStatusCode
    {
        // 摘要:
        // 处理成功
        Success = 200,
        Connect = 201,
        // 不支持get方式。
        NoGet = 501,
        // 请设置Content-Length值。
        Length = 502,
        // 数据格式错误。
        Forma_Error = 503,
        NO_Printer = 504,
    }
}
