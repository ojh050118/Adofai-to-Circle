using System.Text;
using AdofaiToCircle.Adofai;
using Newtonsoft.Json;

namespace AdofaiToCircle
{
    public class AdofaiFileReader
    {
        public AdofaiFile AdofaiFile;

        public AdofaiFile Get(string file)
        {
            using (StreamReader sr = new StreamReader(file, Encoding.UTF8))
            {
                var text = sr.ReadToEnd();
                filterTrailingComma(ref text);

                AdofaiFile = JsonConvert.DeserializeObject<AdofaiFile>(text);
            }

            return AdofaiFile;
        }

        private string filterTrailingComma(ref string result)
        {
            return result = result.Replace(",,", ",");
        }
    }
}
