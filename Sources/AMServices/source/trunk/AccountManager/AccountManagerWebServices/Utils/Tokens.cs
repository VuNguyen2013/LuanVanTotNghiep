namespace ETrade
{
    public class Tokens
    {

        private string data, delimeter;
        private string[] tokens;
        private int index;

        public Tokens(string strdata, string delim)
        {
            //
            // TODO: Add constructor logic here
            //
            init(strdata, delim);
        }

        private void init(string strdata, string delim)
        {

            data = strdata;
            delimeter = delim;
            tokens = data.Split(delimeter.ToCharArray());
            index = 0;
        }

        public bool hasElements()
        {
            return (index < (tokens.Length));
        }

        public string nextElement()
        {
            if (index < tokens.Length)
            {
                return tokens[index++];
            }
            else
            {
                return null;
            }
        }
    } 
}
