namespace SnippetManager.Helpers
{
    public class ResultObject<T>
    {
        public bool Succeded { get; protected set; }
        public T? Result { get; protected set; }
        public String? ErrorCode { get; protected set; }

        public ResultObject(T? result = default, String? errorCode = null, bool? succeded = null)
        {
            if (succeded != null)
            {
                Succeded = (bool) succeded;
                return;
            }

            Result = result;
            ErrorCode = errorCode;
            Succeded = errorCode == null;
        }
    }

    public class ResultObject : ResultObject<dynamic> {}
}
