namespace ClothingStoreBackend.Models.ResponseModels
{
    public class RespondAPI<T> where T: class
    {
        public ResultRespond Result { get; set; }
        public string Code { get; set; } = "00";
        public string Message { get; set; }
        public T Data { get; set; }

        public RespondAPI()
        {
            
        }
        public RespondAPI(ResultRespond result,string code, string message, T data)
        {
            Result = result;
            Code = code;
            Message = message;
            Data = data;
        }
    }
    public enum ResultRespond
    {
        Error, Succeeded, Failed, NotFound, Duplication
    }
}