using System.Linq;

namespace Ryan.WebAPI.Models
{
    public class ResultParameter<T>
    {

        public ResultParameter()
        {
            this.code = 200;
            this.msg = string.Empty;
        }

        public int code { get; set; }

        public string msg { get; set; }

        public int count { get; set; }

        public  IQueryable<T> data{ get; set; }
    }
}