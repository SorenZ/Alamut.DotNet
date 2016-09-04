namespace Alamut.Helpers.Exception
{
    public static class ExceptionExtension
    {
        /// <summary>
        /// Getting all messages from InnerException(s)
        /// </summary>
        /// <param name="e"></param>
        /// <param name="msgs"></param>
        /// <returns></returns>
        /// <summary>
        /// http://stackoverflow.com/questions/9314172/getting-all-messages-from-innerexceptions
        /// </summary>
        public static string GetExceptionMessages(this System.Exception e, string msgs = "")
        {
            if (e == null) return string.Empty;
            if (msgs == "") msgs = e.Message;
            if (e.InnerException != null)
                msgs += ", " + GetExceptionMessages(e.InnerException);

            return msgs;
        }
    }
}
