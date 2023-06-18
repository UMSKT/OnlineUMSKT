using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection;

namespace OnlineUMSKT.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateKeyController : ControllerBase
    {

        private readonly ILogger<GenerateKeyController> _logger;

        public GenerateKeyController(ILogger<GenerateKeyController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GenerateKey")]
        public string Get(string bink, int channelId, int amount = 1, bool i_am_not_spending_money_on_this_and_know_what_im_doing = false)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
            if (!i_am_not_spending_money_on_this_and_know_what_im_doing)
            {
                return "If you spent money on this tool, STOP! You have been scammed and should demand your money back immediately, and should report the seller to Microsoft via https://www.microsoft.com/en-us/howtotell/cfr/report.aspx.\n\n" +
                    "By using this tool, you agree to the AGPLv3 license at https://www.gnu.org/licenses/agpl-3.0.en.html.\nYou can find our GitHub repository here: https://github.com/UMSKT/UMSKT\n" +
                    "If you are embedding this into your own program, please include a copy of the repo link and the AGPLv3 license.\n" +
                    "To continue (and to agree to the license), add \"&i_am_not_spending_money_on_this_and_know_what_im_doing=true\" to the URI's arguments.\n\n" +
                    "Also, please don't sell these keys online. Both us and Microsoft won't be happy.";
            }

            string paddedNumber = channelId.ToString("D3");
            
            Process umsktProcess = new Process();
            umsktProcess.StartInfo.FileName = "xpkey";
            umsktProcess.StartInfo.Arguments = $"-b {bink} -c {paddedNumber} -n {amount} -f {Path.GetFullPath("keys.json")}";
            umsktProcess.StartInfo.RedirectStandardOutput = true;
            umsktProcess.Start();
            umsktProcess.WaitForExit();
            return umsktProcess.StandardOutput.ReadToEnd();
        }
    }

    [ApiController]
    [Route("[controller]")]
    public class GenerateCIDController : ControllerBase
    {

        private readonly ILogger<GenerateCIDController> _logger;

        public GenerateCIDController(ILogger<GenerateCIDController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GenerateCID")]
        public string Get(string iid, bool i_am_not_spending_money_on_this_and_know_what_im_doing = false)
        {
            Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

            if (!i_am_not_spending_money_on_this_and_know_what_im_doing)
            {
                return "If you spent money on this tool, STOP! You have been scammed and should demand your money back immediately, and should report the seller to Microsoft via https://www.microsoft.com/en-us/howtotell/cfr/report.aspx.\n\n" +
                    "By using this tool, you agree to the AGPLv3 license at https://www.gnu.org/licenses/agpl-3.0.en.html.\nYou can find our GitHub repository here: https://github.com/UMSKT/UMSKT\n" +
                    "If you are embedding this into your own program, please include a copy of the repo link and the AGPLv3 license.\n" +
                    "To continue (and to agree to the license), add \"&i_am_not_spending_money_on_this_and_know_what_im_doing=true\" to the URI's arguments.\n\n" +
                    "Also, please don't sell these keys online. Both us and Microsoft won't be happy.";
            }

            Process umsktProcess = new Process();
            umsktProcess.StartInfo.FileName = "xpkey";
            umsktProcess.StartInfo.Arguments = $"-i {iid} -f {Path.GetFullPath("keys.json")}";
            umsktProcess.StartInfo.RedirectStandardOutput = true;
            umsktProcess.Start();
            umsktProcess.WaitForExit();
            return umsktProcess.StandardOutput.ReadToEnd();
        }
    }
}