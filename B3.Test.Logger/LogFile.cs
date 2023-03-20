using System.Collections.Generic;
using System.IO;

namespace B3.Test.Logger
{
    public class LogFile
    {
        public string FilePath = "";
        public char OpenBracket = '[';
        public char CloseBracket = ']';

        private ESecureLevel Lock;
        private bool UseExtension = true;
        private List<Log> _Data;
        public List<Log> Data
        {
            get
            {
                if (Lock == ESecureLevel.SEE_ONLY || Lock == ESecureLevel.ALL)
                    return _Data;

                return null;
            }


            set
            {
                if (Lock == ESecureLevel.SET_ONLY || Lock == ESecureLevel.ALL)
                    _Data = value;

                return;
            }

        }
        
        public string Format = "{0}{2}{1}{0}{3}{1} {4}";
        public LogFile(string filePath, ESecureLevel lockLevel, bool useExtension)
        {
            this.FilePath = filePath; this.Lock = lockLevel;
            this.UseExtension = useExtension;
            this._Data = new List<Log>();
        }

        public void Write(LogType type, string data)
        {
            Log log = new Log(this, type, data);
            using (StreamWriter stream = new StreamWriter(FilePath + (UseExtension ? ".log" : ""), true))
            {
                stream.WriteLine(log.ToString());
            }
            _Data.Add(log);
        }
    }
}
