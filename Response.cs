
namespace pringApp
{
    class Response
    {
        private string code;
        private string describe = "成功";

        public string stauts
        {
            get
            {
                return code;
            }
            set
            {
                code = value;
            }
        }

        public string desc
        {
            get
            {
                return describe;
            }
            set
            {
                describe = value;
            }
        }
    }
    
}
