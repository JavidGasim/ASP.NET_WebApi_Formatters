using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using WebApiDemo3_22_10.Dtos;

namespace WebApiDemo3_22_10.Formatters
{
    public class VCardInputFormatter : TextInputFormatter
    {
        public VCardInputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var request = context.HttpContext.Request;
            return await ReadCustomType(request, encoding);
        }

        private async Task<InputFormatterResult> ReadCustomType(HttpRequest request, Encoding encoding)
        {
            using (var reader = new StreamReader(request.Body, encoding))
            {
                var content = await reader.ReadToEndAsync();
                var datas = content.Split('-');

                if (datas.Length != 4)
                {
                    return await InputFormatterResult.FailureAsync();

                }

                var dto = new StudentAddDto
                {
                    Fullname = datas[0].Trim(),
                    SeriaNo = datas[1].Trim(),
                    Age = int.Parse(datas[2].Trim()),
                    Score = double.Parse(datas[3].Trim())
                };

                return await InputFormatterResult.SuccessAsync(dto);
            }
        }
    }
}
